namespace PowerUps {
    public class RapidFire : TimedPowerUp {
        private const float multiplier = 5f;

        protected override void callPowerUp() {
            powerUpManager.rapidFire(duration, multiplier);
        }
    }
}
