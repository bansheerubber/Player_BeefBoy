
datablock ParticleData(BeefboyGuardSwingParticle) {
	dragCoefficient      = 0;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 600;
	lifetimeVarianceMS   = 300;
	spinSpeed       	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;

	textureName		= "base/data/particles/ring.png";
	animateTexture	= true;
	framesPerSec	= 54;

	colors[0]     = "1.0 0.0 0.0 0.7";
	colors[1]     = "1.0 0.3 0.3 0.7";
	colors[2]     = "1.0 0.5 0.5 0.3";
	colors[3]     = "1.0 0.8 0.8 0.0";

	sizes[0]      = 0.8;
	sizes[1]      = 0.5;
	sizes[2]      = 0.3;
	sizes[3]      = 0.3;

	times[0] = 0.0;
	times[1] = 0.2;
	times[2] = 0.5;
	times[3] = 0.6;

	useInvAlpha = false;
};

datablock ParticleEmitterData(BeefboyGuardSwingEmitter) {
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0;
	ejectionOffset   = 0;
	thetaMin         = 0;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;

	orientOnVelocity = false;
	orientParticles = false;

	particles = BeefboyGuardSwingParticle;
};

datablock ParticleData(BeefboyGuardSpearHitParticle) {
	dragCoefficient      = 0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 166;
	lifetimeVarianceMS   = 0;
	spinSpeed       	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;

	textureName		= "Add-Ons/Player_Beefboy/textures/hit1.png";
	animTexName[0]	= "Add-Ons/Player_Beefboy/textures/hit1.png";
	animTexName[1]	= "Add-Ons/Player_Beefboy/textures/hit2.png";
	animTexName[2]	= "Add-Ons/Player_Beefboy/textures/hit3.png";
	animTexName[3]	= "Add-Ons/Player_Beefboy/textures/hit4.png";
	animTexName[4]	= "Add-Ons/Player_Beefboy/textures/hit5.png";
	animTexName[5]	= "Add-Ons/Player_Beefboy/textures/hit6.png";
	animTexName[6]	= "Add-Ons/Player_Beefboy/textures/hit7.png";
	animTexName[7]	= "Add-Ons/Player_Beefboy/textures/hit8.png";
	animTexName[8]	= "Add-Ons/Player_Beefboy/textures/hit9.png";
	animateTexture	= true;
	framesPerSec	= 54;

	colors[0]     = "1.0 0.3 0.3 0.7";
	colors[1]     = "1.0 0.3 0.3 0.7";
	colors[2]     = "1.0 0.5 0.5 0.7";
	colors[3]     = "1.0 0.8 0.8 0.7";

	sizes[0]      = 5.5;
	sizes[1]      = 5.5;
	sizes[2]      = 5.5;
	sizes[3]      = 5.5;

	times[0] = 0.0;
	times[1] = 0.25;
	times[2] = 0.5;
	times[3] = 0.75;

	useInvAlpha = false;
};

datablock ParticleEmitterData(BeefboyGuardSpearHitEmitter) {
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0;
	ejectionOffset   = 0;
	thetaMin         = 0;
	thetaMax         = 0;
	phiReferenceVel  = 0;
	phiVariance      = 0;
	overrideAdvance = false;

	particles = BeefboyGuardSpearHitParticle;
};

datablock ParticleData(BeefboyGuardSpearHit2Particle) {
	dragCoefficient      = 0;
	gravityCoefficient   = 1.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 800;
	lifetimeVarianceMS   = 800;
	spinSpeed       	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;

	textureName		= "base/data/particles/chunk.png";

	colors[0]     = "0.4 0.1 0.1 0.7";
	colors[1]     = "0.4 0.1 0.1 0.7";
	colors[2]     = "0.1 0.1 0.1 0.3";
	colors[3]     = "0.1 0.1 0.1 0.0";

	sizes[0]      = 1.0;
	sizes[1]      = 0.8;
	sizes[2]      = 0.7;
	sizes[3]      = 0.6;

	times[0] = 0.0;
	times[1] = 0.2;
	times[2] = 0.5;
	times[3] = 0.6;

	useInvAlpha = true;
};

datablock ParticleEmitterData(BeefboyGuardSpearHit2Emitter) {
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 10;
	velocityVariance = 5;
	ejectionOffset   = 0;
	thetaMin         = 0;
	thetaMax         = 60;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;

	orientOnVelocity = true;
	orientParticles = true;

	particles = BeefboyGuardSpearHit2Particle;
};

