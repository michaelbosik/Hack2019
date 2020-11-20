using System.Collections.Generic;
using Enums;
using Player;
using Scenes;
using UnityEngine;
using UnityEngine.UI;

namespace PowerUps {
    public class PowerUpManager : MonoBehaviour {
        // Unity objects
        public GameObject medKit_0, shotgun_0, laser_0, rapidFire_0, bounce_0, coin_0;
        public Image imgShotgun, imgLaser, imgRapidFire, imgBounce;
        public GameObject timer;

        // Constants
        private const float disableAlpha = 0.25f;
        private const float enableAlpha = 1f;
        private const float buffer = 250f;
        private const float lenRange = 0.5f;
        private const float angleRange = 45f;
        private const int medKitSpawnWeight = 3;
        private const int shotgunSpawnWeight = 1;
        private const int laserSpawnWeight = 1;
        private const int rapidFireSpawnWeight = 1;
        private const int bounceSpawnWeight = 1;
        private const int coinSpawnWeight = 2;
        private const float powerUpWarning = 2.0f;
        private const float powerUpWarningRate = 0.25f;
    
        private Astronaut astronaut;
        private bool isShotgun, isLaser, isRapidFire, isBounce;
        private float timerShotgun, timerLaser, timerRapidFire, timerBounce;
        private float durShotGun, durLaser, durRapidFire, durBounce;
        private float rapidMultiplier;
        private static float top, right, bottom, left;
        
        void Start() {
            astronaut = GameObject.Find(SpriteNames.Astronaut.GetString()).GetComponent<Astronaut>();
            
            // Bounds
            (top, right, bottom, left) = Game.getBounds();

            // Shotgun
            isShotgun = false;
            timerShotgun = 0;
            durShotGun = 0;
        
            // Laser
            isLaser = false;
            timerLaser = 0;
            durLaser = 0;
        
            // Rapid fire
            isRapidFire = false;
            timerRapidFire = 0;
            durRapidFire = 0;
            rapidMultiplier = 0;
        
            // Bounce
            isBounce = false;
            timerBounce = 0;
            durBounce = 0;

            // Power up icons
            alphaImage(imgShotgun, disableAlpha);
            alphaImage(imgLaser, disableAlpha);
            alphaImage(imgRapidFire, disableAlpha);
            alphaImage(imgBounce, disableAlpha);
        }

        void Update() {
            if (!Game.isPaused) {
                float tDelta = Time.deltaTime;

            // Shotgun
            if (isShotgun) {
                timerShotgun += tDelta;
                
                if (timerShotgun > durShotGun - powerUpWarning) {
                    float alpha = ((int) ((durShotGun - timerShotgun) / powerUpWarningRate) % 2 == 0)
                        ? disableAlpha
                        : enableAlpha;
                    alphaImage(imgShotgun, alpha);
                }
                
                if (timerShotgun > durShotGun) {
                    astronaut.setShotgun(false);
                    alphaImage(imgShotgun, disableAlpha);
                    isShotgun = false;
                }
            }
        
            // Laser
            if (isLaser) {
                timerLaser += tDelta;
                
                if (timerLaser > durLaser - powerUpWarning) {
                    float alpha = ((int) ((durLaser - timerLaser) / powerUpWarningRate) % 2 == 0)
                        ? disableAlpha
                        : enableAlpha;
                    alphaImage(imgLaser, alpha);
                }
                
                if (timerLaser > durLaser) {
                    astronaut.setLaser(false);
                    alphaImage(imgLaser, disableAlpha);
                    isLaser = false;
                }
            }
        
            // Rapid fire
            if (isRapidFire) {
                timerRapidFire += tDelta;
                
                if (timerRapidFire > durRapidFire - powerUpWarning) {
                    float alpha = ((int) ((durRapidFire - timerRapidFire) / powerUpWarningRate) % 2 == 0)
                        ? disableAlpha
                        : enableAlpha;
                    alphaImage(imgRapidFire, alpha);
                }
                
                if (timerRapidFire > durRapidFire) {
                    astronaut.setRapidFire(false, rapidMultiplier);
                    alphaImage(imgRapidFire, disableAlpha);
                    isRapidFire = false;
                }
            }
        
            // Bounce
            if (isBounce) {
                timerBounce += tDelta;
                
                if (timerBounce > durBounce - powerUpWarning) {
                    float alpha = ((int) ((durBounce - timerBounce) / powerUpWarningRate) % 2 == 0)
                        ? disableAlpha
                        : enableAlpha;
                    alphaImage(imgBounce, alpha);
                }
                
                if (timerBounce > durBounce) {
                    astronaut.setBounce(false);
                    alphaImage(imgBounce, disableAlpha);
                    isBounce = false;
                }
            }
            }
        }

