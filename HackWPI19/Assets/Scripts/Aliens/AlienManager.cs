using System;
using Enums;
using UnityEngine;

namespace Aliens {
    public class AlienManager : MonoBehaviour {
        // Unity objects
        public AudioClip soundByte;
        
        // Attributes
        private AudioSource source;

        void Awake() {
            source = GetComponent<AudioSource>();
        }

        public void deathNoise(float alienVolume) {
            source.PlayOneShot(soundByte, alienVolume);
        }
    }
}
