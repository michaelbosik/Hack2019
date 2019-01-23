using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gloup : MonoBehaviour {

    private const float gloupSpeed = 45F;
    private const float gloupSpeedDev = 10F;
    private const float gloupSize = 50F;
    private const float gloupSizeDev = 10F;

    private float rndmSpeed;

    void Start() {
        // Create gloup with random size
        float rndmSize = Random.Range(gloupSize - gloupSizeDev, gloupSize + gloupSizeDev);
        Debug.Log("Gloup size: " + rndmSize);
        transform.localScale = new Vector3(rndmSize, rndmSize, 0);

        // Create random speed for gloup
        rndmSpeed = Random.Range(gloupSpeed - gloupSpeedDev, gloupSpeed + gloupSpeedDev);
    }

    void Update() {
        // Tracks player
        GameObject player = GameObject.Find("Astronaut");

        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        float angle = Mathf.Atan2(y, x);
        float xVel = rndmSpeed * Mathf.Cos(angle);
        float yVel = rndmSpeed * Mathf.Sin(angle);
        Vector3 pos = transform.position;

        pos.x += Time.deltaTime * xVel;
        pos.y += Time.deltaTime * yVel;
        transform.position = pos;
        transform.localRotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // If collides with a bullet or the player, die
        if (collision.gameObject.name != "GG(Clone)") {
            // If collides with bullet, increase score
            if (collision.gameObject.name == "Bullet(Clone)") {
                Game.score++;
            }
            Destroy(gameObject);
        }
    }
}