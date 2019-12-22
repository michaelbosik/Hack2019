using Enums;
using UnityEngine;

namespace Player {
    public class Bubble : MonoBehaviour {
        // Constants
        private const float size = 15f;
        
        // Attributes
        private GameObject astronaut;
        
        void Start() {
            astronaut = GameObject.Find(SpriteNames.Astronaut.GetString());
            transform.localScale = new Vector3(size, size, 0);
        }

        void Update() {
            Vector3 pos = astronaut.transform.position;
            gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);
        }

        public void destroy() {
            Destroy(gameObject);
        }
    }
}