using Enums;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace PowerUps {
    public class PowerUpManager : MonoBehaviour {
        // Unity objects
        public GameObject medKit_0, shotgun_0, laser_0, rapidFire_0, bounce_0;
        public Image imgShotgun, imgLaser, imgRapidFire, imgBounce;

        private const float disableAlpha = 0.25f;
        private const float enableAlpha = 1f;
    
        private Astronaut astronaut;
        private bool isShotgun, isLaser, isRapidFire, isBounce;
        private float timerShotgun, timerLaser, timerRapidFire, timerBounce;
        private float durShotGun, durLaser, durRapidFire, durBounce;
        private float rapidMultiplier;
    

        void Start() {
            astronaut = GameObject.Find(SpriteNames.Astronaut.GetString()).GetComponent<Astronaut>();

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
            float tDelta = Time.deltaTime;

            // Shotgun
            if (isShotgun) {
                timerShotgun += tDelta;
                if (timerShotgun > durShotGun) {
                    astronaut.setShotgun(false);
                    alphaImage(imgShotgun, disableAlpha);
                    isShotgun = false;
                }
            }
        
            // Laser
            if (isLaser) {
                timerLaser += tDelta;
                if (timerLaser > durLaser) {
                    astronaut.setLaser(false);
                    alphaImage(imgLaser, disableAlpha);
                    isLaser = false;
                }
            }
        
            // Rapid fire
            if (isRapidFire) {
                timerRapidFire += tDelta;
                if (timerRapidFire > durRapidFire) {
                    astronaut.setRapidFire(false, rapidMultiplier);
                    alphaImage(imgRapidFire, disableAlpha);
                    isRapidFire = false;
                }
            }
        
            // Bounce
            if (isBounce) {
                timerBounce += tDelta;
                if (timerBounce > durBounce) {
                    astronaut.setBounce(false);
                    alphaImage(imgBounce, disableAlpha);
                    isBounce = false;
                }
            }
        }

        public GameObject rdmPowerUp() {
            // GameObject[] powerUps = {medKit_0, shotgun_0, laser_0, rapidFire_0, bounce_0};
            GameObject[] powerUps = {bounce_0};
            return powerUps[Random.Range(0, powerUps.Length)];
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
            astronaut.setRapidFire(true, rapidMultiplier);
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
