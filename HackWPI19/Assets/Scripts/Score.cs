using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public GameObject txtScore;

    void Start() {
        
    }

    void Update() {
        txtScore.GetComponent<UnityEngine.UI.Text>().text = " Score: " + Game.score;
    }
}
