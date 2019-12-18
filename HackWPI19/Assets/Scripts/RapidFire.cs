using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapidFire : TimedPowerUp {
    private const float multiplier = 5f;
    
    void Start() {
        initPowerUp();
    }

    void Update() {
        updatePowerUp();
    }

    protected override void callPowerUp() {
        powerUpManager.rapidFire(duration, multiplier);
    }
}
