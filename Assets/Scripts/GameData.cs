using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour {
    [SerializeField] private Text scoreText_;

    private int score_;

    private void Score() {
        scoreText_.text = score_++.ToString();
    }

    private void Fail() {
        scoreText_.text = score_--.ToString();
    }
}
