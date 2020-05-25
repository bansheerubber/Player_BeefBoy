datablock StaticShapeData(BeefBoySwordStatic) {
	shapeFile = "./shapes/megaknightsword2.dts";
};

datablock ItemData(BeefBoySwordItem) {
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./shapes/megaknightsword2.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "Beef Boy Sword";
	iconName = "./";
	doColorShift = false;

	image = BeefBoySwordImage;
	canDrop = true;
};

datablock ShapeBaseImageData(BeefBoySwordImage) {
	shapeFile = "./shapes/megaknightsword2.dts";
	emap = true;
	mountPoint = 0;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix("0 0 0");
	correctMuzzleVector = true;
	className = "WeaponImage";
	item = BeefBoySwordItem;

	melee = false;
	armReady = false;

	doColorShift = false;
	
	swordStartMount[0] 					= 11; // start point for raycast/interpolation
	swordEndMount[0] 					= 8; // end point for raycast/interpolation
	swordStepInterpolationCount			= 0;
	swordStepInterpolationDistance 		= 0.5; // how many linear interpolations we do between steps, based on distance
	swordStepTick						= 33; // how fast we do sword stepping

	swordInterpolationDistance[0, 0]	= 0.1;
	swordInterpolationRadius[0, 0]		= 1.2;
	swordInterpolationDistance[0, 1]	= 0.3;
	swordInterpolationRadius[0, 1]		= 1.2;
	swordInterpolationDistance[0, 2]	= 0.5;
	swordInterpolationRadius[0, 2]		= 1.2;
	swordInterpolationDistance[0, 3]	= 0.7;
	swordInterpolationRadius[0, 3]		= 1.2;
	swordInterpolationDistance[0, 4]	= 1;
	swordInterpolationRadius[0, 4]		= 3;

	swordStopSwingOnBrickHit			= false; // whether or not we want to stop our sword swing when we hit a brick wall (and setImageLoaded to false)

	stateName[0]					= "Activate";
	stateSound[0]					= WeaponSwitchSound;
	stateTimeoutValue[0]			= 0.5;
	stateTransitionOnTimeout[0]		= "Idle";
	stateSequence[0]				= "root";
	
	stateName[1]					= "Idle";
	stateTransitionOnTriggerDown[1]	= "Fire";

	stateName[2]					= "Fire";
	stateScript[2]					= "onFire";
	stateTimeoutValue[2]			= 35 / 24;
	stateTransitionOnTimeout[2]		= "Idle";
	stateSequence[2]				= "wideSlash";
};

function BeefBoySwordImage::onMount(%this, %obj, %slot) {
	Parent::onMount(%this, %obj, %slot);
	%obj.playThread(1, "root");
	%Obj.beefboyInitSword();
}

function BeefBoySwordImage::onUnMount(%this, %obj, %slot) {
	Parent::onUnMount(%this, %obj, %slot);
	%obj.playThread(0, "root");
}

function BeefBoySwordImage::onFire(%this, %obj, %slot) {
	%obj.beefboyWideSlash();
}

function BeefboySwordImage::onSwingHit(%this, %obj, %slot, %col, %tangent, %isRadius, %index, %radiusIndex) {
	if(%col.getType() & $TypeMasks::PlayerObjectType) {
		// %velocity = vectorAdd(vectorScale(%obj.getForwardVector(), 25), "0 0 10");

		%tangent = vectorNormalize(getWords(%tangent, 0, 1));
		%col.progressiveKnockback(vectorScale(%tangent, 35), "35 8", 700);
		%col.damage(%obj, %col.getPosition(), 75, 3);

		serverPlay3d("BeefBoySwordHit" @ getRandom(1, 2) @ "Sound", %col.getPosition());

		(%p = new Projectile() {
			datablock = BeefboySwordHitProjectile;
			initialPosition = %col.getHackPosition();
			initialVelocity = vectorScale(%tangent, 10);
			sourceObject = 0;
			sourceSlot = 0;
		}).explode();
	}
}

