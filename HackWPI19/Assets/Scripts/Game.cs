using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public AudioClip soundByte;
    [SerializeField] GameObject gg_0;
    private static AudioSource source;
    public GameObject txtScore;
    public GameObject txtWave;
    public GameObject txtLeft;

    private const int rate = 3;
    private const int init = 5;
    private const int noiseOdds = 100;
    private const int right = 1000;
    private const int left = 0;
    private const int up = 500;
    private const int down = 0;
    private const int buffer = 250;
    private const int zBuff = 10;

    private float time;
    private int lvl;
    public static int score;
    private List<GameObject> lstGloup;

    void Awake() {
        source = GetComponent<AudioSource>();
    }

    void Start() {
        // Initialize variables
        time = 0F;
        lvl = 0;
        score = 0;
        lstGloup = new List<GameObject>();
    }

    void Update() {
        // Noise
        System.Random rnNoise = new System.Random();
        int noise = rnNoise.Next(0, noiseOdds);
        if (noise == 0) {
            source.PlayOneShot(soundByte);
        }

        // HUD text
        txtScore.GetComponent<Text>().text = "Score: " + score;
        txtWave.GetComponent<Text>().text = "Wave: " + lvl;
        txtLeft.GetComponent<Text>().text = "Gloups Left: " + lstGloup.Count;

        // Quit
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene("End");
        }

        // Spawn Gloups
        if (lstGloup.Count == 0) {
            int numG = rate * lvl + init;
            System.Random rndm = new System.Random();
            for (int i = 0; i < numG; i++) {
                int x, y;
                int coin = rndm.Next(0, 4);
                switch (coin) {
                    case 0: // Up
                        x = rndm.Next(left, right);
                        y = rndm.Next(up, up + buffer);
                        break;
                    case 1: // Right
                        x = rndm.Next(right, right + buffer);
                        y = rndm.Next(down, up);
                        break;
                    case 2: // Down
                        x = rndm.Next(left, right);
                        y = rndm.Next(down - buffer, down);
                        break;
                    default: // Left
                        x = rndm.Next(left - buffer, left);
                        y = rndm.Next(down, up);
                        break;
                }
                Vector3 pos = new Vector3(x, y, zBuff);
                Debug.Log("Spawn " +  coin + ": (" + x + ", " + y + ")");
                GameObject newGloup = Instantiate(gg_0, pos, Quaternion.identity);
                lstGloup.Add(newGloup);
            }
            lvl++;
        }

        // Remove dead gloups
        for (int i = 0; i < lstGloup.Count; i++) {
            if (lstGloup[i] == null) {
                lstGloup.RemoveAt(i);
            }
        }
    }
}
