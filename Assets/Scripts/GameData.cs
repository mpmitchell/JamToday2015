using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour {
    [SerializeField] private Text scoreText_;
    [SerializeField] private int scorePerLevel_;

    [HideInInspector] public float halfLength_;

    private int score_;

    private void Score() {
        score_++;
        scoreText_.text = score_.ToString();
    }

    private void Fail() {
        score_--;
        scoreText_.text = score_.ToString();
    }

    public int GetLevel() {
        return score_ / scorePerLevel_;
    }
}
