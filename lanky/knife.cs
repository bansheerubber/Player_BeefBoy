datablock ItemData(LankyKnifeItem) {
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./shapes/lanky knife.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "Beef Boy Guard Knife";
	iconName = "./";
	doColorShift = false;

	image = LankyKnifeImage;
	canDrop = true;
};

datablock ShapeBaseImageData(LankyKnifeImage) {
	shapeFile = "./shapes/lanky knife.dts";
	emap = true;
	mountPoint = 1;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix("0 0 0");
	correctMuzzleVector = true;
	className = "WeaponImage";
	item = LankyKnifeItem;

	melee = false;
	armReady = false;

	doColorShift = false;
	
	swordStartMount[0] 					= 1; // start point for raycast/interpolation
	swordEndMount[0] 					= 8; // end point for raycast/interpolation
	swordStepInterpolationCount 		= 3; // how many linear interpolations we do between steps, based on distance
	swordStepTick						= 33; // how fast we do sword stepping

	swordInterpolationDistance[0, 0]	= 1;
	swordInterpolationRadius[0, 0]		= 1.2;

	swordStopSwingOnBrickHit			= false; // whether or not we want to stop our sword swing when we hit a brick wall (and setImageLoaded to false)

	stateName[0]					= "Activate";
	stateSound[0]					= WeaponSwitchSound;
	stateTimeoutValue[0]			= 0.01;
	stateTransitionOnTimeout[0]		= "Idle";
	stateSequence[0]				= "root";
	
	stateName[1]					= "Idle";
	stateTransitionOnTriggerDown[1]	= "Fire";

	stateName[2]					= "Fire";
	stateScript[2]					= "onFire";
	stateTimeoutValue[2]			= 35 / 26;
	stateTransitionOnTimeout[2]		= "Done";
	stateSequence[2]				= "wideSlash";

	stateName[3]					= "Done";
	stateScript[3]					= "onDone";
};

function LankyKnifeImage::onMount(%this, %obj, %slot) {
	Parent::onMount(%this, %obj, %slot);
	%obj.lankyInitKnife();
}

function LankyKnifeImage::onFire(%this, %obj, %slot) {
	%this.startSwing(%obj, %slot);
	%this.schedule(33 * 4, endSwing, %obj, %slot);
	%obj.playThread(0, "knifeSwing");
	%obj.playAudio(0, BeefboyGuardKnifeSwingSound);
	%obj.schedule(25 / 26 * 1000, playAudio, 0, BeefboyGuardKnifePutAwaySound);
}

function LankyKnifeImage::onDone(%this, %obj, %slot) {
	%obj.unMountImageSafe(1);
	%obj.lankySetState(0);
}

function Player::lankyInitKnife(%this) {
	if(!isObject(%this.swordMount[8]) && !isObject(%this.swordMount[1])) {
		%this.swordMount[8] = %mountBase = createMountPoint(); // mount 8
		%this.swordMount[1] = %mountTip = createMountPoint(); // mount 1
		%this.mountObject(%mountBase, 8);
		%this.mountObject(%mountTip, 1);
	}
}

function LankyKnifeImage::onSwingHit(%this, %obj, %slot, %col, %tangent, %isRadius, %index, %radiusIndex) {
	if(%col.getType() & $TypeMasks::PlayerObjectType) {
		%velocity = vectorScale(%obj.getForwardVector(), 15);
		%col.progressiveKnockback(%velocity, "15 5", 400);
		%col.damage(%obj, %col.getPosition(), 50, 3);

		serverPlay3d(BeefboyGuard2PikeContactSound, %col.getPosition());

		(%p = new Projectile() {
			datablock = BeefboySwordHitProjectile;
			initialPosition = %col.getHackPosition();
			initialVelocity = %velocity;
			sourceObject = 0;
			sourceSlot = 0;
		}).explode();
	}
}