using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] private GameObject mainMenu_;
    [SerializeField] private GameObject miniGameMenu_;

    public void PlayGamesButton() {
        mainMenu_.SetActive(false);
        miniGameMenu_.SetActive(true);
    }

    public void ExitButton() {
        Application.Quit();
    }

    public void BackButton() {
        miniGameMenu_.SetActive(false);
        mainMenu_.SetActive(true);
    }

    public void Game1Button() {
        Application.LoadLevel(1);
    }

    public void Game2Button() {
        Application.LoadLevel(2);
    }
}
