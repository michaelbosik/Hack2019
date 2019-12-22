namespace PowerUps {
    public class Shotgun : TimedPowerUp {

        protected override void callPowerUp() {
            powerUpManager.shotgun(duration);
        }
    }
}
