using Scenes;

namespace PowerUps {
    public class Coin : PowerUp {
        protected override void callPowerUp() {
            Game.coins++;
        }
    }
}
