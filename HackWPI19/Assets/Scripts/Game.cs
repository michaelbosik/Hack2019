using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    // Unity objects
    public GameObject alien_0;
    public AudioClip soundByte;
    public Text txtScore, txtWave, txtLeft, txtHealth;

    // Constants
    private const int alienRate = 3;
    private const int alienInit = 5;
    private const float powerRate = 0.25f;
    private const float powerInit = 0.5f;
    private const float buffer = 250f;
    private const float zBuff = 10f;
    private const float alienVolume = 0.01f;

    // Attributes
    private static AudioSource source;
    private int wave;
    public static int score;
    private List<GameObject> lstAliens;
    private static float right, left, up, down;
    private Player player;
    private PowerUpManager powerUpManager;

    void Awake() {
        source = GetComponent<AudioSource>();
        source.volume = 0.25f;
    }

    void Start() {
        // Initialize variables
        wave = 0;
        score = 0;
        lstAliens = new List<GameObject>();
        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        right = screenSize.x;
        left = 0;
        up = screenSize.y;
        down = 0;
        player = GameObject.Find("Astronaut").GetComponent<Player>();
        powerUpManager = GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>();
    }

    void Update() {
        // HUD text
        txtScore.text = "Score: " + score;
        txtLeft.text = "Aliens Left: " + lstAliens.Count;
        txtHealth.text = "Health: " + player.getHealth();

        // Quit
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("End");
        }

        // Next wave
        if (lstAliens.Count == 0) {
            // Spawn aliens
            int numA = alienRate * wave + alienInit;
            for (int i = 0; i < numA; i++) {
                (float x, float y) = randomSpawn(-1);
                Vector3 pos = new Vector3(x, y, zBuff);
                GameObject newAlien = Instantiate(alien_0, pos, Quaternion.identity);
                lstAliens.Add(newAlien);
            }

            // Spawn power-ups
            int numP = (int) (powerRate * wave + powerInit);
            for (int i = 0; i < numP; i++) {
                (float x, float y) = randomSpawn(-1);
                Vector3 pos = new Vector3(x, y, zBuff);
                GameObject newPowerUp = powerUpManager.rdmPowerUp();
                Instantiate(newPowerUp, pos, Quaternion.identity);
            }
            
            // Update wave
            txtWave.text = "Wave: " + ++wave;
        }

        // Remove dead aliens
        for (int i = 0; i < lstAliens.Count; i++) {
            if (lstAliens[i] == null) {
                lstAliens.RemoveAt(i);
                source.PlayOneShot(soundByte, alienVolume);
            }
        }
    }

    public static (float, float) randomSpawn(int avoid) {
        List<int> lstEdges = new List<int> { 0, 1, 2, 3 };
        if (avoid != -1) {
            lstEdges.RemoveAt(avoid);
        }
        float x, y;
        int coin = lstEdges[Random.Range(0, lstEdges.Count)];
        switch (coin) {
            case 0: // Up
                x = Random.Range(left, right);
                y = Random.Range(up, up + buffer);
                break;
            case 1: // Right
                x = Random.Range(right, right + buffer);
                y = Random.Range(down, up);
                break;
            case 2: // Down
                x = Random.Range(left, right);
                y = Random.Range(down - buffer, down);
                break;
            default: // Left
                x = Random.Range(left - buffer, left);
                y = Random.Range(down, up);
                break;
        }
        return (x, y);
    }

    public static int findEdge(float x, float y) {
        if (y >= up) {
            return 0; // Up
        } else if (x >= right) {
            return 1; // Right
        } else if (y <= down) {
            return 2; // Down
        } else {
            return 3; // Left
        }
    }

    // Radian [-pi, pi] to degrees [0, 360]
    public static float transformAngle(float angle) {
        angle *= Mathf.Rad2Deg;
        if (angle < 0) {
            angle += 360;
        }
        return angle;
    }
}
