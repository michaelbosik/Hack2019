namespace PowerUps {
    public class Bounce : TimedPowerUp {

        protected override void callPowerUp() {
            powerUpManager.bounce(duration);
        }
    }
}
