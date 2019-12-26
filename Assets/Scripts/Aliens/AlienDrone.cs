using Scenes;
using UnityEngine;

namespace Aliens {
    public class AlienDrone : Alien {
        // Constants
        private const float sizeAvg = 4f;
        private const float sizeDev = 1f;
        private const float speedAvg = 60f;
        private const float speedDev = 10f;
        private const int deathPoints = 1;
        private const float volume = 0.01f;
        
        // Attributes
        private float rdmSize, rdmSpeed;

        protected override void onStart() {
            float z = Random.Range(-1, 1);
            rdmSize = sizeAvg + z * sizeDev;
            rdmSpeed = speedAvg - z * speedDev;
        }

        protected override float getSize() {
            return rdmSize;
        }

        protected override float getSpeed() {
            return rdmSpeed;
        }

        protected override void onUpdate() {
            if (astronaut != null) {
                trackPlayer();
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