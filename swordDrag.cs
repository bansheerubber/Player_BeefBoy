datablock ParticleData(BeefboySwordDragParticle) {
	dragCoefficient      = 4;
	gravityCoefficient   = -1.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 1800;
	lifetimeVarianceMS   = 600;
	spinSpeed       	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;

	textureName = "base/data/particles/cloud";

	colors[0] = "1.0 1.0 1.0 0.2";
	colors[1] = "0.9 0.9 0.9 0.2";
	colors[2] = "0.5 0.5 0.5 0.1";
	colors[3] = "0.3 0.3 0.3 0.0";

	sizes[0] = 0.25;
	sizes[1] = 2.0;
	sizes[2] = 4.0;
	sizes[3] = 4.0;

	times[0] = 0.0;
	times[1] = 0.05;
	times[2] = 0.5;
	times[3] = 0.9;

	useInvAlpha = true;
};

datablock ParticleEmitterData(BeefboySwordDragEmitter) {
	ejectionPeriodMS = 25;
	periodVarianceMS = 0;
	ejectionVelocity = 2;
	velocityVariance = 2;
	ejectionOffset   = 0.5;
	thetaMin         = -180;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;
	orientOnVelocity = false;
	orientParticles = false;

	particles = BeefboySwordDragParticle;
};

datablock ShapeBaseImageData(BeefboySwordDragImage) {
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
	stateEmitter[0]		= "BeefboySwordDragEmitter";
	stateEmitterTime[0]	= 9999;
};