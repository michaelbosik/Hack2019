using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour {

    public AudioClip soundByte;
    [SerializeField] GameObject gg_0;
    private static AudioSource source;

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
    public static int numLeft;
    public static int score;

    void Awake() {
        source = GetComponent<AudioSource>();
    }

    void Start() {
        // Initialize variables
        time = 0F;
        lvl = 0;
        numLeft = 0;
        score = 0;
    }

    void Update() {
        System.Random rnNoise = new System.Random();
        int noise = rnNoise.Next(0, noiseOdds);
        if (noise == 0) {
            source.PlayOneShot(soundByte);
        }
        // Quit
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
        if (numLeft == 0) {
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
                        x = rndm.Next(right + buffer);
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
                Instantiate(gg_0, pos, Quaternion.identity);
                numLeft++;
            }
            lvl++;
        }
    }
}
