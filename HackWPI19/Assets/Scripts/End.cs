using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour {

    public GameObject txtScore;

    void Start() {
        int score = Game.score;
        txtScore.GetComponent<Text>().text = "Score: " + score;
    }
}
