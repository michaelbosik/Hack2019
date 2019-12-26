using System;
using Scenes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Aliens {
    public class AlienGunner : Alien {
        // Unity objects
        public GameObject blast;
        
        // Constants
        private const float sizeAvg = 5f;
        private const float sizeDev = 1f;
        private const float speedAvg = 40f;
        private const float speedDev = 5f;
        private const float shotDist = 350f;
        private const float shotRate = 1f;
        private const float shotSlowDown = 0.5f;
        private const int deathPoints = 5;
        private const float volume = 0.01f;
        
        // Attributes
        private float rdmSize, rdmSpeed, shotTimer;
        private Renderer r;

        protected override void onStart() {
            float z = Random.Range(-1, 1);
            rdmSize = sizeAvg + z * sizeDev;
            rdmSpeed = speedAvg - z * speedDev;
            shotTimer = 0;
            r = GetComponent<Renderer>();
        }

        protected override float getSize() {
            return rdmSize;
        }

        protected override float getSpeed() {
            return rdmSpeed;
        }

        protected override void onUpdate() {
            if (astronaut != null) {
                Vector3 astroPos = astronaut.transform.position;
                Vector3 alienPos = transform.position;
                float x = astroPos.x - alienPos.x;
                float y = astroPos.y - alienPos.y;
                float angle = Mathf.Atan2(y, x);
                transform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
                float dist = (float) Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                float xVel = speed * Mathf.Cos(angle);
                float yVel = speed * Mathf.Sin(angle);
                if ((dist < shotDist) && r.isVisible) {
                    xVel *= shotSlowDown;
                    yVel *= shotSlowDown;
                    if (shotTimer < shotRate) {
                        shotTimer += Time.deltaTime;
                    } else {
                        shotTimer = 0;
                        Instantiate(blast, transform.position, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
                    }
                }
                alienPos.x += Time.deltaTime * xVel;
                alienPos.y += Time.deltaTime * yVel;
                transform.position = alienPos;
            } else {
                celebrate();
            }
        }

        protected override void onShot() {
            Game.score += getDeathPoints();
            playDeathNoise();
            Destroy(gameObject);
        }

        protected override int getDeathPoints() {
            return deathPoints;
        }

        protected override void playDeathNoise() {
            alienManager.playDeathNoise(volume);
        }
    }
}