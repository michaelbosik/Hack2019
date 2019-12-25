using System;
using Enums;
using Scenes;
using UnityEngine;

namespace PowerUps {
    public abstract class PowerUp : MonoBehaviour {
        private const float size = 5f;
        private const float speed = 25f;
    
        private float xVel, yVel;
        protected PowerUpManager powerUpManager;

        private void Start() {
            transform.localScale = new Vector3(size, size, 0);

            float curX = transform.position.x;
            float curY = transform.position.y;
            int edge = Game.findEdge(curX, curY);
            (float destX, float destY) = Game.randomSpawn(edge);
            float x = destX - curX;
            float y = destY - curY;

            float angle = Mathf.Atan2(y, x);

            xVel = speed * Mathf.Cos(angle);
            yVel = speed * Mathf.Sin(angle);
        
            powerUpManager = GameObject.Find(ScriptNames.PowerUpManager.GetString()).GetComponent<PowerUpManager>();
        }

        private void Update() {
            Vector3 pos = transform.position;

            float tDelta = Time.deltaTime;
            pos.x += tDelta * xVel;
            pos.y += tDelta * yVel;
            transform.position = pos;
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
