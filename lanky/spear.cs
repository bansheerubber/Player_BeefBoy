datablock ItemData(BeefboyGuardSpearItem) {
	category = "Weapon";
	className = "Weapon";

	shapeFile = "./shapes/lanky spear.dts";
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	uiName = "Beef Boy Guard Spear";
	iconName = "./icon_spear";
	doColorShift = true;
	colorShiftColor = "1 1 1 1";

	image = BeefboyGuardSpearImage;
	canDrop = true;
};

datablock ShapeBaseImageData(BeefboyGuardSpearImage) {
	shapeFile = "./shapes/lanky spear.dts";
	emap = true;

	mountPoint = 0;
	offset = "0 0 0";
	correctMuzzleVector = true;

	className = "WeaponImage";

	item = BeefboyGuardSpearItem;
	ammo = " ";
	projectile = spearProjectile;
	projectileType = Projectile;

	melee = false;
	armReady = true;

	doColorShift = true;
	colorShiftColor = BeefboyGuardSpearItem.colorShiftColor;

	stateName[0]					= "Activate";
	stateTimeoutValue[0]			= 0.1;
	stateTransitionOnTimeout[0]		= "Ready";
	stateSequence[0]				= "ready";
	stateSound[0]					= weaponSwitchSound;

	stateName[1]					= "Ready";
	stateTransitionOnTriggerDown[1]	= "Charge";
	stateAllowImageChange[1]		= true;

	stateName[2]					= "Charge";
	stateTransitionOnTimeout[2]		= "Armed";
	stateTimeoutValue[2]			= 0.7;
	stateWaitForTimeout[2]			= false;
	stateTransitionOnTriggerUp[2]	= "AbortCharge";
	stateScript[2]					= "onCharge";
	stateAllowImageChange[2]		= false;

	stateName[3]					= "AbortCharge";
	stateTransitionOnTimeout[3]		= "Ready";
	stateTimeoutValue[3]			= 0.3;
	stateWaitForTimeout[3]			= true;
	stateScript[3]					= "onAbortCharge";
	stateAllowImageChange[3]		= false;

	stateName[4]					= "Armed";
	stateTransitionOnTriggerUp[4]	= "Fire";
	stateAllowImageChange[4]		= false;

	stateName[5]					= "Fire";
	stateTransitionOnTimeout[5]		= "Ready";
	stateTimeoutValue[5]			= 0.5;
	stateFire[5]					= true;
	stateSequence[5]				= "fire";
	stateScript[5]					= "onFire";
	stateWaitForTimeout[5]			= true;
	stateAllowImageChange[5]		= false;
	stateSound[5]					= spearFireSound;
};

function BeefboyGuardSpearImage::onCharge(%this, %obj, %slot) {
	%obj.playThread(1, spearReady);
}

function BeefboyGuardSpearImage::onAbortCharge(%this, %obj, %slot) {
	%obj.playThread(1, root);
}

function BeefboyGuardSpearImage::onFire(%this, %obj, %slot) {
	%obj.playThread(1, spearThrow);

	if(%obj.lankyState $= "") {
		new Projectile() {
			datablock = BeefboyGuardSpearProjectile;
			initialPosition = %obj.getMuzzlePoint(0);
			initialVelocity = vectorScale(%obj.getMuzzleVector(0), 50);
			sourceObject = %obj;
			sourceSlot = 0;
			client = %obj.client;
		};
	}
}

function Player::getClosestTarget(%this) {
	initContainerRadiusSearch(%this.getPosition(), 500, $TypeMasks::PlayerObjectType);
	while(%col = containerSearchNext()) {
		if(%col != %this) {
			return %col;
		}
	}
}

function Player::test(%this) {
	%this.testFireSpear(%this.getClosestTarget());
	%this.testSchedule = %this.schedule(300, test);
}

function spawnExplosion(%position) {
	(new Projectile() {
		datablock = BeefboyGuardSpearProjectile;
		initialPosition = %position;
		initialVelocity = "0 0 10";
		sourceObject = 0;
		sourceSlot = 0;
	}).explode();
}

function Player::testFireSpear(%this, %targetObject) {
	%speed = 50;
	%position = %this.getMuzzlePoint(0);
	%targetPosition = %targetObject.getHackPosition();

	%time = calculateProjectileFlightTime(%position, %targetPosition, %speed, 0.5);
	%targetPosition = %targetObject.getPredictedPosition(%time);
	
	schedule(%time * 1000, 0, spawnExplosion, %targetPosition);
	drawDebugSphere(%targetPosition, 0.2, "1 0 0 1", 500);

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
		client = %this.client;
		start = getSimTime();
	};
}

function Player::calculateAcceleration(%this, %test) {
	%timeCoefficient = 1000 / (getSimTime() - %this.lastVelocityRead);
	%this.acceleration = vectorScale(vectorSub(%this.getVelocity(), %this.lastVelocity), %timeCoefficient);
	%this.jerk = vectorScale(vectorSub(%this.acceleration, %this.lastAcceleration), %timeCoefficient);
	%this.jounce = vectorScale(vectorSub(%this.jerk, %this.lastJerk), %timeCoefficient);

	%this.lastVelocityRead = getSimTime();
	%this.lastVelocity = %this.getVelocity();
	%this.lastAcceleration = %this.acceleration;
	%this.lastJerk = %this.jerk;
	%this.accelerationTick++;

	%this.calculateAccelerationSchedule = %this.schedule(33, calculateAcceleration, %this.accelerationTick);
}

function Player::getPredictedPosition(%this, %time) {
	%linear = vectorScale(%this.getVelocity(), %time);
	%quadratic = vectorScale(%this.acceleration, mPow(%time, 2) * 0.5);

	if(vectorLen(%this.jerk) < 100) {
		%quitdratic = vectorScale(%this.jerk, mPow(%time, 3) / 6);
		%position = vectorAdd(%this.getHackPosition(), vectorAdd(%quintdratic, vectorAdd(%quadratic, %linear)));
		return %position;
	}
	else {
		%position = vectorAdd(%this.getHackPosition(), %linear);
		return %position;
	}
}

deActivatePackage(SpearPrediction);
package SpearPrediction {
	function Armor::onAdd(%this, %obj) {
		Parent::onAdd(%this, %obj);
		%obj.calculateAcceleration();
	}
};
activatePackage(SpearPrediction);