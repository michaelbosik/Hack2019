using Enums;
using Scenes;
using UnityEngine;

namespace Aliens {
    public class AlienDrone : Alien {
        // Constants
        private const float alienSize = 50F;
        private const float alienSizeDev = 10F;
        private const float alienSpeed = 45F;
        private const float alienSpeedDev = 10F;
        
        // Attributes
        private const int deathPoints = 1;
        private const float alienVolume = 0.01f;

        protected override float getSize() {
            return Random.Range(alienSize - alienSizeDev, alienSize + alienSizeDev);
        }

        protected override float getSpeed() {
            return Random.Range(alienSpeed - alienSpeedDev, alienSpeed + alienSpeedDev);
        }

        protected override void onStart() { }

        protected override void onUpdate() { }

        protected override void onCollision(Collision2D collision) {
            if (collision.gameObject.name.Equals(SpriteNames.Astronaut.GetString()) || collision.gameObject.name.Equals(SpriteNames.Bullet.GetString())) {
                // If collides with bullet, increase score
                if (collision.gameObject.name.Equals(SpriteNames.Bullet.GetString())) {
                    Game.score += getDeathPoints();
                }
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