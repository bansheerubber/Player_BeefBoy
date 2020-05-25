datablock TSShapeConstructor(BeefBoyGuard2DTS) {
	baseShape = "base/data/shapes/player/bull.dts";
	sequence0 = "base/data/shapes/player/bull.dsq";
};

datablock PlayerData(BeefBoyGuard2Armor : PlayerStandardArmor)  {
	shapeFile = BeefBoyGuard2DTS.baseShape;
	uiName = "Beef Boy Guard 2";

	isBeefBoyGuard2 = true;
	canJet = false;
	jumpForce = 0;
	boundingBox = "5 5 12";

	maxForwardSpeed = 8;
	maxBackwardSpeed = 8;
	maxSideSpeed = 8;

	minImpactSpeed = 500;
};

function Player::beefboyGuard2FixAppearance(%obj, %client) {
	%obj.hideNode("ALL");
	%obj.unHideNode("chest");
	%obj.unHideNode("lArm");
	%obj.unHideNode("rArm");
	%obj.unHideNode("lHandPike");
	%obj.unHideNode("rHand");
	%obj.unHideNode("headskin");
	%obj.unHideNode("armor");
	%obj.unHideNode("lFoot");
	%obj.unHideNode("rFoot");
	%obj.unHideNode("pants");
	%obj.unHideNode("pike");
	%obj.unHideNode("shield");
	%obj.setHeadUp(0);

	%obj.setNodeColor("armor", %client.secondPackColor);

	%obj.setFaceName(%client.faceName);
	%obj.setDecalName(%client.decalName);
	
	%obj.setNodeColor("headskin", %client.headColor);
	
	%obj.setNodeColor("chest", %client.chestColor);
	%obj.setNodeColor("lArm", %client.lArmColor);
	%obj.setNodeColor("rArm", %client.rArmColor);
	%obj.setNodeColor("lHand", %client.lHandColor);
	%obj.setNodeColor("lHandPike", %client.lHandColor);
	%obj.setNodeColor("rHand", %client.rHandColor);
	%obj.setNodeColor("lFoot", %client.lLegColor);
	%obj.setNodeColor("rFoot", %client.rLegColor);
	%obj.setNodeColor("pants", %client.hipColor);
}

deActivatePackage(BeefBoyGuard2Package);
package BeefBoyGuard2Package {
	function GameConnection::applyBodyColors(%this) {
		Parent::applyBodyColors(%this);

		if(%this.player && %this.player.getDatablock().isBeefBoyGuard2) {
			%this.player.beefboyGuard2FixAppearance(%this);
		}
	}

	function GameConnection::applyBodyParts(%this) {
		Parent::applyBodyParts(%this);

		if(%this.player && %this.player.getDatablock().isBeefBoyGuard2) {
			%this.player.beefboyGuard2FixAppearance(%this);
		}
	}
};
activatePackage(BeefBoyGuard2Package);