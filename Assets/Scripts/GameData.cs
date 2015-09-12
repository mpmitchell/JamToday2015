using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour {
    [SerializeField] private Text scoreText_;

    private int score_;

    private void Score() {
        score_++;
        scoreText_.text = score_.ToString();
    }

    private void Fail() {
        score_--;
        scoreText_.text = score_.ToString();
    }
}
