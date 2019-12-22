namespace PowerUps {
    public class Laser : TimedPowerUp {

        protected override void callPowerUp() {
            powerUpManager.laser(duration);
        }
    }
}
