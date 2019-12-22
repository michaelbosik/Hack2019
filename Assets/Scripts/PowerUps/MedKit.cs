namespace PowerUps {
    public class MedKit : PowerUp {
        private const float health = 0.1F;

        protected override void callPowerUp() {
            powerUpManager.medKit(health);
        }
    }
}
