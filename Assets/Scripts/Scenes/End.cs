using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes {
    public class End : MonoBehaviour {

        private const string fileName = "data.txt";

        public GameObject txtScore;
        public GameObject txtHighScore;

        void Start() {
            // Display score
            int score = Game.score;
            txtScore.GetComponent<Text>().text = "Score: " + score;

            // Read from file
            int highScore;
            try {
                String strScore;
                using (StreamReader sr = new StreamReader(fileName)) {
                    strScore = sr.ReadLine();
                    highScore = Int32.Parse(strScore);
                }
            } catch (Exception) {
                highScore = 0;
            }

            // Update and display high score
            if (score > highScore) {
                highScore = score;
                using (StreamWriter sw = new StreamWriter(fileName)) {
                    sw.WriteLine(highScore);
                }
            }
            txtHighScore.GetComponent<Text>().text = "High Score: " + highScore;


        }
    }
}
