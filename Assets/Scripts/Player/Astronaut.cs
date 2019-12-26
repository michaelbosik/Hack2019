using Enums;
using Scenes;
using UnityEngine;

namespace Player {
    public class Astronaut : MonoBehaviour {
        // Unity objects
        public GameObject bullet, serialBar, bubble;

        // Constants
        private const float kickBack = 15f;
        private const float hitCoolDown = 0.5f;
        private const float totalHealth = 1f;
        private const float damage = 0.1f;
        private const float shotGunAngle = 5f;
        private const int healthBarOffset = 30;

        // Attributes
        private PlayerManager playerManager;
        private Vector3 healthBarPos;
        private float mouseAngle, gunAngle, astroAngle; // Degrees
        private float xVel, yVel;
        private float coolDownTimer;
        private float health;
        private HealthBar healthBar;
        private float top, right, bottom, left;
        private float shotTimer, shotLimit;
        private bool isShotgun, isLaser, isBounce;
        private Bubble bubbleAstro;

        void Start() {
            playerManager = GameObject.Find(ScriptNames.PlayerManager.GetString()).GetComponent<PlayerManager>();
            
            // Health
            coolDownTimer = 0;
            healthBarPos = new Vector3(0, 0, 0);
            healthBar = Instantiate(serialBar, healthBarPos, transform.localRotation).GetComponent<HealthBar>();
            health = totalHealth;
            healthBar.setSize(health);
            healthBar.setColor(Color.green);

            // Initialize variables
            gunAngle = 0F;
            astroAngle = 0F;
            xVel = 0F;
            yVel = 0F;

            // Screen size
            (top, right, bottom, left) = Game.getBounds();

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
                playerManager.astronautDeath(xVel, yVel);
                healthBar.destroy();
                Destroy(gameObject);
            }
            healthBar.setSize(health);
        }

        void OnCollisionEnter2D(Collision2D collision) {
            string colObj = collision.gameObject.name;
            if (colObj.Equals(SpriteNames.AlienDrone.GetString()) || colObj.Equals(SpriteNames.AlienKing.GetString()) ||
                colObj.Equals(SpriteNames.AlienGunner.GetString()) || colObj.Equals(SpriteNames.Blast.GetString())) {
                health -= damage;
                coolDownTimer = 0;
            }
        }

        void OnCollisionStay2D(Collision2D collision) {
            string colObj = collision.gameObject.name;
            if (colObj.Equals(SpriteNames.AlienDrone.GetString()) || colObj.Equals(SpriteNames.AlienKing.GetString()) ||
                colObj.Equals(SpriteNames.AlienGunner.GetString())) {
                coolDownTimer += Time.deltaTime;
                if (coolDownTimer > hitCoolDown) {
                    health -= damage;
                    coolDownTimer = 0;
                }
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
        }

        public void setBounce(bool isBounce_) {
            isBounce = isBounce_;
            if (isBounce_) {
                if (bubbleAstro == null) {
                    bubbleAstro = Instantiate(bubble, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Bubble>();
                }
            } else {
                bubbleAstro.destroy();
            }
        }

        private void movePlayer() {
            // Checks boundaries
            checkBounce();

            // Rotate player and arm/gun
            updateAngle();

            // Updates position
            Transform tf = transform;
            Vector3 pos = tf.position;
            pos.x += Time.deltaTime * xVel;
            pos.y += Time.deltaTime * yVel;
            tf.position = pos;

            // Change health pos
            healthBarPos = new Vector3(pos.x, pos.y - healthBarOffset, pos.z);
            healthBar.transform.position = healthBarPos;
        }

        private void checkBounce() {
            Transform tf = transform;
            Vector3 pos = tf.position;
            if (pos.x < left || pos.x > right) {
                float xPos = pos.x < left ? left : right;
                tf.position = new Vector3(xPos, pos.y, pos.z);
                if (!isBounce) {
                    health -= damage;
                    xVel *= -0.5F;
                } else {
                    xVel *= -1f;
                }
            }
            if (pos.y < bottom || pos.y > top) {
                float yPos = pos.y < bottom ? bottom : top;
                tf.position = new Vector3(pos.x, yPos, pos.z);
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
        }
    }
}
