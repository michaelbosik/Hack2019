using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloup : MonoBehaviour
{

    float speed = 50F;

    void Start() {
        transform.localScale = new Vector3(50F, 50F, 0);
    }

    void Update() {
        GameObject player = GameObject.Find("Astronaut");

        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        Debug.Log("X: " + x);
        Debug.Log("Y:" + y);
        float angle = Mathf.Atan2(y, x);
        float xVel = speed * Mathf.Cos(angle);
        float yVel = speed * Mathf.Sin(angle);
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * xVel;
        pos.y += Time.deltaTime * yVel;
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name != "GG(Clone)") {
            Game.numLeft--;
            Destroy(gameObject);
        }
    }
}