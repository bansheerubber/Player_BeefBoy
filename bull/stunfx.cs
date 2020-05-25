datablock ParticleData(BeefboyGuard2StunParticle) {
	dragCoefficient      = 4;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 400;
	lifetimeVarianceMS   = 400;
	spinSpeed       	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;

	textureName = "base/data/particles/star1";

	colors[0] = "1.0 1.0 0.0 0.7";
	colors[1] = "1.0 1.0 0.0 0.7";
	colors[2] = "1.0 1.0 0.0 0.7";
	colors[3] = "0.5 0.5 0.0 0.0";

	sizes[0] = 0.8;
	sizes[1] = 0.8;
	sizes[2] = 0.8;
	sizes[3] = 0.8;

	times[0] = 0.0;
	times[1] = 0.05;
	times[2] = 0.5;
	times[3] = 0.9;

	useInvAlpha = true;
};

datablock ParticleEmitterData(BeefboyGuard2StunEmitter) {
	ejectionPeriodMS = 75;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 2;
	ejectionOffset   = 1;
	thetaMin         = -180;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	orientOnVelocity = false;
	orientParticles = false;

	particles = BeefboyGuard2StunParticle;
};

datablock ShapeBaseImageData(BeefboyGuard2StunImage) {
	maxAmmo = 1;

	shapeFile = "base/data/shapes/empty.dts";
	emap = true;
	mountPoint = 5;
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
	stateEmitter[0]		= "BeefboyGuard2StunEmitter";
	stateEmitterTime[0]	= 9999;
};