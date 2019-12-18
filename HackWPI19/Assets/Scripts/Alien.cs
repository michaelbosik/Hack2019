using UnityEngine;

public class Alien : MonoBehaviour {
    // Constants
    private const float alienSpeed = 45F;
    private const float alienSpeedDev = 10F;
    private const float alienSize = 50F;
    private const float alienASizeDev = 10F;

    // Attributes
    private float rdmSpeed;

    void Start() {
        // Create alien with random size
        float rdmSize = Random.Range(alienSize - alienASizeDev, alienSize + alienASizeDev);
        transform.localScale = new Vector3(rdmSize, rdmSize, 0);

        // Create random speed for alien
        rdmSpeed = Random.Range(alienSpeed - alienSpeedDev, alienSpeed + alienSpeedDev);
    }

    void Update() {
        // Tracks player
        GameObject player = GameObject.Find("Astronaut");

        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        float angle = Mathf.Atan2(y, x);
        float xVel = rdmSpeed * Mathf.Cos(angle);
        float yVel = rdmSpeed * Mathf.Sin(angle);
        Vector3 pos = transform.position;

        pos.x += Time.deltaTime * xVel;
        pos.y += Time.deltaTime * yVel;
        transform.position = pos;
        transform.localRotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // If collides with a bullet or the player, die
        if (collision.gameObject.name.Equals("Astronaut") || collision.gameObject.name.Equals("Bullet(Clone)")) {
            // If collides with bullet, increase score
            if (collision.gameObject.name.Equals("Bullet(Clone)")) {
                Game.score++;
            }
            Destroy(gameObject);
        }
    }
}