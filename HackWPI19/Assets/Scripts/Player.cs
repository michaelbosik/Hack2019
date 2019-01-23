﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject bullet;
    public HealthBar serialBar;
    public AudioClip pewSound;
    private AudioSource source;

    private const float kickBack = 10F;
    private const float totalHealth = 1f;
    private const float damage = 0.1f;

    private Vector3 healthBarPos;
    private float mouseAngle;
    private float astroAngle;
    private bool lookRight;
    private float xVel;
    private float yVel;
    private float health;
    private HealthBar healthBar;
    private float right;
    private float left;
    private float up;
    private float down;

    void Awake() {
        source = GetComponent<AudioSource>();
    }

    void Start() {
        //Health
        healthBarPos = new Vector3(0, 0, 0);
        healthBar = Instantiate(serialBar, healthBarPos, transform.localRotation);
        health = totalHealth;
        healthBar.setSize(health);
        healthBar.setColor(Color.green);

        // Initialize variables
        mouseAngle = 0F;
        astroAngle = 0F;
        lookRight = true;
        xVel = 0F;
        yVel = 0F;

        // Screen size
        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        right = screenSize.x;
        left = 0;
        up = screenSize.y;
        down = 0;
    }

    void Update() {
        // Angle of mouse
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.GetChild(0).position);
        float x = Input.mousePosition.x - screenPos.x;
        float y = Input.mousePosition.y - screenPos.y;
        mouseAngle = Mathf.Atan2(y, x);

        // Left click - Fire
        if (Input.GetMouseButtonDown(0)) {
            xVel += kickBack * -1 * Mathf.Cos(mouseAngle);
            yVel += kickBack * -1 * Mathf.Sin(mouseAngle);
            Instantiate(bullet, transform.GetChild(0).GetChild(0).position, Quaternion.Euler(0, 0, transform.GetChild(0).localRotation.z + astroAngle));
            source.PlayOneShot(pewSound);
        }

        // Player movement
        movePlayer();

        // Health bar
        if (health > (0.75) * totalHealth) {
            healthBar.setColor(Color.green);
        } else if (health > (0.5) * totalHealth) {
            healthBar.setColor(Color.yellow);
        } else if (health > 0) {
            healthBar.setColor(Color.red);
        } else {
            SceneManager.LoadScene("End");
        }
        healthBar.setSize(health);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "GG(Clone)")
            health -= damage;
    }

    private void movePlayer() {
        checkBounce();
        if ((mouseAngle >= -1 * Mathf.PI / 2) && (mouseAngle <= Mathf.PI / 2)) { // right
            lookRight = true;
            if (mouseAngle >= 0) { // [0, pi/2]
                astroAngle = mouseAngle * 180 / Mathf.PI / 2;
            } else { // [-pi/2, 0)
                astroAngle = 360 + mouseAngle * 180 / Mathf.PI / 2;
            }
            transform.localRotation = Quaternion.Euler(0, 0, astroAngle);
        } else { // left
            lookRight = false;
            if (mouseAngle > 0) { // (pi/2, pi] 
                astroAngle = 90 - mouseAngle * 180 / Mathf.PI / 2;
            } else { // [-pi, -pi/2)
                astroAngle = -1 * (mouseAngle * 180 / Mathf.PI + 180) / 2;
            }
            transform.localRotation = Quaternion.Euler(0, 180, astroAngle);
        }
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * xVel;
        pos.y += Time.deltaTime * yVel;
        transform.position = pos;
        Debug.Log("Position: " + pos);
        Debug.Log("xVel: " + xVel);
        Debug.Log("yVel: " + yVel);

        //Change health pos
        healthBarPos = new Vector3(transform.position.x, transform.position.y - 18, transform.position.z);
        healthBar.transform.position = healthBarPos;

        if (lookRight) {
            transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, mouseAngle * 180 / Mathf.PI - astroAngle);
        } else {
            transform.GetChild(0).transform.localRotation = Quaternion.Euler(0, 0, -1 * (mouseAngle * 180 / Mathf.PI) + 180 - astroAngle);
        }
    }

    private void checkBounce() {
        Vector3 astroPos = transform.position;
        if ((astroPos.x < left) || (astroPos.x > right)) {
            if (astroPos.x < left) {
                transform.position = new Vector3(left, transform.position.y, transform.position.z);
            } else {
                transform.position = new Vector3(right, transform.position.y, transform.position.z);
            }
            xVel *= -0.5F;
            health -= damage;
        }
        if ((astroPos.y < down) || (astroPos.y > up)) {
            if (astroPos.y < down) {
                transform.position = new Vector3(transform.position.x, down, transform.position.z);
            } else {
                transform.position = new Vector3(transform.position.x, up, transform.position.z);
            }
            yVel *= -0.5F;
            health -= damage;
        }
    }
}
