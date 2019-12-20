using Enums;
using UnityEngine;

namespace Player {
    public class Bullet : MonoBehaviour {
        // Constants
        private const float speed = 500F;
        private const float bulletSize = 75F;

        // Attributes
        private float xVel;
        private float yVel;
        private Astronaut astronaut;
        private bool isPen;

        void Start() {
            // Re-sizes the bullet
            transform.localScale = new Vector3(bulletSize, bulletSize, 0);

            // Calculates the velocities
            float angle = transform.localEulerAngles.z * Mathf.Deg2Rad;
            xVel = speed * Mathf.Cos(angle);
            yVel = speed * Mathf.Sin(angle);
        
            astronaut = GameObject.Find(SpriteNames.Astronaut.GetString()).GetComponent<Astronaut>();
            isPen = astronaut.getLaser();
        }

        void Update() {
            //Movement
            Vector3 pos = transform.position;
            pos.x += Time.deltaTime * xVel;
            pos.y += Time.deltaTime * yVel;
            transform.position = pos;
        }

        void OnBecameInvisible() {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.name.Equals(SpriteNames.AlienDrone.GetString()) && !isPen) {
                Destroy(gameObject);
            }
        }
    }
}
