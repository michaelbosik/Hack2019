using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour {
    private const float size = 100F;
    private const float speed = 25F;
    private const float health = 0.1F;

    private float xVel, yVel;

    void Start() {
        transform.localScale = new Vector3(size, size, 0);

        float curX = transform.position.x;
        float curY = transform.position.y;
        int edge = Game.findEdge(curX, curY);
        (float destX, float destY) = Game.randomSpawn(edge);
        float x = destX - curX;
        float y = destY - curY;
        
        float angle = Mathf.Atan2(y, x);

        xVel = speed * Mathf.Cos(angle);
        yVel = speed * Mathf.Sin(angle);
    }

    void Update() {
        Vector3 pos = transform.position;

        pos.x += Time.deltaTime * xVel;
        pos.y += Time.deltaTime * yVel;
        transform.position = pos;
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Astronaut") {
            GameObject player = GameObject.Find("Astronaut");
            player.GetComponent<Player>().addHealth(health);
            Destroy(gameObject);
        }
    }
}
