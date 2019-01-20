using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloup : MonoBehaviour {

    private const float speed = 50F;

    void Start() {
        transform.localScale = new Vector3(50F, 50F, 0);
    }

    void Update() {
        GameObject player = GameObject.Find("Astronaut");

        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        float angle = Mathf.Atan2(y, x);
        float xVel = speed * Mathf.Cos(angle);
        float yVel = speed * Mathf.Sin(angle);
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * xVel;
        pos.y += Time.deltaTime * yVel;
        transform.position = pos;
        transform.localRotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name != "GG(Clone)") {
            Game.numLeft--;
            if (collision.gameObject.name == "Bullet(Clone)") {
                Game.score++;
            }
            Destroy(gameObject);
        }
    }
}