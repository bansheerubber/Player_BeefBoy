datablock ParticleData(BeefboyGuard2ChargeParticle) {
	dragCoefficient      = 4;
	gravityCoefficient   = -1.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 400;
	lifetimeVarianceMS   = 200;
	spinSpeed       	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;

	textureName = "Add-Ons/Player_Beefboy/textures/trail";

	colors[0] = "1.0 0.0 0.0 0.3";
	colors[1] = "1.0 0.0 0.0 0.3";
	colors[2] = "1.0 0.3 0.3 0.2";
	colors[3] = "1.0 0.4 0.4 0.0";

	sizes[0] = 3;
	sizes[1] = 3;
	sizes[2] = 3;
	sizes[3] = 3;

	times[0] = 0.0;
	times[1] = 0.05;
	times[2] = 0.5;
	times[3] = 0.9;

	useInvAlpha = false;
};

datablock ParticleEmitterData(BeefboyGuard2ChargeEmitter) {
	ejectionPeriodMS = 15;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0;
	ejectionOffset   = 0;
	thetaMin         = 0;
	thetaMax         = 0;
	phiReferenceVel  = 0;
	phiVariance      = 0;
	overrideAdvance = false;
	orientOnVelocity = false;
	orientParticles = false;

	particles = BeefboyGuard2ChargeParticle;
};

datablock ShapeBaseImageData(BeefboyGuard2ChargeImage) {
	shapeFile = "base/data/shapes/empty.dts";
	emap = true;
	mountPoint = 11;
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
	stateEmitter[0]		= "BeefboyGuard2ChargeEmitter";
	stateEmitterTime[0]	= 9999;
};