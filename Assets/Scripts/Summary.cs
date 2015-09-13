using UnityEngine;
using System.Collections.Generic;

public class Summary : MonoBehaviour {
    [SerializeField] private GameObject scoreDisplayPrefab_;

    private LinkedList<ScoreDisplay> scoreDisplays_ = new LinkedList<ScoreDisplay>();

    public void Initiate() {
        var pattern = GameController.instance.recordedPatterns_.First;
        for (int i = 0; pattern != null; ++i, pattern = pattern.Next) {
            GameObject scoreDisaply = Instantiate(scoreDisplayPrefab_, new Vector3(-7, 0, 0), Quaternion.identity) as GameObject;
            scoreDisaply.GetComponent<ScoreDisplay>().Initiate(pattern.Value, i * 4);
            scoreDisaply.transform.parent = transform;
        }
    }
}
