using Enums;
using Scenes;
using UnityEngine;

namespace PowerUps {
    public abstract class PowerUp : MonoBehaviour {
        // Constants
        private const float size = 5f;
        private const float speed = 25f;
        private const float zBuff = 10f;
    
        // Attributes
        private float xVel, yVel;
        protected PowerUpManager powerUpManager;

        private void Start() {
            Transform tf = transform;
            tf.localScale = new Vector3(size, size, 0);

            (float x, float y, float angle) = PowerUpManager.randomSpawn();
            tf.position = new Vector3(x, y, zBuff);
            xVel = speed * Mathf.Cos(angle);
            yVel = speed * Mathf.Sin(angle);
        
            powerUpManager = GameObject.Find(ScriptNames.PowerUpManager.GetString()).GetComponent<PowerUpManager>();
        }

        private void Update() {
            if (!Game.isPaused) {
                Transform tf = transform;
                Vector3 pos = tf.position;

                float tDelta = Time.deltaTime;
                pos.x += tDelta * xVel;
                pos.y += tDelta * yVel;
                tf.position = pos;
            }
        }

        private void OnBecameInvisible() {
            Destroy(gameObject);
        }
    
        protected abstract void callPowerUp();
    
        void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.name.Equals(SpriteNames.Astronaut.GetString())) {
                callPowerUp();
                Destroy(gameObject);
            }
        }
    }
}
