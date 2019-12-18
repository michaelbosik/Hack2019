using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public Bullet bullet;
    public HealthBar serialBar;
    public AudioClip pewSound;
    private AudioSource source;

    private const float kickBack = 10F;
    private const float totalHealth = 1F;
    private const float damage = 0.1F;
    private const float shotGunAngle = 5F;

    private Vector3 healthBarPos;
    private float mouseAngle, gunAngle, astroAngle; // Degrees
    private float xVel, yVel;
    private float health;
    private HealthBar healthBar;
    private float right, left, up, down;
    private float shotTimer, shotLimit;
    private bool isShotgun, isLaser, isBounce;

    void Awake() {
        source = GetComponent<AudioSource>();
    }

    void Start() {
        // Health
        healthBarPos = new Vector3(0, 0, 0);
        healthBar = Instantiate(serialBar, healthBarPos, transform.localRotation);
        health = totalHealth;
        healthBar.setSize(health);
        healthBar.setColor(Color.green);

        // Initialize variables
        gunAngle = 0F;
        astroAngle = 0F;
        xVel = 0F;
        yVel = 0F;

        // Screen size
        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        right = screenSize.x;
        left = 0F;
        up = screenSize.y;
        down = 0F;

        // Bullet
        shotTimer = 0;
        shotLimit = 0.25F;
        isShotgun = false;
        isLaser = false;
        isBounce = false;
    }

    void Update() {
        // Left-click
        if (Input.GetMouseButtonDown(0)) {
            shotTimer = 0;
            shootBullet(isShotgun);
        }
        if (Input.GetMouseButton(0)) {
            if (shotTimer < shotLimit) {
                shotTimer += Time.deltaTime;
            } else {
                shotTimer = 0;
                shootBullet(isShotgun);
            }
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
        string alien = "Alien";
        if (collision.gameObject.name.Substring(0, alien.Length).Equals(alien)) {
            health -= damage;
        }
    }

    public int getHealth() {
        return (int) Mathf.Round(health * 100);
    }

    public void addHealth(float add) {
        health += add;
        if (health > totalHealth) {
            health = totalHealth;
        }
    }
     
    public void setShotgun(bool isShotgun_) {
        isShotgun = isShotgun_;
    }

    public void setLaser(bool isLaser_) {
        isLaser = isLaser_;
    }

    public bool getLaser() {
        return isLaser;
    }

    public void setRapidFire(bool isRapidFire, float multiplier) {
        if (isRapidFire) {
            shotLimit /= multiplier;
        } else {
            shotLimit *= multiplier;
        }
        Debug.Log(shotLimit);
    }

    public void setBounce(bool isBounce_) {
        isBounce = isBounce_;
    }

    private void movePlayer() {
        // Checks boundaries
        checkBounce();

        // Rotate player and arm/gun
        updateAngle();

        // Updates position
        Vector3 pos = transform.position;
        pos.x += Time.deltaTime * xVel;
        pos.y += Time.deltaTime * yVel;
        transform.position = pos;

        //Change health pos
        healthBarPos = new Vector3(transform.position.x, transform.position.y - 18, transform.position.z);
        healthBar.transform.position = healthBarPos;
    }

    private void checkBounce() {
        Vector3 astroPos = transform.position;
        if ((astroPos.x < left) || (astroPos.x > right)) {
            if (astroPos.x < left) {
                transform.position = new Vector3(left, transform.position.y, transform.position.z);
            } else {
                transform.position = new Vector3(right, transform.position.y, transform.position.z);
            }
            if (!isBounce) {
                health -= damage;
                xVel *= -0.5F;
            } else {
                xVel *= -1f;
            }
        }
        if ((astroPos.y < down) || (astroPos.y > up)) {
            if (astroPos.y < down) {
                transform.position = new Vector3(transform.position.x, down, transform.position.z);
            } else {
                transform.position = new Vector3(transform.position.x, up, transform.position.z);
            }
            if (!isBounce) {
                health -= damage;
                yVel *= -0.5F;
            } else {
                yVel *= -1f;
            }
        }
    }

    private void updateAngle() {
        // Angle of mouse
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.GetChild(0).position);
        float x = Input.mousePosition.x - screenPos.x;
        float y = Input.mousePosition.y - screenPos.y;
        mouseAngle = Game.transformAngle(Mathf.Atan2(y, x));

        // Angle of movement
        astroAngle = Game.transformAngle(Mathf.Atan2(yVel, xVel));
        if ((astroAngle > 90) && (astroAngle < 270)) {
            astroAngle = astroAngle - 180;
        }

        // Angle of gun
        gunAngle = mouseAngle - astroAngle;
        gunAngle = gunAngle >= 0 ? gunAngle : gunAngle + 360;

        // Transform sprites
        if ((gunAngle <= 90) || (gunAngle >= 270)) {
            transform.localRotation = Quaternion.Euler(0, 0, astroAngle);
            transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, gunAngle);
        } else {
            transform.localRotation = Quaternion.Euler(0, 180, 360 - astroAngle);
            transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 180 - gunAngle);
        }
    }

    private void shootBullet(bool triple) {
        Vector3 bullPos = transform.GetChild(0).GetChild(0).position;
        Quaternion bullAngle = Quaternion.Euler(0, 0, mouseAngle);

        xVel += -1 * kickBack * Mathf.Cos(mouseAngle * Mathf.Deg2Rad);
        yVel += -1 * kickBack * Mathf.Sin(mouseAngle * Mathf.Deg2Rad);
        Instantiate(bullet, bullPos, bullAngle);
        if (triple) {
            xVel += -2 * Mathf.Cos(shotGunAngle * Mathf.Deg2Rad) * kickBack * Mathf.Cos(mouseAngle * Mathf.Deg2Rad);
            yVel += -2 * Mathf.Cos(shotGunAngle * Mathf.Deg2Rad) * kickBack * Mathf.Sin(mouseAngle * Mathf.Deg2Rad);
            Quaternion upShot = Quaternion.Euler(0, 0, shotGunAngle);
            Quaternion downShot = Quaternion.Euler(0, 0, -shotGunAngle);
            Instantiate(bullet, bullPos, bullAngle * upShot);
            Instantiate(bullet, bullPos, bullAngle * downShot);
        }
        source.PlayOneShot(pewSound);
    }
}
