using Player;
using Scenes;
using UnityEngine;

namespace Aliens {
    public class AlienKing : Alien {
        // Constants
        private const float alienSize = 150f;
        private const float alienSpeed = 30f;
        private const float totalHealth = 25f;
        private const int healthBarOffset = 25;
        private const int deathPoints = 25;
        private const float alienVolume = 0.01f;
        
        // Attributes
        private Vector3 healthBarPos;
        private HealthBar healthBar;
        private float health;

        protected override float getSize() {
            return alienSize;
        }

        protected override float getSpeed() {
            return alienSpeed;
        }

        protected override void onStart() {
            Transform tf = transform;
            Vector3 pos = tf.position;
            healthBar = Instantiate(serialBar, new Vector3(pos.x, pos.y - healthBarOffset, pos.z), tf.localRotation).GetComponent<HealthBar>();
            health = totalHealth;
            healthBar.setSize(health / totalHealth);
            healthBar.setColor(Color.green);
        }

        protected override void onUpdate() {
            Vector3 pos = transform.position;
            healthBar.transform.position = new Vector3(pos.x, pos.y - healthBarOffset, pos.z);
            float curHealth = health / totalHealth;
            if (curHealth > 0.75) {
                healthBar.setColor(Color.green);
            } else if (curHealth > 0.5) {
                healthBar.setColor(Color.yellow);
            } else {
                healthBar.setColor(Color.red);
            }
            healthBar.setSize(curHealth);
        }

        protected override void onShot() {
            if (--health <= 0) {
                Game.score += getDeathPoints();
                playDeathNoise();
                healthBar.destroy();
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