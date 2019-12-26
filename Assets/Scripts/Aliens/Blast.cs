using Enums;
using UnityEngine;

namespace Aliens {
    public class Blast : MonoBehaviour {
        // Constants
        private const float speed = 350f;
        private const float bulletSize = 2.5f;
        
        // Attributes
        private float xVel, yVel;
        
        void Start() {
            Transform tf = transform;
            tf.localScale = new Vector3(bulletSize, bulletSize, 0);
            
            float angle = tf.localEulerAngles.z * Mathf.Deg2Rad;
            xVel = speed * Mathf.Cos(angle);
            yVel = speed * Mathf.Sin(angle);
        }

        void Update() {
            Transform tf = transform;
            Vector3 pos = tf.position;
            pos.x += Time.deltaTime * xVel;
            pos.y += Time.deltaTime * yVel;
            tf.position = pos;
        }

        void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.name.Equals(SpriteNames.Astronaut.GetString())) {
                Destroy(gameObject);
            }
        }
        
        void OnBecameInvisible() {
            Destroy(gameObject);
        }
    }
}