        public GameObject rdmPowerUp() {
            Dictionary<GameObject, int> powerUpWeights = new Dictionary<GameObject, int> {
                {medKit_0, medKitSpawnWeight},
                {shotgun_0, shotgunSpawnWeight},
                {laser_0, laserSpawnWeight},
                {rapidFire_0, rapidFireSpawnWeight},
                {bounce_0, bounceSpawnWeight},
                {coin_0, coinSpawnWeight}
            };
            List<GameObject> powerUpsDist = new List<GameObject>();
            foreach (GameObject go in powerUpWeights.Keys) {
                for (int i = 0; i < powerUpWeights[go]; i++) {
                    powerUpsDist.Add(go);
                }
            }
            return powerUpsDist[Random.Range(0, powerUpsDist.Count)];
        }

        public static (float x, float y, float angle) randomSpawn() {
            float xLen = right - left;
            float yLen = top - bottom;
            float xMid = xLen / 2f;
            float yMid = yLen / 2f;
            float xRange = xLen * lenRange;
            float yRange = yLen * lenRange;
            
            float x, y, angleOffset;
            int side = Random.Range(0, 4);
            switch (side) {
                case 0: // Top
                    x = Random.Range(xMid - xRange, xMid + xRange);
                    y = Random.Range(top, top + buffer);
                    angleOffset = 270f;
                    break;
                case 1: // Right
                    x = Random.Range(right, right + buffer);
                    y = Random.Range(yMid - yRange, yMid + yRange);
                    angleOffset = 180f;
                    break;
                case 2: // Bottom
                    x = Random.Range(xMid - xRange, xMid + xRange);
                    y = Random.Range(bottom - buffer, bottom);
                    angleOffset = 90f;
                    break;
                default:
                    x = Random.Range(left - buffer, left);
                    y = Random.Range(yMid - yRange, yMid + yRange);
                    angleOffset = 0f;
                    break;
            }
            float angle = Random.Range(angleOffset - angleRange, angleOffset + angleRange) * Mathf.Deg2Rad;
            return (x, y, angle);
        }

        public void shotgun(float duration) {
            astronaut.setShotgun(true);
            alphaImage(imgShotgun, enableAlpha);
            isShotgun = true;
            timerShotgun = 0;
            durShotGun = duration;
        }

        public void laser(float duration) {
            astronaut.setLaser(true);
            alphaImage(imgLaser, enableAlpha);
            isLaser = true;
            timerLaser = 0;
            durLaser = duration;
        }

        public void rapidFire(float duration, float multiplier) {
            rapidMultiplier = multiplier;
            if (!isRapidFire) {
                astronaut.setRapidFire(true, rapidMultiplier);
            }
            alphaImage(imgRapidFire, enableAlpha);
            isRapidFire = true;
            timerRapidFire = 0;
            durRapidFire = duration;
        }

        public void medKit(float health) {
            astronaut.addHealth(health);
        }

        public void bounce(float duration) {
            astronaut.setBounce(true);
            alphaImage(imgBounce, enableAlpha);
            isBounce = true;
            timerBounce = 0;
            durBounce = duration;
        }

        private void alphaImage(Image img, float a) {
            var tempColor = img.color;
            tempColor.a = a;
            img.color = tempColor;
        }
    }
}
