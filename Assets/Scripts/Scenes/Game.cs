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
        public GameObject alienDrone, alienKing, alienGunner, imgPause, txtPause;
        public Text txtScore, txtWave, txtLeft, txtHealth, txtWaveFlash, txtCoins;

        public static bool isPaused;

        // Private constants
        private const int alienRate = 3;
        private const int alienInit = 5;
        private const int kingRounds = 4;
        private const float kingRate = 0.75f;
        private const int gunnerRounds = 3;
        private const float powerRate = 0.75f;
        private const float powerInit = 0.5f;
        private const float flashTime = 2f;

        // Attributes
        private int wave;
        private bool isFlash;
        private float flashTimer;
        public static int score, coins;
        private List<GameObject> lstAliens;
        private static float right, left, top, bottom;
        private Astronaut astronaut;
        private PowerUpManager powerUpManager;

        void Start() {
            // Initialize variables
            isPaused = false;
            imgPause.SetActive(false);
            txtPause.SetActive(false);
            wave = 0;
            flashTimer = 0;
            score = 0;
            coins = 0;
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
            if (Input.GetKeyDown(KeyCode.Escape)) {
                isPaused = !isPaused;
                imgPause.SetActive(isPaused);
                txtPause.SetActive(isPaused);
            }
            
            if (!isPaused) {
                // HUD text
                txtScore.text = "Score: " + score;
                txtLeft.text = "Aliens Left: " + lstAliens.Count;
                txtHealth.text = "Health: " + astronaut.getHealth();
                txtCoins.text = "Coins: " + coins;
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
                        GameObject newAlien = Instantiate(alienDrone);
                        lstAliens.Add(newAlien);
                    }
                    for (int i = 0; i < numKing; i++) {
                        Instantiate(alienKing);
                    }
                    for (int i = 0; i < numGunner; i++) {
                        GameObject newAlien = Instantiate(alienGunner);
                        lstAliens.Add(newAlien);
                    }

                    // Spawn power-ups
                    int numP = (int) (powerRate * wave + powerInit);
                    for (int i = 0; i < numP; i++) {
                        Instantiate(powerUpManager.rdmPowerUp());
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
        }

        public static (float top, float right, float bottom, float left) getBounds() {
            Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            right = screenSize.x;
            left = 0F;
            top = screenSize.y;
            bottom = 0F;
            return (top, right, bottom, left);
        }

        public static void endGame() {
            SceneManager.LoadScene(SceneNames.End.GetString());
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
