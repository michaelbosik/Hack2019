using UnityEngine;

namespace PowerUps {
    public class Timer : MonoBehaviour {
        // Attributes
        private Animator animator;
        
        void Start() {
            animator = gameObject.GetComponent<Animator>();
            animator.Play("Timer", -1, 0f);
        }

        void Update() {
            
        }
    }
}