using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    private float time;

    void Start() {
        // Initialize variables
        time = 0F;
    }

    void Update() {
        // Quit
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
