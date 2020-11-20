using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes {
    public class End : MonoBehaviour {

        private const string fileName = "data.txt";

        public GameObject txtScore, txtHighScore, txtCoins;

        void Start() {
            // Display score
            int score = Game.score;
            txtScore.GetComponent<Text>().text = "Score: " + score;
            
            // Get collected coins
            int newCoins = Game.coins;

            // Read from file
            int highScore, coins;
            try {
                String strScore, strCoins;
                using (StreamReader sr = new StreamReader(fileName)) {
                    strScore = sr.ReadLine();
                    highScore = Int32.Parse(strScore);
                    strCoins = sr.ReadLine();
                    coins = Int32.Parse(strCoins);
                }
            } catch (Exception) {
                highScore = 0;
                coins = 0;
            }

            // Update and display high score
            if (score > highScore) {
                highScore = score;
            }

            int totalCoins = coins + newCoins;
            txtHighScore.GetComponent<Text>().text = "High Score: " + highScore;
            txtCoins.GetComponent<Text>().text = "Coins: " + totalCoins;
            
            // Write to data file
            using (StreamWriter sw = new StreamWriter(fileName)) {
                sw.WriteLine(highScore);
                sw.WriteLine(totalCoins);
            }
        }
    }
}
