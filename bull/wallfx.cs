datablock ParticleData(BeefboyGuard2Collision1Particle) {
	dragCoefficient      = 4;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0;
	constantAcceleration = 1;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 100;
	textureName          = "base/data/particles/star1";
	spinSpeed			 = 0;
	spinRandomMin		 = -5000;
	spinRandomMax		 = 5000;

	colors[0] = "1.0 1.0 0 1.0";
	colors[1] = "1.0 1.0 0 0.7";
	colors[2] = "1.0 1.0 0 0.1";
	colors[3] = "1.0 1.0 0 0.0";

	sizes[0] = 5;
	sizes[1] = 2;
	sizes[2] = 0.3;
	sizes[3] = 0;

	times[0] = 0.0;
	times[1] = 0.1;
	times[2] = 0.7;
	times[3] = 1.0;

	useInvAlpha = true;
};

datablock ParticleEmitterData(BeefboyGuard2Collision1Emitter) {
	ejectionPeriodMS = 15;
	periodVarianceMS = 1;
	ejectionVelocity = 10;
	velocityVariance = 10;
	ejectionOffset   = 0.5;
	thetaMin         = 0;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	particles = BeefboyGuard2Collision1Particle;
};

datablock ParticleData(BeefboyGuard2Collision2Particle) {
	dragCoefficient      = 7;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0;
	constantAcceleration = 0;
	lifetimeMS           = 1300;
	lifetimeVarianceMS   = 300;
	textureName          = "base/data/particles/cloud";
	spinSpeed			 = 0;
	spinRandomMin		 = -100;
	spinRandomMax		 = 100;

	colors[0] = "0.7 0.7 0.7 0.6";
	colors[1] = "0.7 0.7 0.7 0.3";
	colors[2] = "0.7 0.7 0.7 0.1";
	colors[3] = "0.7 0.7 0.7 0.0";

	sizes[0] = 0.5;
	sizes[1] = 2;
	sizes[2] = 1;
	sizes[3] = 3;

	times[0] = 0.0;
	times[1] = 0.1;
	times[2] = 0.7;
	times[3] = 1.0;

	useInvAlpha = false;
};

datablock ParticleEmitterData(BeefboyGuard2Collision2Emitter) {
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 20;
	velocityVariance = 5;
	ejectionOffset   = 0;
	thetaMin         = 80;
	thetaMax         = 85;
	phiReferenceVel  = 10000;
	phiVariance      = 360;
	particles = BeefboyGuard2Collision2Particle;
};

datablock ParticleData(BeefboyGuard2Collision3Particle) {
	dragCoefficient      = 0;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0;
	constantAcceleration = 0;
	lifetimeMS           = 800;
	lifetimeVarianceMS   = 0;
	textureName          = "Add-Ons/Player_BeefBoy/textures/blastflash2";
	spinSpeed			 = 0;
	spinRandomMin		 = -1500;
	spinRandomMax		 = 1500;

	colors[0] = "1.0 1.0 1.0 1.0";
	colors[1] = "1.0 0.3 0.1 0.7";
	colors[2] = "1.0 0.3 0.1 0.4";
	colors[3] = "1.0 0.3 0.1 0.0";

	sizes[0] = 15;
	sizes[1] = 15;
	sizes[2] = 2;
	sizes[3] = 0;

	times[0] = 0.0;
	times[1] = 0.2;
	times[2] = 0.7;
	times[3] = 1.0;

	useInvAlpha = false;
};

datablock ParticleEmitterData(BeefboyGuard2Collision3Emitter) {
	ejectionPeriodMS = 20;
	periodVarianceMS = 10;
	ejectionVelocity = 0;
	velocityVariance = 0;
	ejectionOffset   = 0.5;
	thetaMin         = 0;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	particles = BeefboyGuard2Collision3Particle;
};

datablock ExplosionData(BeefboyGuard2CollisionExplosion) {
	emitter[0] = BeefboyGuard2Collision1Emitter;
	emitter[1] = BeefboyGuard2Collision2Emitter;
	emitter[2] = BeefboyGuard2Collision3Emitter;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "6 6 6";
	camShakeAmp = "4 4 4";
	camShakeDuration = 1.5;
	camShakeRadius = 35;

	lightStartRadius 	= 25;
	lightEndRadius 		= 0;
	lightStartColor 	= "1 1 0.1 1";
	lightEndColor 		= "1 1 0.1 1";

	lifetimeMS = 100;

	// soundProfile = BeefboyGuard2CollisionSound;
};

datablock ProjectileData(BeefboyGuard2CollisionProjectile : GunProjectile) {
	explosion = BeefboyGuard2CollisionExplosion;
	uiName = "";
};