using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    [SerializeField] GameObject bullet;
    [SerializeField] private HealthBar healthBar;

    private const float kickBack = 10F;

    private bool pause;

    private Vector3 healthBarPos = new Vector3(0, 0, 0);
    private float angle;
    private bool lookRight;
    private float xVel;
    private float yVel;
    private float health;

    void Start() {
        pause = false;

        //Health
        Instantiate(healthBar, healthBarPos, transform.localRotation);
        health = 1f;
        healthBar.setSize(health);
        healthBar.setColor(Color.green);

        angle = 0F;
        lookRight = true;
        xVel = 0F;
        yVel = 0F;
    }

    void Update() {
        // Angle of mouse
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float x = Input.mousePosition.x - screenPos.x;
        float y = Input.mousePosition.y - screenPos.y;
        angle = Mathf.Atan2(y, x);

        if (Input.GetKey(KeyCode.Escape)) {
            //pause = !pause;
        }

        if (!pause) {
            // Left click
            if (Input.GetMouseButtonDown(0)) {
                xVel += kickBack * -1 * Mathf.Cos(angle);
                yVel += kickBack * -1 * Mathf.Sin(angle);
                Instantiate(bullet, transform.position, transform.GetChild(0).localRotation);
            }

            movePlayer();
        }

        if(health < 0f)
            SceneManager.LoadScene(sceneName: "Menu");

        healthBar.setColor(Color.green);

        if (health < .75f)
            healthBar.setColor(Color.yellow);
        if (health < .25f)
            healthBar.setColor(Color.red);

        healthBar.setSize(health);

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "GG(Clone)")
            health -= .1f;
    }

    private void movePlayer() {
        if ((angle > -1 * Mathf.PI / 2) && (angle <= Mathf.PI / 2)) {
            if (!lookRight) {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                lookRight = true;
            }
        } else {
            if (lookRight) {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                lookRight = false;
            }
        }
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * xVel;
        pos.y += Time.deltaTime * yVel;
        transform.position = pos;

        //Change health pos
        healthBarPos = new Vector3(transform.position.x, transform.position.y - 18, transform.position.z);
        healthBar.transform.position = healthBarPos;

        if (lookRight) {
            transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
        } else {
            transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, -1 * (angle * 180 / Mathf.PI) + 180);
        }
    }

    void OnBecameInvisible()
    {
        SceneManager.LoadScene(sceneName: "Menu");
    }
}
