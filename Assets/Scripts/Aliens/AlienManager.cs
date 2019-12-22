using UnityEngine;

namespace Aliens {
    public class AlienManager : MonoBehaviour {
        // Unity objects
        public AudioClip deathNoise;
        
        // Attributes
        private AudioSource source;

        void Awake() {
            source = GetComponent<AudioSource>();
        }

        public void playDeathNoise(float alienVolume) {
            source.PlayOneShot(deathNoise, alienVolume);
        }
    }
}
