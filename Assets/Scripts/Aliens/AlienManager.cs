using System.Collections.Generic;
using Scenes;
using UnityEngine;

namespace Aliens {
    public class AlienManager : MonoBehaviour {
        // Unity objects
        public AudioClip deathNoise;
        
        // Constants
        private const float buffer = 250f;

        // Attributes
        private AudioSource source;
        private static float top, right, bottom, left;

        void Awake() {
            source = GetComponent<AudioSource>();
        }

        void Start() {
            // Bounds
            (top, right, bottom, left) = Game.getBounds();
        }
        
        public static (float, float) randomSpawn() {
            float x, y;
            int side = Random.Range(0, 4);
            switch (side) {
                case 0: // Up
                    x = Random.Range(left, right);
                    y = Random.Range(top, top + buffer);
                    break;
                case 1: // Right
                    x = Random.Range(right, right + buffer);
                    y = Random.Range(bottom, top);
                    break;
                case 2: // Down
                    x = Random.Range(left, right);
                    y = Random.Range(bottom - buffer, bottom);
                    break;
                default: // Left
                    x = Random.Range(left - buffer, left);
                    y = Random.Range(bottom, top);
                    break;
            }
            return (x, y);
        }

        public void playDeathNoise(float alienVolume) {
            source.PlayOneShot(deathNoise, alienVolume);
        }
    }
}
