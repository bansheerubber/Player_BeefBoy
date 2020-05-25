function spawnBull(%position) {
	%bull = new AiPlayer() {
		datablock = BeefBoyGuard2Armor;
		position = %position;
		rotation = "0 0 0 1";
		isBot = true;
		isBeefboyGuard = true;
		name = generateRandomName();
	};
	%bull.setAvatar("bull");
	%bull.bullTrackShield();
	%bull.bullSetState(0);
	%bull.mountImage(BullPikeImage, 0);
	%bull.setShapeName(%bull.name, 8564862);
	return %bull;
}

function AiPlayer::bullSetState(%this, %state, %flag) {
	cancel(%this.aiSchedule);
	switch(%state) {
		case 0: // pathfinding state, tries to find/chase a target
			%this.bullProjectilePosition = "";
			%this.bullProjectile = 0;
			%this.bullPikeApproach();
		
		case 1: // if we have a target, then attack it
			%this.bullProjectilePosition = "";
			%this.bullProjectile = 0;
			%this.bullPikeAttack();

		case 2: // this readies our charge
			%this.setMoveX(0);
			%this.setMoveY(0);
			%this.playThread(0, "pikeReadyToHigh");
			%this.playAudio(0, BeefboyGuard2ChargeReadySound);
			%this.emote(winStarProjectile, 1);
			%this.aiSchedule = %this.schedule(1000, bullPikeCharge);
		
		case 3: // if our target died before we charged, then we enter this state and gracefully exit the charge
			%this.setMoveX(0);
			%this.setMoveY(0);
			%this.playThread(0, "pikeHighToReady");
			%this.aiSchedule = %this.schedule(600, bullSetState, 0);
		
		case 4: // start charging. if we bash into a wall, exit this state and enter our stunned state
			%this.bullPikeChargeLoop();
			%this.mountImage(BeefboyGuard2ChargeImage, 3);
			%this.schedule(1500, unMountImageSafe, 3);
		
		case 5: // finish the charge and start skidding out as a fun side effect. also, if we bash into a wall, exit this state and enter our stunned state
			%this.setMoveX(0);
			%this.setMoveY(0);
			%this.playThread(0, "pikeHighToReady");
			%this.bullAlarmSchedule = %this.schedule(300, emote, AlarmProjectile);
			%this.aiSchedule = %this.schedule(600, bullSetState, 0, 5);
			%this.bullSkid(%this);
		
		case 6: // stunned state, we pause for a few seconds and are open to any attacks
			%this.setVelocity(vectorAdd(vectorScale(%this.bullChargeDirection, -5), "0 0 15"));
			%this.setMoveX(0);
			%this.setMoveY(0);
			%this.clearAim();
			%this.setImageTrigger(0, false);
			%this.playThread(3, "sit");

			if(%this.bullState == 4) {
				%this.playThread(0, "pikeHighToReady");
			}

			(new Projectile() {
				datablock = BeefboyGuard2CollisionProjectile;
				initialPosition = %this.getPosition();
				initialVelocity = vectorScale(%this.bullChargeDirection, 10);
				sourceObject = 0;
				sourceSlot = 0;
			}).explode();

			%this.playAudio(2, BeefboyGuard2HitWallSound);
			%this.playAudio(3, BeefboyGuard2StunSound);
			%this.mountImage(BeefboyGuard2StunImage, 2);
			%this.schedule(4000, unMountImageSafe, 2);
			%this.schedule(4000, playThread, 3, "root");
			%this.schedule(4000, playAudio, 1, BeefboyGuardKnifePutAwaySound);
			%this.aiSchedule = %this.schedule(4000, bullSetState, 0);
		
		case 7: // guard against incoming projectiles
			%this.setMoveX(0);
			%this.setMoveY(0);
			%this.clearAim();
			%this.setImageTrigger(0, false);
			%this.emote(AlarmProjectile);
			%this.bullProjectileGuard();
	}
	%this.bullState = %state;
}

function AiPlayer::bullGetPikeTarget(%this, %ignore) {
	%position = %this.getPosition();
	%radius =  100;
	initContainerRadiusSearch(%position, %radius, $TypeMasks::PlayerObjectType);
	%minDistance = %radius;
	while(%col = containerSearchNext()) {
		if(%col != %this && %col != %ignore && %col.getState() !$= "Dead" && (%distance = vectorDist(%position, %col.getPosition())) < %minDistance && %col.getClassName() $= "AiPlayer" && !%col.isBeefboyGuard) {
			%minDistance = %distance;
			%target = %col;
		}	
	}
	return %target;
}

