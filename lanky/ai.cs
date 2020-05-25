function spawnLanky(%position) {
	%lanky = new AiPlayer() {
		datablock = BeefBoyGuardArmor;
		position = %position;
		rotation = "0 0 0 1";
		isBot = true;
		isBeefboyGuard = true;
		lankyInvalidTargetCount = 0;
		name = generateRandomName();
	};
	%lanky.setAvatar("lanky");
	%lanky.lankySetState(0);
	%lanky.setShapeName(%lanky.name, 8564862);
	return %lanky;
}

function AiPlayer::lankySetState(%this, %state) {
	cancel(%this.aiSchedule);
	%this.lankyState = %state;
	switch(%state) {
		case 0:
			%this.playThread(1, "armReadyRight");
			%this.lankySpear();
		
		case 1:
			%this.lankyKnife();
	}
}

function AiPlayer::lankyGetSpearTarget(%this, %ignore) {
	%position = %this.getPosition();
	%radius =  100;
	initContainerRadiusSearch(%position, %radius, $TypeMasks::PlayerObjectType);
	%minDistance = %radius;
	while(%col = containerSearchNext()) {
		if(%col != %this && %col != %ignore && %col.getState() !$= "Dead" && (%distance = vectorDist(%position, %col.getPosition())) < %minDistance && %col.getClassName() $= "AiPlayer" && !%col.isBeefboyGuard && %this.lankyIsTargetValid(%col)) {
			%minDistance = %distance;
			%target = %col;
		}	
	}
	return %target;
}

function AiPlayer::lankyKnifeCheck(%this) {
	%position = %this.getPosition();
	%radius = 5;
	initContainerRadiusSearch(%position, %radius, $TypeMasks::PlayerObjectType);
	%minDistance = %radius;
	while(%col = containerSearchNext()) {
		if(%col != %this && %col.getState() !$= "Dead" && (%distance = vectorDist(%position, %col.getPosition())) < 4) {
			return %col;
		}	
	}
	return false;
}

function AiPlayer::lankyKnife(%this) {
	%this.playThread(1, "root");
	%this.mountImage(LankyKnifeImage, 1);

	%this.setMoveY(-0.2);

	%this.lankyTarget = 0;

	%this.setImageTrigger(1, true);
}

function AiPlayer::lankyIsTargetValid(%this, %target) {
	if(getWord(%target.getVelocity(), 2) >= 1) {
		return false;
	}
	else {
		%speed = 50;
		%position = %this.getMuzzlePoint(0);
		%targetPosition = %target.getHackPosition();

		%count = 5;
		%total = 0;
		for(%i = 0; %i < %count; %i++) {
			%time = calculateProjectileFlightTime(%position, %targetPosition, %speed, 0.5);
			%targetPosition = %target.getPredictedPosition(%time);
			%total += %time;
		}

		%direction = vectorNormalize(vectorSub(%targetPosition, %target.getPosition()));
		%length = mClamp(vectorDist(%targetPosition, %target.getPosition()), 0, 500);
		echo(%length);
		%raycast = containerRaycast(%target.getPosition(), vectorAdd(%target.getPosition(), vectorScale(%direction, %length)), $TypeMasks::FxBrickObjectType | $TypeMasks::StaticObjectType, false);

		if(isObject(%raycast)) {
			return false;
		}
		else {
			return %targetPosition;
		}
	}
}

function AiPlayer::lankySpear(%this) {
	if(%this.getState() $= "Dead") {
		return;
	}
	
	// check to make sure we don't need to lanky stab
	if(%this.lankyKnifeTarget = %this.lankyKnifeCheck()) {
		%this.schedule(300, lankySetState, 1);
		%this.setAimObject(%this.lankyKnifeTarget);
		return;
	}
	
	if(!isObject(%this.lankyTarget) || %this.lankyTarget.getState() $= "Dead") {
		%this.clearAim();
		%this.setMoveX(0);
		%this.setMoveY(0);
		
		if(!isObject(%this.lankyInvalidTarget) || %this.lankyInvalidTarget.getState() $= "Dead") {
			%this.lankyTarget = %this.lankyGetSpearTarget();
			%this.lankyInvalidTarget = 0;
		}
		else {
			%this.lankyTarget = %this.lankyInvalidTarget;
			%this.lankyInvalidTarget = 0;
		}

		if(!isObject(%this.lankyTarget)) {
			%this.aiSchedule = %this.schedule(100, lankySpear); // reset and look for another target if we didn't find any
			return;
		}
	}
	if(%this.getMountedImage(0).getName() !$= "BeefboyGuardSpearImage") {
		%this.mountImage(BeefboyGuardSpearImage, 0);
		%this.playThread(1, "armReadyRight");
	}

	%this.setAimObject(%this.lankyTarget);
	%this.setVelocity(%this.getVelocity());

	// determine if we're able to fire our spear
	if(%this.getImageState(0) $= "Armed") {
		if(isObject(%this.lankyInvalidTarget)) {
			%targetPosition = %this.lankyIsTargetValid(%this.lankyInvalidTarget);
			if(%targetPosition != false) {
				%this.lankyTarget = %this.lankyInvalidTarget;
				%this.lankyInvalidTarget = 0;

				if(!isObject(%this.lankyTarget) || %this.lankyTarget.getState() $= "Dead") {
					%this.lankyTarget = %this.lankyGetSpearTarget();
					%this.lankySpear();
					return;
				}
			}
			else {
				%targetPosition = %this.lankyIsTargetValid(%this.lankyTarget);		
			}
		}
		else {
			%targetPosition = %this.lankyIsTargetValid(%this.lankyTarget);
			if(%targetPosition == false) {
				%newTarget = %this.lankyGetSpearTarget(%this.lankyTarget);
				if(isObject(%newTarget)) {
					%this.lankyInvalidTarget = %this.lankyTarget;
					%this.lankyTarget = %newTarget;
					%this.lankySpear();
					return;
				}
			}
		}

		// https://bansheerubber.com/i/f/uBI3D.png, from wikipedia
		if(%targetPosition != false) {
			%speed = 50;
			%position = %this.getMuzzlePoint(0);
			
			%zAngle = calculateProjectileZAngle(%position, %targetPosition, %speed, 0.5);
			%yDifference = getWord(%targetPosition, 1) - getWord(%position, 1);
			%dot = vectorDot("1 0 0", vectorNormalize(vectorSub(getWords(%targetPosition, 0, 1), getWords(%position, 0, 1))));
			%xyAngle = mACos(%dot) * (%yDifference < 0 ? -1 : 1);
			%xDirection = mCos(%xyAngle) * mCos(%zAngle);
			%yDirection = mSin(%xyAngle) * mCos(%zAngle);
			%zDirection = mSin(%zAngle);

			new Projectile() {
				datablock = BeefboyGuardSpearProjectile;
				initialPosition = %position;
				initialVelocity = vectorScale(%xDirection SPC %yDirection SPC %zDirection, %speed);
				sourceObject = %this;
				sourceSlot = 0;
			};

			%this.lankyLastFire = getSimTime();
			%this.setImageTrigger(0, false);
		}
	}
	else {
		%this.setImageTrigger(0, true);
	}

	%this.aiSchedule = %this.schedule(100, lankySpear);
}