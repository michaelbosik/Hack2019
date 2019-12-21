using Scenes;
using UnityEngine;

namespace Aliens {
    public class AlienKing : Alien {
        private const float alienSize = 150F;
        private const float alienSpeed = 30F;
        private const int totalHealth = 25;
        private const int deathPoints = 100;
        private const float alienVolume = 0.01f;

        protected override float getSize() {
            return alienSize;
        }

        protected override float getSpeed() {
            return alienSpeed;
        }

        protected override int getTotalHealth() {
            return totalHealth;
        }

        protected override void onCollision(Collision2D collision) {
            if (--health <= 0) {
                Game.score += getDeathPoints();
                playDeathNoise();
                Destroy(gameObject);
            }
        }

        protected override int getDeathPoints() {
            return deathPoints;
        }

        protected override void playDeathNoise() {
            alienManager.playDeathNoise(alienVolume);
        }
    }
}