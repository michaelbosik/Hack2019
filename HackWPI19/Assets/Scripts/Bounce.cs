public class Bounce : TimedPowerUp {

    void Start() {
        initPowerUp();
    }
    
    void Update() {
        updatePowerUp();
    }

    protected override void callPowerUp() {
        powerUpManager.bounce(duration);
    }
}
