using Enums;
using Scenes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Aliens {
    public abstract class Alien : MonoBehaviour {
        // Constants

        // Attributes
        protected AlienManager alienManager;
        private float speed;
        protected int health;

        private void Start() {
            alienManager = GameObject.Find(ScriptNames.AlienManager.GetString()).GetComponent<AlienManager>();
            
            // Create alien with random size
            float size = getSize();
            transform.localScale = new Vector3(size, size, 0);

            // Create random speed for alien
            speed = getSpeed();

            health = getTotalHealth();
        }

        private void Update() {
            // Tracks player
            GameObject player = GameObject.Find(SpriteNames.Astronaut.GetString());

            float x = player.transform.position.x - transform.position.x;
            float y = player.transform.position.y - transform.position.y;
            float angle = Mathf.Atan2(y, x);
            float xVel = speed * Mathf.Cos(angle);
            float yVel = speed * Mathf.Sin(angle);
            Vector3 pos = transform.position;

            pos.x += Time.deltaTime * xVel;
            pos.y += Time.deltaTime * yVel;
            transform.position = pos;
            transform.localRotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
        }
        
        void OnCollisionEnter2D(Collision2D collision) {
            onCollision(collision);
        }

        protected abstract float getSize();

        protected abstract float getSpeed();

        protected abstract int getTotalHealth();

        protected abstract void onCollision(Collision2D collision);

        protected abstract int getDeathPoints();

        protected abstract void playDeathNoise();
    }
}