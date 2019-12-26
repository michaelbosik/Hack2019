﻿using Enums;
using UnityEngine;

namespace Player {
    public class Bullet : MonoBehaviour {
        // Constants
        private const float speed = 500f;
        private const float bulletSize = 2.5f;
        private const float bulletVolume = 0.01f;

        // Attributes
        private float xVel, yVel;
        private Astronaut astronaut;
        private bool isPen;
        private PlayerManager playerManager;

        void Start() {
            Transform tf = transform;
            
            // Re-sizes the bullet
            tf.localScale = new Vector3(bulletSize, bulletSize, 0);

            // Calculates the velocities
            float angle = tf.localEulerAngles.z * Mathf.Deg2Rad;
            xVel = speed * Mathf.Cos(angle);
            yVel = speed * Mathf.Sin(angle);
        
            astronaut = GameObject.Find(SpriteNames.Astronaut.GetString()).GetComponent<Astronaut>();
            isPen = astronaut.getLaser();
            
            playerManager = GameObject.Find(ScriptNames.PlayerManager.GetString()).GetComponent<PlayerManager>();
            playerManager.playBulletShot(bulletVolume);
        }

        void Update() {
            //Movement
            Transform tf = transform;
            Vector3 pos = tf.position;
            pos.x += Time.deltaTime * xVel;
            pos.y += Time.deltaTime * yVel;
            tf.position = pos;
        }

        void OnBecameInvisible() {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if ((collision.gameObject.name.Equals(SpriteNames.AlienDrone.GetString()) ||
                 collision.gameObject.name.Equals(SpriteNames.AlienKing.GetString())) && !isPen) {
                Destroy(gameObject);
            }
        }
    }
}
