using UnityEngine;

namespace Player {
    public class PlayerManager : MonoBehaviour {
        // Unity objects
        public AudioClip bulletShot;
        
        // Attributes
        private AudioSource source;

        void Awake() {
            source = GetComponent<AudioSource>();
        }

        public void playBulletShot(float bulletVolume) {
            source.PlayOneShot(bulletShot, bulletVolume);
        }
    }
}