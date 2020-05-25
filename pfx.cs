datablock ParticleData(BeefboySwordHit2Particle) {
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

	colors[0]     = "0.7 0.1 0.1 0.7";
	colors[1]     = "0.7 0.1 0.1 0.7";
	colors[2]     = "0.3 0.1 0.1 0.3";
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

datablock ParticleEmitterData(BeefboySwordHit2Emitter) {
	ejectionPeriodMS = 3;
	periodVarianceMS = 0;
	ejectionVelocity = 10;
	velocityVariance = 5;
	ejectionOffset   = 2;
	thetaMin         = 0;
	thetaMax         = 35;
	phiReferenceVel  = 0;
	phiVariance      = 360;
	overrideAdvance = false;

	lifeTimeMS = 100;

	orientOnVelocity = false;
	orientParticles = false;

	particles = BeefboySwordHit2Particle;
};

datablock ParticleData(BeefboySwordHitParticle) {
	dragCoefficient      = 0;
	gravityCoefficient   = 0.0;
	inheritedVelFactor   = 0.0;
	constantAcceleration = 0.0;
	lifetimeMS           = 166;
	lifetimeVarianceMS   = 0;
	spinSpeed       	 = 0;
	spinRandomMin        = 0;
	spinRandomMax        = 0;

	textureName		= "./textures/hit1.png";
	animTexName[0]	= "./textures/hit1.png";
	animTexName[1]	= "./textures/hit2.png";
	animTexName[2]	= "./textures/hit3.png";
	animTexName[3]	= "./textures/hit4.png";
	animTexName[4]	= "./textures/hit5.png";
	animTexName[5]	= "./textures/hit6.png";
	animTexName[6]	= "./textures/hit7.png";
	animTexName[7]	= "./textures/hit8.png";
	animTexName[8]	= "./textures/hit9.png";
	animateTexture	= true;
	framesPerSec	= 54;

	colors[0]     = "2.0 0.0 0.0 0.7";
	colors[1]     = "2.0 0.0 0.0 0.7";
	colors[2]     = "2.0 0.0 0.0 0.7";
	colors[3]     = "2.0 0.0 0.0 0.7";

	sizes[0]      = 5.5;
	sizes[1]      = 5.5;
	sizes[2]      = 5.5;
	sizes[3]      = 5.5;

	times[0] = 0.0;
	times[1] = 0.25;
	times[2] = 0.5;
	times[3] = 0.75;

	useInvAlpha = true;
};

datablock ParticleEmitterData(BeefboySwordHitEmitter) {
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

	particles = BeefboySwordHitParticle;
};

datablock ExplosionData(BeefboySwordHitExplosion) {
	particleEmitter = BeefboySwordHitEmitter;
	particleDensity = 1;
	particleRadius = 0;

	emitter[0] = "BeefboySwordHit2Emitter";

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

datablock ProjectileData(BeefboySwordHitProjectile : gunProjectile) {
	explosion = BeefboySwordHitExplosion;
	uiName = "";
};

function testBeefboySwordProjectile() {
	(new Projectile() {
		datablock = BeefboySwordHitProjectile;
		initialPosition = "0 0 1";
		initialVelocity = "0 0 10";
		sourceObject = 0;
		sourceSlot = 0;
	}).explode();
}