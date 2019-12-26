using Enums;
using Scenes;
using UnityEngine;

namespace Player {
    public class BodyPart : MonoBehaviour {
        // Constants
        private const float speedAvg = 20f;
        private const float speedDev = 20f;
        private const float rotAvg = 50f;
        private const float rotDev = 45f;
        private const float velReduce = 0.5f;
        
        // Attributes
        private PlayerManager playerManager;
        private float top, right, bottom, left;
        private float speed, rotSpeed, angle, xVel, yVel;

        void Start() {
            playerManager = GameObject.Find(ScriptNames.PlayerManager.GetString()).GetComponent<PlayerManager>();
            (float xAstroVel, float yAstroVel) = playerManager.getVelocities();
            
            (top, right, bottom, left) = Game.getBounds();
            
            float dev = Random.Range(-1f, 1f);
            speed = speedAvg + speedDev * dev;
            rotSpeed = rotAvg - rotDev * dev;
            angle = Random.Range(0f, 360f);
            xVel = velReduce * xAstroVel + speed * Mathf.Cos(angle * Mathf.Deg2Rad);
            yVel = velReduce * yAstroVel + speed * Mathf.Sin(angle * Mathf.Deg2Rad);
        }

        void Update() {
            float tDelta = Time.deltaTime;
            Transform tf = transform;
            
            checkBounce();
            Vector3 pos = tf.position;
            pos.x += xVel * tDelta;
            pos.y += yVel * tDelta;
            tf.position = pos;

            Quaternion dRot = Quaternion.Euler(0, 0, rotSpeed * tDelta);
            tf.localRotation *= dRot;
        }
        
        private void checkBounce() {
            Transform tf = transform;
            Vector3 pos = tf.position;
            if (pos.x < left || pos.x > right) {
                float xPos = pos.x < left ? left : right;
                tf.position = new Vector3(xPos, pos.y, pos.z);
                xVel *= -0.5F;
            }
            if (pos.y < bottom || pos.y > top) {
                float yPos = pos.y < bottom ? bottom : top;
                tf.position = new Vector3(pos.x, yPos, pos.z);
                yVel *= -0.5F;
            }
        }
    }
}