function AiPlayer::bullPikeApproach(%this) {
	if(isObject(%this.bullProjectile)) {
		%this.bullSetState(7);
		return;
	}
	
	if(!isObject(%this.bullTarget) || %this.bullTarget.getState() $= "Dead") {
		%this.clearAim();
		%this.setMoveX(0);
		%this.setMoveY(0);
		%this.setImageTrigger(0, false);
		%this.bullMoveXDirection = 0;

		%this.bullTarget = %this.bullGetPikeTarget();
	}

	if(isObject(%this.bullTarget)) {
		%this.setAimObject(%this.bullTarget);
		%this.schedule(400, setMoveX, 0);
		%this.bullMoveXDirection = 0;
		%this.setMoveY(1);
		%this.setImageTrigger(0, false);

		%distance = vectorDist(%this.getPosition(), %this.bullTarget.getPosition());
		if(%distance < 10) {
			%this.bullSetState(1);
			return;
		}
		else if(%distance > 30 && !isEventPending(%this.bullSkidSchedule) && getSimTime() > %this.bullNextCharge) {
			%this.bullSetState(2);
			return;
		}
	}
	
	%this.aiSchedule = %this.schedule(100, bullPikeApproach);
}

function AiPlayer::bullPikeAttack(%this) {
	if(isObject(%this.bullProjectile)) {
		%this.bullSetState(7);
		return;
	}
	
	if(!isObject(%this.bullTarget) || %this.bullTarget.getState() $= "Dead") {
		%this.bullSetState(0);
		return;
	}

	%distance = vectorDist(%this.getPosition(), %this.bullTarget.getPosition());
	if(%distance > 10) {
		%this.bullSetState(0);
		return;
	}
	else if(%distance < 7) {
		%this.setMoveY(-1);
	}
	else {
		%this.setMoveY(0);
	}

	if(getSimTime() - %this.bullLastStopTime > %this.bullNextStopDuration) {
		if(%this.bullMoveXDirection == 0) {
			%this.bullMoveXDirection = getRandom(0, 1) ? -1 : 1;
		}
		%this.setMoveX(%this.bullMoveXDirection);
	}
	
	if(getSimTime() > %this.bullNextStopTime && %this.bullStopped == false) {
		%this.setMoveX(0);
		%this.bullMoveXDirection *= -1;
		%this.bullLastStopTime = getSimTime();
		%this.bullLastStopWait = getRandom(1000, 2500);
		%this.bullStopped = true;
	}
	
	if(%this.getImageState(0) $= "Idle" && getSimTime() - %this.bullLastFireTime > %this.bullNextFireTime) {
		%this.setImageTrigger(0, true);
		%this.bullLastFireTime = getSimTime();
		%this.bullNextFireTime = getRandom(800, 1500);

		if(getSimTime() - %this.bullLastStopTime > %this.bullLastStopWait) {
			%this.bullNextStopTime = getSimTime() + 500;
			%this.bullNextStopDuration = 100;
			%this.bullStopped = false;
		}
	}
	else {
		%this.setImageTrigger(0, false);
	}

	%this.aiSchedule = %this.schedule(100, bullPikeAttack);
}

function AiPlayer::bullPikeCharge(%this) {
	if(!isObject(%this.bullTarget) || %this.bullTarget.getState() $= "Dead") {
		%this.bullSetState(3);
		return;
	}

	%this.bullNextCharge = getSimTime() + getRandom(8000, 12000);
	%this.bullLastCharge = getSimTime();
	%this.bullChargeDirection = vectorNormalize(getWords(vectorSub(%this.bullTarget.getPosition(), %this.getPosition()), 0, 1));
	%this.bullChargeId = getRandom(0, 100000);
	%this.playAudio(1, BeefboyGuard2ChargeSound);
	%this.getMountedImage(0).endSwing(%this, 0);
	%this.getMountedImage(0).startSwing(%this, 0);
	%this.getMountedImage(0).schedule(1500, endSwing, %this, 0);
	%this.bullLastDizzyPosition = "";

	%this.bullSetState(4);
}

