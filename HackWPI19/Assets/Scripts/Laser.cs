public class Laser : TimedPowerUp {

    void Start() {
        initPowerUp();
    }
    
    void Update() {
        updatePowerUp();
    }

    protected override void callPowerUp() {
        powerUpManager.laser(duration);
    }
}
