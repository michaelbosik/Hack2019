using Enums;
using UnityEngine;

namespace Player {
    public class Bubble : MonoBehaviour {
        // Constants
        private const float size = 10f;
        
        // Attributes
        private GameObject astronaut;
        
        void Start() {
            astronaut = GameObject.Find(SpriteNames.Astronaut.GetString());
            transform.localScale = new Vector3(size, size, 0);
        }

        void Update() {
            if (astronaut != null) {
                gameObject.transform.position = astronaut.transform.position;
            } else {
                Destroy(gameObject);
            }
        }

        public void destroy() {
            Destroy(gameObject);
        }
    }
}