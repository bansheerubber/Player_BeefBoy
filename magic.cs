datablock ParticleData(BeefboySwordMagicSwingParticle) {
	dragCoefficient      = 0;
	gravityCoefficient   = 1.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 600;
	lifetimeVarianceMS   = 300;
	spinSpeed       	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;

	textureName		= "base/data/particles/dot.png";
	animateTexture	= true;
	framesPerSec	= 54;

	colors[0]     = "0.3 0.1 0.9 0.7";
	colors[1]     = "0.3 0.1 0.9 0.7";
	colors[2]     = "0.3 0.1 0.9 0.3";
	colors[3]     = "0.3 0.1 0.9 0.0";

	sizes[0]      = 1.8;
	sizes[1]      = 1.5;
	sizes[2]      = 0.3;
	sizes[3]      = 0.3;

	times[0] = 0.0;
	times[1] = 0.2;
	times[2] = 0.5;
	times[3] = 0.6;

	useInvAlpha = false;
};

datablock ParticleEmitterData(BeefboySwordMagicSwingEmitter) {
	ejectionPeriodMS = 3;
	periodVarianceMS = 0;
	ejectionVelocity = 1;
	velocityVariance = 0;
	ejectionOffset   = 0.2;
	thetaMin         = 0;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;

	orientOnVelocity = false;
	orientParticles = false;

	particles = BeefboySwordMagicSwingParticle;
};

datablock ShapeBaseImageData(BeefboySwordMagicSwingImage) {
	maxAmmo = 1;

	shapeFile = "base/data/shapes/empty.dts";
	emap = true;
	mountPoint = 10;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix("0 0 0");
	correctMuzzleVector = true;
	className = "WeaponImage";
	item = BeefBoyBeefboySwordItem;

	melee = false;
	armReady = true;

	doColorShift = false;

	stateName[0]		= "Activate";
	stateEmitter[0]		= "BeefboySwordMagicSwingEmitter";
	stateEmitterTime[0]	= 9999;
};

function Player::beefboyMountMagicSwing(%this) {
	%this.beefboyDeleteMagicSwing();

	for(%i = 12; %i <= 15; %i++) {
		%this.swordMount[%i] = createMountPoint();
		%this.mountObject(%this.swordMount[%i], %i);
		%this.swordMount[%i].mountImage(BeefboySwordMagicSwingImage, 0);
	}
}

function Player::beefboyDeleteMagicSwing(%this) {
	for(%i = 12; %i <= 15; %i++) {
		if(isObject(%this.swordMount[%i])) {
			%this.swordMount[%i].delete();
		}
	}
}