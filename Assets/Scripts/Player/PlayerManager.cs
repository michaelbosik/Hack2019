using Enums;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player {
    public class PlayerManager : MonoBehaviour {
        // Unity objects
        public AudioClip bulletShot;
        public GameObject head, torso, leftLeg, rightLeg, arm, armBehind;
        
        // Constants
        private const float deathLimit = 5f;
        
        // Attributes
        private AudioSource source;
        private GameObject astronaut;
        private bool isDead;
        private float deathTimer;
        private float xVel_, yVel_;

        void Awake() {
            source = GetComponent<AudioSource>();
        }

        void Start() {
            astronaut = GameObject.Find(SpriteNames.Astronaut.GetString());
            isDead = false;
            deathTimer = 0;
            xVel_ = 0;
            yVel_ = 0;
        }

        void Update() {
            if (isDead) {
                deathTimer += Time.deltaTime;
                if (deathTimer > deathLimit) {
                    Game.endGame();
                }
            }
        }

        public void playBulletShot(float bulletVolume) {
            source.PlayOneShot(bulletShot, bulletVolume);
        }

        public void astronautDeath(float xVel, float yVel) {
            isDead = true;
            xVel_ = xVel;
            yVel_ = yVel;
            
            Vector3 astroPos = astronaut.transform.position;
            GameObject[] bodyParts = {head, torso, leftLeg, rightLeg, arm, armBehind};
            foreach (GameObject bodyPart in bodyParts) {
                Instantiate(bodyPart, astroPos, Quaternion.identity);
            }
        }

        public (float, float) getVelocities() {
            return (xVel_, yVel_);
        }
    }
}