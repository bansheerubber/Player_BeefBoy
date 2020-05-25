deActivatePackage(IsGroundedPackage);
package IsGroundedPackage {
	function Armor::onAdd(%this, %obj) {
		Parent::onAdd(%this, %obj);
		%obj.isGroundedLoop();
	}
};
activatePackage(IsGroundedPackage);

function Player::isGroundedLoop(%this) {
	%this.isGrounded = !containerBoxEmpty($TypeMasks::fxBrickObjectType | $TypeMasks::StaticObjectType, %this.getPosition(), 0.6, 0.6, 0.3);
	%this.isGroundedSchedule = %this.schedule(100, isGroundedLoop);
}