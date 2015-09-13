using UnityEngine;
using System.Collections.Generic;

public class ScoreDisplay : MonoBehaviour {
    [SerializeField] private GameObject display1_;
    [SerializeField] private GameObject display2_;

    private LinkedList<PatternNode> nodes1_ = new LinkedList<PatternNode>();
    private LinkedList<PatternNode> nodes2_ = new LinkedList<PatternNode>();

    private void Awake() {
        foreach (Transform child in display1_.transform) {
            nodes1_.AddLast(child.GetComponent<PatternNode>());
        }

        foreach (Transform child in display2_.transform) {
            nodes2_.AddLast(child.GetComponent<PatternNode>());
        }
    }

    public void Initiate(Pattern pattern, float xOffset) {
        pattern.ReplacePlayerNodeSet(nodes1_);
        pattern.ReplacePatternNodeSet(nodes2_);

        pattern.DisaplyPattern();
        pattern.DisaplyPlayerPattern();

        transform.Translate(-Vector3.up * xOffset);
    }
}
