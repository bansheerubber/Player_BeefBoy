datablock ParticleData(BeefboyRunesParticle) {
	dragCoefficient      = 0;
	gravityCoefficient   = -1.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 800;
	lifetimeVarianceMS   = 600;
	spinSpeed       	 = 0;
	spinRandomMin        = -1000;
	spinRandomMax        = 1000;

	textureName		= "Add-Ons/Projectile_Radio_Wave/bolt";
	animateTexture	= true;
	framesPerSec	= 54;

	colors[0]     = "0.8 0.5 1.0 1.0";
	colors[1]     = "0.3 0.1 0.9 0.7";
	colors[2]     = "0.3 0.1 0.9 0.7";
	colors[3]     = "0.3 0.1 0.9 0.0";

	sizes[0]      = 5.0;
	sizes[1]      = 1.5;
	sizes[2]      = 0.3;
	sizes[3]      = 0.3;

	times[0] = 0.0;
	times[1] = 0.1;
	times[2] = 0.5;
	times[3] = 0.8;

	useInvAlpha = false;
};

datablock ParticleEmitterData(BeefboyRunesEmitter) {
	ejectionPeriodMS = 150;
	periodVarianceMS = 0;
	ejectionVelocity = 1;
	velocityVariance = 0;
	ejectionOffset   = 3;
	thetaMin         = 0;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;

	orientOnVelocity = false;
	orientParticles = false;

	particles = BeefboyRunesParticle;
};

datablock ShapeBaseImageData(BeefboyRunesImage) {
	maxAmmo = 1;

	shapeFile = "base/data/shapes/empty.dts";
	emap = true;
	mountPoint = 16;
	offset = "0 0 0";
	eyeOffset = "0 0 0";
	rotation = eulerToMatrix("0 0 0");
	correctMuzzleVector = true;
	className = "WeaponImage";
	item = BeefBoyBeefboySwordItem;

	melee = false;
	armReady = false;

	doColorShift = false;

	stateName[0]		= "Activate";
	stateEmitter[0]		= "BeefboyRunesEmitter";
	stateEmitterTime[0]	= 9999;
};

function BeefboyRunesImage::onUnMount(%this, %obj, %slot) {

}