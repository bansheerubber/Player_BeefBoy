datablock ParticleData(BeefboyGuard2SkidParticle) {
	dragCoefficient      = 4;
	gravityCoefficient   = 1.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 400;
	lifetimeVarianceMS   = 200;
	spinSpeed       	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;

	textureName = "base/data/particles/cloud";

	colors[0] = "1.0 1.0 1.0 0.4";
	colors[1] = "0.9 0.9 0.9 0.4";
	colors[2] = "0.9 0.9 0.9 0.2";
	colors[3] = "0.5 0.5 0.5 0.0";

	sizes[0] = 0.25;
	sizes[1] = 1.0;
	sizes[2] = 2.0;
	sizes[3] = 0.5;

	times[0] = 0.0;
	times[1] = 0.05;
	times[2] = 0.5;
	times[3] = 0.9;

	useInvAlpha = true;
};

datablock ParticleEmitterData(BeefboyGuard2SkidEmitter) {
	ejectionPeriodMS = 15;
	periodVarianceMS = 0;
	ejectionVelocity = 7;
	velocityVariance = 0;
	ejectionOffset   = 0.1;
	thetaMin         = -180;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	orientOnVelocity = false;
	orientParticles = false;

	particles = BeefboyGuard2SkidParticle;
};

datablock ShapeBaseImageData(BeefboyGuard2SkidImage) {
	maxAmmo = 1;

	shapeFile = "base/data/shapes/empty.dts";
	emap = true;
	mountPoint = 20;
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
	stateEmitter[0]		= "BeefboyGuard2SkidEmitter";
	stateEmitterTime[0]	= 9999;
};