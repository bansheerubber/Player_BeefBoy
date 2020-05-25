datablock TSShapeConstructor(BeefBoyDTS) {
	baseShape = "./shapes/megaknight.dts";
	sequence0 = "./shapes/megaknight.dsq";
};

datablock PlayerData(BeefBoyArmor : PlayerStandardArmor)  {
	shapeFile = BeefBoyDTS.baseShape;
	uiName = "Beef Boy";

	isBeefBoy = true;
	canJet = false;
	jumpForce = 0;
	boundingBox = "13 13 20";

	maxForwardSpeed = 8;
	maxBackwardSpeed = 8;
	maxSideSpeed = 8;
	mass = 800;
	runForce = 60000;
};

function Player::testBounds(%this) {
	%objectBox = %this.getObjectBox();
	(new Projectile() {
		datablock = gunProjectile;
		initialPosition = vectorAdd(%this.getPosition(), getWords(%objectBox, 0, 2));
		initialVelocity = "0 0 20";
		sourceObject = 0;
		sourceSlot = 0;
	}).explode();

	(new Projectile() {
		datablock = gunProjectile;
		initialPosition = vectorAdd(%this.getPosition(), getWords(%objectBox, 3, 5));
		initialVelocity = "0 0 20";
		sourceObject = 0;
		sourceSlot = 0;
	}).explode();

	%this.schedule(33, testBounds);
}

function Player::beefboyFixAppearance(%obj, %client) {
	%obj.hideNode("ALL");
	%obj.unHideNode("armor");
	%obj.unHideNode("helmet");
	%obj.unHideNode("lArm");
	%obj.unHideNode("rArm");
	%obj.unHideNode("lHand");
	%obj.unHideNode("rHand");
	%obj.unHideNode("headskin");
	%obj.unHideNode("shoulderPads");
	%obj.unHideNode("lFoot");
	%obj.unHideNode("rFoot");
	%obj.setHeadUp(0);

	%obj.setNodeColor("shoulderPads", %client.secondPackColor);

	%obj.setFaceName(%client.faceName);
	%obj.setDecalName(%client.decalName);
	
	%obj.setNodeColor("headskin", %client.headColor);
	
	%obj.setNodeColor("armor", %client.chestColor);
	%obj.setNodeColor("lArm", %client.lArmColor);
	%obj.setNodeColor("rArm", %client.rArmColor);
	%obj.setNodeColor("lHand", %client.lHandColor);
	%obj.setNodeColor("rHand", %client.rHandColor);
	%obj.setNodeColor("lFoot", %client.lLegColor);
	%obj.setNodeColor("rFoot", %client.rLegColor);
	%obj.setNodeColor("helmet", %client.hatColor);
}

function BeefBoyArmor::talk(%this, %obj) {
	if(isObject(%obj)) {
		(%p = new Projectile() {
			datablock = BeefBoyTalkProjectile;
			initialPosition = %obj.getHackPosition();
			initialVelocity = "0 0 10";
			sourceObject = 0;
			sourceSlot = 0;
		}).explode();
		%obj.talkSchedule = %this.schedule(150, talk, %obj);
	}
}

function Player::beefboyRuneFade(%this, %value, %step) {
	if(%value >= 0 && %value <= 1) {
		%this.setNodeColor("runes", "0 0 0" SPC %value);
		%this.schedule(33, beefboyRuneFade, %value + %step, %step);	
	}
	else if(%value >= 1) {
		%this.setNodeColor("runes", "0 0 0 1");
	}
	else if(%value <= 0) {
		%this.setNodeColor("runes", "0 0 0 0");
	}
}

function Player::beefboyActivateRunes(%this) {
	%this.setNodeColor("runes", "0 0 0 1");
	%this.unHideNode("runes");

	%this.playAudio(1, BeefBoyRunesSound);
	%this.playAudio(2, BeefBoyRunesRiseSound);

	%this.beefboyRightSword();
	%this.playThread(3, "runes");

	for(%i = 0; %i < 4; %i++) {
		%this.schedule(1500 + 600 * %i, beefboyToggleSword);
	}

	%this.mountImage(BeefboyRunesImage, 3);
	%this.schedule(1500, beefboySpinRunes);
	
	%this.schedule(1500 + 35 / 24 * 1000, beefboySummonMinions);

	%this.schedule(1500 + 600 * %i + 400, playThread, 3, "runesStop");
	%this.schedule(1500 + 600 * %i + 400, beefboyDisableRunes);
	%this.schedule(1500 + 600 * %i + 400 + 1000 * 7 / 24, beefboyLand);
	%this.schedule(1500 + 600 * %i + 400, unMountImage, 3);
}

function Player::beefboySummonMinions(%this) {
	%rightVector = vectorNormalize(vectorCross(%this.getForwardVector(), "0 0 1"));
	%position = vectorAdd(%this.getPosition(), "0 0 2");

	%this.playAudio(0, BeefBoySpawnGuySound);

	%this.playThread(0, "talk");
	%this.schedule(1000, playThread, 0, root);

	for(%i = 0; %i < 1; %i++) {
		%bot = spawnLanky(vectorAdd(%position, vectorScale(%rightVector, 7 + %i * 3)));
		messageAll('', "\c3" @ %this.getShapeName() @ "\c6: Assist me" SPC %bot.getShapeName() @ "!");
	}

	for(%i = 0; %i < 1; %i++) {
		%bot = spawnBull(vectorAdd(%position, vectorScale(%rightVector, -7 - %i * 3)));
		messageAll('', "\c3" @ %this.getShapeName() @ "\c6: Defend me" SPC %bot.getShapeName() @ "!");
	}
}

function Player::beefboySpinRunes(%this) {
	%this.unHideNode("magicBody");
	%this.setNodeColor("magicBody", "0.2 0 1 1");

	%this.playThread(1, "runeSpin");
	
	%this.schedule(44 / 24 * 1000, hideNode, "magicBody");
}

function Player::beefboyLand(%this) {
	(%p = new Projectile() {
		datablock = BeefBoyLandProjectile;
		initialPosition = %this.getHackPosition();
		initialVelocity = "0 0 10";
		sourceObject = 0;
		sourceSlot = 0;
	}).explode();
}

function Player::beefboyDisableRunes(%this) {
	%this.hideNode("runes");
}

deActivatePackage(BeefBoyPackage);
package BeefBoyPackage {
	function Player::playThread(%this, %slot, %thread) {
		Parent::playThread(%this, %slot, %thread);

		if(%this.getDatablock().getName() $= "BeefBoyArmor") {
			if(%thread $= "talk" && !%this.talkThreads[%slot]) {
				cancel(%obj.talkSchedule);
				%this.getDatablock().talk(%this);
				%this.talkThreads[%slot] = true;
			}
			else if(%thread $= "root" && %this.talkThreads[%slot] == true) {
				cancel(%this.talkSchedule);
				%this.talkThreads[%slot] = false;
			}
		}
	}
	
	function GameConnection::applyBodyColors(%this) {
		Parent::applyBodyColors(%this);

		if(%this.player && %this.player.getDatablock().isBeefBoy) {
			%this.player.beefboyFixAppearance(%this);
		}
	}

	function GameConnection::applyBodyParts(%this) {
		Parent::applyBodyParts(%this);

		if(%this.player && %this.player.getDatablock().isBeefBoy) {
			%this.player.beefboyFixAppearance(%this);
		}
	}
};
activatePackage(BeefBoyPackage);