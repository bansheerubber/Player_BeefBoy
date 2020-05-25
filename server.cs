exec("./botDamage.cs");
exec("./projectileDetect.cs");
exec("./isGrounded.cs");

exec("./sounds.cs");
exec("./player.cs");
exec("./sword.cs");
exec("./pfx.cs");
exec("./swordDrag.cs");
exec("./magic.cs");
exec("./runes.cs");

exec("./lanky/sounds.cs");
exec("./lanky/player.cs");
exec("./lanky/knife.cs");
exec("./lanky/spearProjectile.cs");
exec("./lanky/spear.cs");
exec("./lanky/ai.cs");

exec("./bull/sounds.cs");
exec("./bull/skidfx.cs");
exec("./bull/stunfx.cs");
exec("./bull/player.cs");
exec("./bull/pike.cs");
exec("./bull/ai.cs");
exec("./bull/shield.cs");
exec("./bull/wallfx.cs");
exec("./bull/chargefx.cs");

setLogMode(1);

datablock StaticShapeData(VisibilityStatic) {
	shapeFile = "./visibility test3.dts";
};

datablock StaticShapeData(LightStatic) {
	shapeFile = "./light test3.dts";
};

datablock ExplosionData(BeefBoyTalkExplosion) {
	explosionShape = "";

	lifeTimeMS = 150;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "0.2 0.2 0.2";
	camShakeAmp = "0.4 0.4 0.9";
	camShakeDuration = 1;
	camShakeRadius = 100.0;
};

datablock ProjectileData(BeefBoyTalkProjectile) {
	lifetime = 10;
	fadeDelay = 10;
	explodeondeath = true;
	explosion = BeefBoyTalkExplosion;
};

datablock ExplosionData(BeefBoyLandExplosion) {
	explosionShape = "";

	lifeTimeMS = 500;

	faceViewer     = true;
	explosionScale = "1 1 1";

	shakeCamera = true;
	camShakeFreq = "0.4 0.4 0.4";
	camShakeAmp = "1.5 1.5 2";
	camShakeDuration = 1;
	camShakeRadius = 100.0;
};

datablock ProjectileData(BeefBoyLandProjectile) {
	lifetime = 10;
	fadeDelay = 10;
	explodeondeath = true;
	explosion = BeefBoyLandExplosion;
};