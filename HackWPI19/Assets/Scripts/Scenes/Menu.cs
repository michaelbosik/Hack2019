using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes {
    public class Menu : MonoBehaviour {

        public void StartGame() {
            SceneManager.LoadScene(SceneNames.Space.GetString());
        }

        public void Quit() {
            Application.Quit();
        }
    }
}