datablock ParticleData(BeefboyGuardSpearHit3Particle) {
	dragCoefficient      = 0;
	gravityCoefficient   = 1.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 800;
	lifetimeVarianceMS   = 800;
	spinSpeed       	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;

	textureName		= "Add-Ons/Player_Beefboy/textures/blastFlash2.png";

	colors[0]     = "0.4 0.1 0.1 0.7";
	colors[1]     = "0.4 0.1 0.1 0.7";
	colors[2]     = "0.1 0.1 0.1 0.3";
	colors[3]     = "0.1 0.1 0.1 0.0";

	sizes[0]      = 1.0;
	sizes[1]      = 0.8;
	sizes[2]      = 0.7;
	sizes[3]      = 0.6;

	times[0] = 0.0;
	times[1] = 0.2;
	times[2] = 0.5;
	times[3] = 0.6;

	useInvAlpha = true;
};

datablock ParticleEmitterData(BeefboyGuardSpearHit3Emitter) {
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 10;
	velocityVariance = 5;
	ejectionOffset   = 0;
	thetaMin         = 0;
	thetaMax         = 60;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;

	orientOnVelocity = true;
	orientParticles = true;

	particles = BeefboyGuardSpearHit3Particle;
};

datablock ParticleData(BeefboyGuardSpearHit4Particle) {
	dragCoefficient      = 0;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0;
	constantAcceleration = 0;
	lifetimeMS           = 200;
	lifetimeVarianceMS   = 200;
	spinSpeed       	 = 1;
	spinRandomMin        = -500;
	spinRandomMax        = 500;

	textureName		= "Add-Ons/Player_Beefboy/textures/blastFlash2.png";

	colors[0]     = "0.4 0.1 0.1 0.4";
	colors[1]     = "0.4 0.1 0.1 0.4";
	colors[2]     = "0.1 0.1 0.1 0.4";
	colors[3]     = "0.1 0.1 0.1 0.0";

	sizes[0]      = 7.0;
	sizes[1]      = 18.8;
	sizes[2]      = 7.7;
	sizes[3]      = 0.6;

	times[0] = 0.0;
	times[1] = 0.2;
	times[2] = 0.5;
	times[3] = 0.6;

	useInvAlpha = false;
};

datablock ParticleEmitterData(BeefboyGuardSpearHit4Emitter) {
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 0;
	velocityVariance = 0;
	ejectionOffset   = 0;
	thetaMin         = 0;
	thetaMax         = 180;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;

	orientOnVelocity = false;
	orientParticles = false;

	particles = BeefboyGuardSpearHit4Particle;
};

datablock ExplosionData(BeefboyGuardSpearExplosion) {
	particleEmitter = BeefboyGuardSpearHitEmitter;
	particleDensity = 1;
	particleRadius = 0;

	soundProfile = BeefboyGuardSpearHitSound;

	emitter[0] = BeefboyGuardSpearHit2Emitter;
	emitter[1] = BeefboyGuardSpearHit3Emitter;
	emitter[2] = "";

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = false;
	camShakeFreq = "2 2 2";
	camShakeAmp = "10 10 10";
	camShakeDuration = 0;
	camShakeRadius = 0;

	lightStartRadius 	= 0;
	lightEndRadius 		= 0;
	lightStartColor 	= "1 0.03 0.03 1";
	lightEndColor 		= "0 0 0 1";

	lifetimeMS = 100;
};

datablock ProjectileData(BeefboyGuardSpearProjectile) {
	projectileShapeName = "./shapes/lanky spear projectile2.dts";
	directDamage        = 50;
	impactImpulse	   = 1000;
	verticalImpulse	   = 2000;
	explosion           = BeefboyGuardSpearExplosion;
	particleEmitter     = BeefboyGuardSwingEmitter;

	muzzleVelocity      = 50;
	velInheritFactor    = 1;

	armingDelay         = 0;
	lifetime            = 20000;
	fadeDelay           = 19500;
	bounceElasticity    = 0;
	bounceFriction      = 0;
	isBallistic         = true;
	gravityMod = 0.50;

	hasLight    = false;
	lightRadius = 3.0;
	lightColor  = "0 0 0.5";
};