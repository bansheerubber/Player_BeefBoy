deActivatePackage(BeefBoyBotDamage);
package BeefBoyBotDamage {
	function Player::damage(%this, %col, %position, %damage, %damageType) {
		Parent::damage(%this, %col, %position, %damage, %damageType);

		if(%col.isBot) {
			%col.onBotDamage(%this, %position, %damage, %damageType);
		}
		else if(%col.sourceObject.isBot) {
			%col.sourceObject.onBotDamage(%this, %position, %damage, %damageType);
		}
	}
};
activatePackage(BeefBoyBotDamage);

function AiPlayer::onBotDamage(%this, %victim, %position, %damage, %damageType) {

}