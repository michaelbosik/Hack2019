﻿public class Shotgun : TimedPowerUp {

    void Start() {
        initPowerUp();
    }

    void Update() {
        updatePowerUp();
    }

    protected override void callPowerUp() {
        powerUpManager.shotgun(duration);
    }
}
