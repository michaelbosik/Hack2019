using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private const float speed = 500F;
    private const float bulletSize = 75F;

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

    public void penetrate() {
        isPen = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if ((collision.gameObject.name == "GG(Clone)") && !isPen) {
            Destroy(gameObject);
        }
    }
}
