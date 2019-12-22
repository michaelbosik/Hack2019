using System.Collections.Generic;
using Enums;
using Player;
using PowerUps;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes {
    public class Game : MonoBehaviour {
        // Unity objects
        public GameObject alienDrone, alienKing;
        public Text txtScore, txtWave, txtLeft, txtHealth;

        // Constants
        private const int alienRate = 3;
        private const int alienInit = 5;
        private const int kingRounds = 4;
        private const float kingRate = 0.75f;
        private const float powerRate = 0.25f;
        private const float powerInit = 0.5f;
        private const float buffer = 250f;
        private const float zBuff = 10f;

        // Attributes
        private int wave;
        public static int score;
        private List<GameObject> lstAliens;
        private static float right, left, up, down;
        private Astronaut astronaut;
        private PowerUpManager powerUpManager;

        void Start() {
            // Initialize variables
            wave = 0;
            score = 0;
            lstAliens = new List<GameObject>();
            Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            right = screenSize.x;
            left = 0;
            up = screenSize.y;
            down = 0;
            astronaut = GameObject.Find(SpriteNames.Astronaut.GetString()).GetComponent<Astronaut>();
            powerUpManager = GameObject.Find(ScriptNames.PowerUpManager.GetString()).GetComponent<PowerUpManager>();
        }

        void Update() {
            // HUD text
            txtScore.text = "Score: " + score;
            txtLeft.text = "Aliens Left: " + lstAliens.Count;
            txtHealth.text = "Health: " + astronaut.getHealth();

            // Quit
            if (Input.GetKey(KeyCode.Escape)) {
                SceneManager.LoadScene(SceneNames.End.GetString());
            }

            // Next wave
            if (lstAliens.Count == 0) {
                // Spawn aliens
                int numDrone, numKing;
                if ((wave + 1) % kingRounds != 0) {
                    numDrone = alienRate * wave + alienInit;
                    numKing = 0;
                } else {
                    numDrone = (int) ((alienRate * wave + alienInit) * kingRate);
                    numKing = (wave + 1) / kingRounds;
                }
                for (int i = 0; i < numDrone; i++) {
                    (float x, float y) = randomSpawn(-1);
                    Vector3 pos = new Vector3(x, y, zBuff);
                    GameObject newAlien = Instantiate(alienDrone, pos, Quaternion.identity);
                    lstAliens.Add(newAlien);
                }
                for (int i = 0; i < numKing; i++) {
                    (float x, float y) = randomSpawn(-1);
                    Vector3 pos = new Vector3(x, y, zBuff);
                    GameObject newAlien = Instantiate(alienKing, pos, Quaternion.identity);
                    // lstAliens.Add(newAlien);
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
                txtWave.text = "Wave: " + ++wave;
            }

            // Remove dead aliens
            for (int i = 0; i < lstAliens.Count; i++) {
                if (lstAliens[i] == null) {
                    lstAliens.RemoveAt(i);
                }
            }
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
                    y = Random.Range(up, up + buffer);
                    break;
                case 1: // Right
                    x = Random.Range(right, right + buffer);
                    y = Random.Range(down, up);
                    break;
                case 2: // Down
                    x = Random.Range(left, right);
                    y = Random.Range(down - buffer, down);
                    break;
                default: // Left
                    x = Random.Range(left - buffer, left);
                    y = Random.Range(down, up);
                    break;
            }
            return (x, y);
        }

        public static int findEdge(float x, float y) {
            if (y >= up) {
                return 0; // Up
            } else if (x >= right) {
                return 1; // Right
            } else if (y <= down) {
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