function Player::beefboyNebenhut(%this) {
	%success = %this.beefboyUpSword();

	if(!%success) {
		%this.beefboyNebenhut2();
	}
	else {
		%this.schedule(400, beefboyNebenhut2);
	}
}

function Player::beefboyNebenhut2(%this) {
	%this.unMountImageSafe(1);
	%this.stopAudio(1);
	%this.playThread(2, "root");
	%this.beefboySwordStatus = "";
	
	%this.playThread(0, "nebenhut");
	%this.playAudio(0, BeefBoyDropSwordSound);
	%this.playAudio(2, BeefBoyArmorMove1Sound);

	%this.schedule(23 / 24 * 1000, playAudio, 1, BeefBoyDragSwordSound);
	%this.schedule(23 / 24 * 1000, mountImage, BeefboySwordDragImage, 1);
}

function Player::beefboyWideSlash(%this) {
	%this.unMountImageSafe(1);
	%this.stopAudio(1);
	%this.playThread(2, "root");
	%this.beefboySwordStatus = "";

	%this.playThread(0, "wideSlash");
	%this.playAudio(0, BeefBoySwordReadySound);
	%this.schedule(500, playAudio, 1, BeefBoySwordSwingSound);
	%this.schedule(450, beefboyMountMagicSwing);
	%this.schedule(600, beefboyDeleteMagicSwing);
	%this.getMountedImage(0).schedule(500, startSwing, %this, 0);
	%this.getMountedImage(0).schedule(666, endSwing, %this, 0);
	%this.schedule(900, playAudio, 0, BeefBoySwordFinishSound);
}

function Player::beefboyInitSword(%this) {
	if(!isObject(%this.swordMount[11]) && !isObject(%this.swordMount[8])) {
		%this.swordMount[11] = %mountBase = createMountPoint(); // mount 11
		%this.swordMount[8] = %mountTip = createMountPoint(); // mount 8
		%this.mountObject(%mountBase, 11);
		%this.mountObject(%mountTip, 8);
	}
}

function Player::beefboyLeftSword(%this) {
	%this.playThread(0, "root");
	switch$(%this.beefboySwordStatus) {
		case "fumble2" or "fumble6":
			%this.playThread(2, "fumble3");
			%this.beefboySwordStatus = "fumble3";
		
		case "fumble1" or "fumble3":
			return;
		
		default:
			%this.playThread(2, "fumble1");
			%this.beefboySwordStatus = "fumble1";
	}
}

function Player::beefboyRightSword(%this) {
	%this.playThread(0, "root");
	switch$(%this.beefboySwordStatus) {
		case "fumble1" or "fumble3":
			%this.playThread(2, "fumble6");
			%this.beefboySwordStatus = "fumble6";
		
		case "fumble2" or "fumble6":
			return;
		
		default:
			%this.playThread(2, "fumble2");
			%this.beefboySwordStatus = "fumble2";
	}
}

function Player::beefboyToggleSword(%this) {
	%this.playThread(0, "root");
	switch$(%this.beefboySwordStatus) {
		case "fumble1" or "fumble3": // go from left to right
			%this.playThread(2, "fumble6");
			%this.beefboySwordStatus = "fumble6";
			return;
		
		case "fumble2" or "fumble6": // go from right to left
			%this.playThread(2, "fumble3");
			%this.beefboySwordStatus = "fumble3";
			return;
	}
}

function Player::beefboyUpSword(%this) {
	%this.playThread(0, "root");
	switch$(%this.beefboySwordStatus) {
		case "fumble3" or "fumble1":
			%this.playThread(2, "fumble4");
			%this.beefboySwordStatus = "fumble4";
			return true;
		
		case "fumble2" or "fumble6":
			%this.playThread(2, "fumble5");
			%this.beefboySwordStatus = "fumble5";
			return true;
	}
	return false;
}

function createSwordInStone(%position) {
	%static = new StaticShape() {
		datablock = BeefBoySwordStatic;
		position = %position;
		rotation = "0 0 0 1";
	};
	%static.setTransform(%position SPC eulerToAxis("0 180 0"));
	return %static;
}