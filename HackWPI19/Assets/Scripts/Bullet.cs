using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    // Constants
    private const float speed = 500F;
    private const float bulletSize = 75F;

    // Attributes
    private float xVel;
    private float yVel;
    private bool isPen = false;

    void Start() {
        // Re-sizes the bullet
        transform.localScale = new Vector3(bulletSize, bulletSize, 0);

        // Calculates the velocities
        float angle = transform.localEulerAngles.z * Mathf.Deg2Rad;
        xVel = speed * Mathf.Cos(angle);
        yVel = speed * Mathf.Sin(angle);
    }

    void Update() {
        //Movement
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * xVel;
        pos.y += Time.deltaTime * yVel;
        transform.position = pos;
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    public void togglePenetrate() {
        isPen = !isPen;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        string alien = "Alien";
        if (collision.gameObject.name.Substring(0, alien.Length).Equals(alien) && !isPen) {
            Destroy(gameObject);
        }
    }
}
