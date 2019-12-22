using Enums;
using UnityEngine;

namespace Aliens {
    public abstract class Alien : MonoBehaviour {
        // Unity objects
        public GameObject serialBar;
        
        // Attributes
        protected AlienManager alienManager;
        private GameObject astronaut;
        private float speed;

        private void Start() {
            alienManager = GameObject.Find(ScriptNames.AlienManager.GetString()).GetComponent<AlienManager>();
            astronaut = GameObject.Find(SpriteNames.Astronaut.GetString());

            float size = getSize();
            transform.localScale = new Vector3(size, size, 0);
            speed = getSpeed();

            onStart();
        }

        private void Update() {
            trackPlayer();
            onUpdate();
        }

        private void trackPlayer() {
            Vector3 astroPos = astronaut.transform.position;
            Vector3 alienPos = transform.position;
            float x = astroPos.x - alienPos.x;
            float y = astroPos.y - alienPos.y;
            float angle = Mathf.Atan2(y, x);
            float xVel = speed * Mathf.Cos(angle);
            float yVel = speed * Mathf.Sin(angle);

            alienPos.x += Time.deltaTime * xVel;
            alienPos.y += Time.deltaTime * yVel;
            transform.position = alienPos;
            transform.localRotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
        }
        
        void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.name.Equals(SpriteNames.Bullet.GetString())) {
                onShot();
            }
        }

        protected abstract float getSize();
        protected abstract float getSpeed();
        protected abstract void onStart();
        protected abstract void onUpdate();
        protected abstract void onShot();
        protected abstract int getDeathPoints();
        protected abstract void playDeathNoise();
    }
}