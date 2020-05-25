datablock StaticShapeData(BeefboyGuard2ShieldStatic) {
	shapeFile = "./shapes/bull shield col.dts";
};

function AiPlayer::bullInitShield(%this) {
	if(!isObject(%this.swordMount[9]) && !isObject(%this.swordMount[10])) {
		%this.swordMount[9] = createMountPoint(); // mount 9
		%this.swordMount[10] = createMountPoint(); // mount 10
		%this.mountObject(%this.swordMount[9], 9);
		%this.mountObject(%this.swordMount[10], 10);
	}
}

function AiPlayer::bullGetShieldPosisiton(%this) {
	return vectorAdd(%this.swordMount[9].getPosition(), vectorScale(%this.bullGetShieldNormal(), 0.5));
}

function AiPlayer::bullGetShieldNormal(%this) {
	return vectorNormalize(vectorSub(%this.swordMount[10].getPosition(), %this.swordMount[9].getPosition()));
}

function AiPlayer::bullTrackShield(%this) {
	if(!isObject(%this.bullShield)) {
		%this.bullShield = new StaticShape() {
			datablock = BeefboyGuard2ShieldStatic;
			position = "0 0 -100";
			rotation = "0 0 0 1";
			bullShieldOwner = %this;
		};
		%this.bullInitShield();
	}

	%euler = vectorToEuler(%this.bullGetShieldNormal());
	%this.bullShield.setTransform(%this.bullGetShieldPosisiton() SPC eulerToAxis(%euler));
	
	%this.bullShieldSchedule = %this.schedule(33, bullTrackShield);
}

deActivatePackage(BeefboyGuard2ShieldPackage);
package BeefboyGuard2ShieldPackage {
	function Armor::onRemove(%this, %obj) {
		if(isObject(%obj.bullShield)) {
			%obj.bullShield.delete();
		}
		
		Parent::onRemove(%this, %obj);
	}

	function AiPlayer::playDeathCry(%this) {
		if(isObject(%this.bullShield)) {
			%this.bullShield.delete();
		}
		
		Parent::playDeathCry(%this);
	}

	function ProjectileData::onCollision(%this, %obj, %col, %fade, %pos, %normal) {
		Parent::onCollision(%this, %obj, %col, %fade, %pos, %normal);

		if(isObject(%col.bullShieldOwner)) {
			%col.bullShieldOwner.playThread(2, "plant");
			serverPlay3d(BeefboyGuard2ShieldSound, %pos);
			%col.bullShieldOwner.applyImpulse(%pos, vectorScale(vectorNormalize(%obj.getVelocity()), %this.impactImpulse * 0.5));
			%col.bullShieldOwner.applyImpulse(%pos, vectorScale("0 0 1", %this.verticalImpulse * 0.5));
		}
	}
};
activatePackage(BeefboyGuard2ShieldPackage);