using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public GameObject gg_0;
    public AudioClip soundByte;
    public GameObject txtScore;
    public GameObject txtWave;
    public GameObject txtLeft;

    private const int rate = 3;
    private const int init = 5;
    private const float buffer = 250F;
    private const float zBuff = 10F;

    private static AudioSource source;
    private int wave;
    public static int score;
    private List<GameObject> lstGloup;
    private float right;
    private float left;
    private float up;
    private float down;

    void Awake() {
        source = GetComponent<AudioSource>();
    }

    void Start() {
        // Initialize variables
        wave = 0;
        score = 0;
        lstGloup = new List<GameObject>();
        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        right = screenSize.x;
        left = 0;
        up = screenSize.y;
        down = 0;
    }

    void Update() {
        // HUD text
        txtScore.GetComponent<Text>().text = "Score: " + score;
        txtWave.GetComponent<Text>().text = "Wave: " + wave;
        txtLeft.GetComponent<Text>().text = "Gloups Left: " + lstGloup.Count;

        // Quit
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("End");
        }

        // Spawn Gloups
        if (lstGloup.Count == 0) {
            int numG = rate * wave + init;
            for (int i = 0; i < numG; i++) {
                float x, y;
                int coin = Random.Range(0, 3);
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
                Vector3 pos = new Vector3(x, y, zBuff);
                GameObject newGloup = Instantiate(gg_0, pos, Quaternion.identity);
                lstGloup.Add(newGloup);
            }
            wave++;
        }

        // Remove dead gloups
        for (int i = 0; i < lstGloup.Count; i++) {
            if (lstGloup[i] == null) {
                lstGloup.RemoveAt(i);
                source.PlayOneShot(soundByte);
            }
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
