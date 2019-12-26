using System;
using Player;
using Scenes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Aliens {
    public class AlienKing : Alien {
        // Unity objects
        public GameObject serialBar;
        
        // Constants
        private const float sizeAvg = 10f;
        private const float sizeDev = 3f;
        private const float speedAvg = 30f;
        private const float speedDev = 5f;
        private const float healthAvg = 25f;
        private const float healthDev = 5f;
        private const int healthBarOffset = 25;
        private const float volume = 0.01f;
        
        // Attributes
        private Vector3 healthBarPos;
        private HealthBar healthBar;
        private float rdmSize, rdmSpeed, totalHealth, health;
        private int deathPoints;
        
        protected override void onStart() {
            float z = Random.Range(-1, 1);
            rdmSize = sizeAvg + z * sizeDev;
            rdmSpeed = speedAvg - z * speedDev;
            totalHealth = (float) Math.Round(healthAvg + z * healthDev);
            deathPoints = (int) totalHealth;
            
            Transform tf = transform;
            Vector3 pos = tf.position;
            healthBar = Instantiate(serialBar, new Vector3(pos.x, pos.y - healthBarOffset, pos.z), tf.localRotation).GetComponent<HealthBar>();
            health = totalHealth;
            healthBar.setSize(health / totalHealth);
            healthBar.setColor(Color.green);
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
            
            // Update health bar
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
            alienManager.playDeathNoise(volume);
        }
    }
}