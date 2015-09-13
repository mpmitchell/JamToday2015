using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public static GameController instance = null;

    public float maxTimePerPattern_;
    public float patternTimeShown_;
    public int patternCount_;
    public int minPatternSize_;
    public int maxPatternSize_;

    [SerializeField] private GameObject scoreContainer_;
    [SerializeField] private GameObject shootingStarPrefab_;
    [SerializeField] private GameObject patternDisplayPrefab_;
    [SerializeField] private Vector3[] levelBands_;

    private int level_ = 0;

    private LinkedList<Pattern> newPatterns_ = new LinkedList<Pattern>();
    public LinkedList<Pattern> recordedPatterns_ = new LinkedList<Pattern>();

    public Pattern currentPattern_;
    private GameObject currentPatternDisplay_;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Multiple GameControllers");
        }
    }

    public Vector2 GetLevelBand() {
        Vector2 band = Vector2.zero;

        foreach (Vector3 levelBand in levelBands_) {
            if (level_ >= levelBand.x) {
                band = new Vector2(levelBand.y, levelBand.z);
            }
        }

        return band;
    }

    public void RegisterPattern(Pattern pattern) {
        if (currentPattern_ == null) {
            currentPattern_ = pattern;

            GameObject shootingStar = Instantiate(shootingStarPrefab_) as GameObject;
            shootingStar.GetComponent<ShootingStar>().Initiate(pattern);

            //currentPatternDisplay_ = Instantiate(patternDisplayPrefab_) as GameObject;
            //currentPatternDisplay_.GetComponent<PatternDisplay>().Initiate(pattern);
        } else {
            newPatterns_.AddLast(pattern);
        }
    }

    public void RegisterAttempt() {
        recordedPatterns_.AddLast(currentPattern_);
        
        if (currentPattern_.playerAttempt.Count == currentPattern_.pattern.Count) {
            int correct = 0;

            var playerNode = currentPattern_.playerAttempt.First;
            var node = currentPattern_.pattern.First;

            for (int i = 0; correct == 0 && i < currentPattern_.playerAttempt.Count; ++i) {
                if (playerNode.Value.number != node.Value.number) {
                    correct = 1;
                } else {
                    playerNode = playerNode.Next;
                    node = node.Next;
                }
            }

            if (correct == 1) {
                playerNode = currentPattern_.playerAttempt.First;
                node = currentPattern_.pattern.Last;

                for (int i = 0; correct == 1 && i < currentPattern_.playerAttempt.Count; ++i) {
                    if (playerNode.Value.number != node.Value.number) {
                        correct = 2;
                    } else {
                        playerNode = playerNode.Next;
                        node = node.Previous;
                    }
                }
            }

            if (correct != 2) {
                currentPattern_.successful = true;
                level_++;
            } else {
                if (--level_ < 0) {
                    level_ = 0;
                }
            }
        } else {
            if (--level_ < 0) {
                level_ = 0;
            }
        }

        foreach (PatternNode node in currentPattern_.playerAttempt) {
            node.lineRenderer.SetPosition(1, Vector3.zero);
        }

        if (newPatterns_.Count > 0) {
            if (currentPatternDisplay_ != null) {
                Destroy(currentPatternDisplay_);
            }

            currentPattern_ = newPatterns_.Last.Value;
            newPatterns_.RemoveLast();

            GameObject shootingStar = Instantiate(shootingStarPrefab_) as GameObject;
            shootingStar.GetComponent<ShootingStar>().Initiate(currentPattern_);

            //currentPatternDisplay_ = Instantiate(patternDisplayPrefab_) as GameObject;
            //currentPatternDisplay_.GetComponent<PatternDisplay>().Initiate(currentPattern_);
        } else {
            foreach (Transform child in transform) {
                child.gameObject.SetActive(false);
            }

            scoreContainer_.SetActive(true);
            scoreContainer_.GetComponent<Summary>().Initiate();
        }
    }
}
