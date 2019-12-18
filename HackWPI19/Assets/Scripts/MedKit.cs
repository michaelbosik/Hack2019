public class MedKit : PowerUp {
    private const float health = 0.1F;

    void Start() {
        initPowerUp();
    }

    void Update() {
        updatePowerUp();
    }

    protected override void callPowerUp() {
        powerUpManager.medKit(health);
    }
}