function AiPlayer::bullPikeChargeLoop(%this) {
	if(!%this.bullDizzyWalls()) {
		%this.setVelocity(vectorScale(%this.bullChargeDirection, %this.bullSkidSpeed = 35));

		%position = %this.getPosition();
		initContainerRadiusSearch(%position, 10, $TypeMasks::PlayerObjectType);
		while(%col = containerSearchNext()) {
			if(%col != %this && minigameCanDamage(%col, %this) == 1 && vectorDist(%position, %col.getPosition()) < 4 && !%col.bullHitCharge[%this.bullChargeId]) {
				%col.bullHitCharge[%this.bullChargeId] = true;
				%col.setVelocity(vectorAdd(vectorScale(%this.bullChargeDirection, 35), "0 0 10"));
				%col.damage(%this, %col.getPosition(), 75, 3);
				serverPlay3d(BeefboyGuard2ChargeHitSound, %col.getPosition());
			}
		}

		if(getSimTime() - %this.bullLastCharge > 1500) {
			%this.bullSetState(5);
			return;
		}

		%this.aiSchedule = %this.schedule(100, bullPikeChargeLoop);
	}
}

function AiPlayer::bullSkid(%this) {
	if(%this.bullSkidSpeed > 15 && !%this.bullDizzyWalls()) {
		%this.setVelocity(vectorScale(%this.bullChargeDirection, %this.bullSkidSpeed));
		%this.bullSkidSpeed *= 0.95;
		%this.playThread(2, plant);

		if(!isObject(%this.getMountedImage(1))) {
			%this.mountImage(BeefboyGuard2SkidImage, 1);
		}

		serverPlay3d(BeefboyGuard2SlowDownSound, %this.getPosition());

		%this.bullSkidSchedule = %this.schedule(100, bullSkid);
	}
	else {
		%this.unMountImageSafe(1);
	}
}

function AiPlayer::bullDizzyWalls(%this) {
	if(vectorLen(%this.getVelocity()) < 15 || %this.bullState == 6) {
		%this.bullLastDizzyPosition = "";
		return false;
	}
	else {
		%targetPosition = vectorAdd(%this.getHackPosition(), vectorScale(%this.bullChargeDirection, 1.5));
		%foundWall = !containerBoxEmpty($TypeMasks::fxBrickObjectType, %targetPosition, 1, 1, 0.3);
		
		if(%this.bullLastDizzyPosition !$= "") {
			%raycast = containerRaycast(%targetPosition, %this.bullLastDizzyPosition, $TypeMasks::fxBrickObjectType, false);
		}
		%this.bullLastDizzyPosition = %targetPosition;

		if(%foundWall || isObject(%raycast)) {
			cancel(%this.bullSkidSchedule);
			cancel(%this.bullAlarmSchedule);
			cancel(%this.aiSchedule);
			%this.bullSetState(6);
			return true;
		}
		else {
			return false;
		}
	}
}

function AiPlayer::bullProjectileGuard(%this) {
	%position = %this.bullProjectilePosition;

	if(isObject(%this.bullProjectile) && !%this.bullProjectile.exploded) {
		%direction = vectorNormalize(getWords(vectorSub(%this.bullProjectilePosition, %this.getPosition()), 0, 1));

		%angle = mACos(getWord(%direction, 0));
		if(getWord(%this.getPosition(), 1) - getWord(%this.bullProjectilePosition, 1) > 0) {
			%angle *= -1;
		}

		%newAngle = %angle - $pi / 4;
		%newDirection = mCos(%newAngle) SPC mSin(%newAngle) SPC 0;

		%this.setAimVector(%newDirection);

		%this.aiSchedule = %this.schedule(100, bullProjectileGuard);
	}
	else {
		%this.bullProjectilePosition = "";
		%this.bullProjectile = 0;
		%this.schedule(1200, bullSetState, 0);
	}
}

deActivatePackage(BeefBoyGuard2Package);
package BeefBoyGuard2Package {
	function AiPlayer::onProjectileWillHit(%this, %projectile, %distance, %speed) {
		Parent::onProjectileWillHit(%this, %projectile, %distance, %speed);
		
		if(%this.getDatablock().isBeefBoyGuard2 && %projectile.sourceObject != %this) {
			%time = %distance / %speed;
			if(%time < 1.5 && %time > 0.2) {
				%this.bullProjectilePosition = %projectile.getPosition();
				%this.bullProjectile = %projectile;
			}
		}
	}
};
activatePackage(BeefBoyGuard2Package);