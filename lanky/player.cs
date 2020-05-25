datablock TSShapeConstructor(BeefBoyGuardDTS) {
	baseShape = "base/data/shapes/player/lanky.dts";
	sequence0 = "base/data/shapes/player/lanky.dsq";
};

datablock PlayerData(BeefBoyGuardArmor : PlayerStandardArmor)  {
	shapeFile = BeefBoyGuardDTS.baseShape;
	uiName = "Beef Boy Guard";

	isBeefBoyGuard = true;
	canJet = false;
	jumpForce = 0;
	boundingBox = "5 5 12";

	maxForwardSpeed = 8;
	maxBackwardSpeed = 8;
	maxSideSpeed = 8;
};

function Player::beefboyGuardFixAppearance(%obj, %client) {
	%obj.hideNode("ALL");
	%obj.unHideNode("torso");
	%obj.unHideNode("lArm");
	%obj.unHideNode("rArm");
	%obj.unHideNode("lHand");
	%obj.unHideNode("rHand");
	%obj.unHideNode("headskin");
	%obj.unHideNode("armor");
	%obj.unHideNode("lFoot");
	%obj.unHideNode("rFoot");
	%obj.unHideNode("pants");
	%obj.unHideNode("knife");
	%obj.setHeadUp(0);

	%obj.setNodeColor("armor", %client.secondPackColor);

	%obj.setFaceName(%client.faceName);
	%obj.setDecalName(%client.decalName);
	
	%obj.setNodeColor("headskin", %client.headColor);
	
	%obj.setNodeColor("torso", %client.chestColor);
	%obj.setNodeColor("lArm", %client.lArmColor);
	%obj.setNodeColor("rArm", %client.rArmColor);
	%obj.setNodeColor("lHand", %client.lHandColor);
	%obj.setNodeColor("rHand", %client.rHandColor);
	%obj.setNodeColor("lFoot", %client.lLegColor);
	%obj.setNodeColor("rFoot", %client.rLegColor);
	%obj.setNodeColor("pants", %client.hipColor);
}

deActivatePackage(BeefBoyGuardPackage);
package BeefBoyGuardPackage {
	function GameConnection::applyBodyColors(%this) {
		Parent::applyBodyColors(%this);

		if(%this.player && %this.player.getDatablock().isBeefBoyGuard) {
			%this.player.beefboyGuardFixAppearance(%this);
		}
	}

	function GameConnection::applyBodyParts(%this) {
		Parent::applyBodyParts(%this);

		if(%this.player && %this.player.getDatablock().isBeefBoyGuard) {
			%this.player.beefboyGuardFixAppearance(%this);
		}
	}

	function AiPlayer::onBotDamage(%this, %victim, %position, %damage, %damageType) {
		Parent::onBotDamage(%this, %victim, %position, %damage, %damageType);

		if(%this.getDatablock().isBeefBoyGuard) {
			// talk(%victim SPC %damage);
			%inAir = containerBoxEmpty($TypeMasks::fxBrickObjectType | $TypeMasks::StaticObjectType, %victim.getPosition(), 0.6, 0.6, 0.3);
			if(%inAir) {
				%this.emote(winStarProjectile, 1);
				%this.playAudio(2, BeefboyGuardAirshotRewardSound);
			}
		}
	}
};
activatePackage(BeefBoyGuardPackage);