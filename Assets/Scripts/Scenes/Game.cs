﻿using System.Collections.Generic;
using Enums;
using Player;
using PowerUps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes {
    public class Game : MonoBehaviour {
        // Unity objects
        public GameObject alienDrone, alienKing, alienGunner;
        public Text txtScore, txtWave, txtLeft, txtHealth, txtWaveFlash;

        // Private constants
        private const int alienRate = 3;
        private const int alienInit = 5;
        private const int kingRounds = 4;
        private const float kingRate = 0.75f;
        private const int gunnerRounds = 3;
        private const float powerRate = 0.75f;
        private const float powerInit = 0.5f;
        private const float buffer = 250f;
        private const float zBuff = 10f;
        private const float flashTime = 2f;

        // Attributes
        private int wave;
        private bool isFlash;
        private float flashTimer;
        public static int score;
        private List<GameObject> lstAliens;
        private static float right, left, top, bottom;
        private Astronaut astronaut;
        private PowerUpManager powerUpManager;

        void Start() {
            // Initialize variables
            wave = 0;
            flashTimer = 0;
            score = 0;
            lstAliens = new List<GameObject>();
            Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            right = screenSize.x;
            left = 0;
            top = screenSize.y;
            bottom = 0;
            astronaut = GameObject.Find(SpriteNames.Astronaut.GetString()).GetComponent<Astronaut>();
            powerUpManager = GameObject.Find(ScriptNames.PowerUpManager.GetString()).GetComponent<PowerUpManager>();
        }

        void Update() {
            // HUD text
            txtScore.text = "Score: " + score;
            txtLeft.text = "Aliens Left: " + lstAliens.Count;
            txtHealth.text = "Health: " + astronaut.getHealth();
            if (isFlash) {
                flashTimer += Time.deltaTime;
                if (flashTimer > flashTime) {
                    isFlash = false;
                    flashTimer = 0;
                    Color tempColor = txtWaveFlash.color;
                    tempColor.a = 0;
                    txtWaveFlash.color = tempColor;
                }
            }

            // Quit
            if (Input.GetKey(KeyCode.Escape)) {
                SceneManager.LoadScene(SceneNames.End.GetString());
            }

            // Next wave
            if (lstAliens.Count == 0) {
                // Calculate number of alien types
                int numDrone, numKing, numGunner;
                if ((wave + 1) % kingRounds != 0) {
                    numDrone = alienRate * wave + alienInit;
                    numKing = 0;
                } else {
                    numDrone = (int) ((alienRate * wave + alienInit) * kingRate);
                    numKing = (wave + 1) / kingRounds;
                }
                if ((wave + 1) % gunnerRounds != 0) {
                    numGunner = 0;
                } else {
                    numGunner = (wave + 1) / gunnerRounds;
                }
                
                // Spawn aliens
                for (int i = 0; i < numDrone; i++) {
                    (float x, float y) = randomSpawn(-1);
                    Vector3 pos = new Vector3(x, y, zBuff);
                    GameObject newAlien = Instantiate(alienDrone, pos, Quaternion.identity);
                    lstAliens.Add(newAlien);
                }
                for (int i = 0; i < numKing; i++) {
                    (float x, float y) = randomSpawn(-1);
                    Vector3 pos = new Vector3(x, y, zBuff);
                    Instantiate(alienKing, pos, Quaternion.identity);
                }
                for (int i = 0; i < numGunner; i++) {
                    (float x, float y) = randomSpawn(-1);
                    Vector3 pos = new Vector3(x, y, zBuff);
                    GameObject newAlien = Instantiate(alienGunner, pos, Quaternion.identity);
                    lstAliens.Add(newAlien);
                }

                // Spawn power-ups
                int numP = (int) (powerRate * wave + powerInit);
                for (int i = 0; i < numP; i++) {
                    (float x, float y) = randomSpawn(-1);
                    Vector3 pos = new Vector3(x, y, zBuff);
                    GameObject newPowerUp = powerUpManager.rdmPowerUp();
                    Instantiate(newPowerUp, pos, Quaternion.identity);
                }
            
                // Update wave
                string strWave = "Wave: " + ++wave;
                txtWave.text = strWave;
                txtWaveFlash.text = strWave;
                Color tempColor = txtWaveFlash.color;
                tempColor.a = 0.5f;
                txtWaveFlash.color = tempColor;
                isFlash = true;
            }

            // Remove dead aliens
            for (int i = 0; i < lstAliens.Count; i++) {
                if (lstAliens[i] == null) {
                    lstAliens.RemoveAt(i);
                }
            }
        }

        public static (float top, float right, float bottom, float left) getBounds() {
            Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            right = screenSize.x;
            left = 0F;
            top = screenSize.y;
            bottom = 0F;
            return (top, right, bottom, left);
        }

        public static (float, float) randomSpawn(int avoid) {
            List<int> lstEdges = new List<int> { 0, 1, 2, 3 };
            if (avoid != -1) {
                lstEdges.RemoveAt(avoid);
            }
            float x, y;
            int coin = lstEdges[Random.Range(0, lstEdges.Count)];
            switch (coin) {
                case 0: // Up
                    x = Random.Range(left, right);
                    y = Random.Range(top, top + buffer);
                    break;
                case 1: // Right
                    x = Random.Range(right, right + buffer);
                    y = Random.Range(bottom, top);
                    break;
                case 2: // Down
                    x = Random.Range(left, right);
                    y = Random.Range(bottom - buffer, bottom);
                    break;
                default: // Left
                    x = Random.Range(left - buffer, left);
                    y = Random.Range(bottom, top);
                    break;
            }
            return (x, y);
        }

        public static int findEdge(float x, float y) {
            if (y >= top) {
                return 0; // Up
            } else if (x >= right) {
                return 1; // Right
            } else if (y <= bottom) {
                return 2; // Down
            } else {
                return 3; // Left
            }
        }

        // Radian [-pi, pi] to degrees [0, 360]
        public static float transformAngle(float angle) {
            angle *= Mathf.Rad2Deg;
            if (angle < 0) {
                angle += 360;
            }
            return angle;
        }
    }
}
