using UnityEngine;
using System.Collections.Generic;

public class PatternDisplay : MonoBehaviour {
    private LinkedList<PatternNode> nodes_ = new LinkedList<PatternNode>();

    private float timer = 0.0f;

    private void Awake() {
        foreach (Transform child in transform) {
            nodes_.AddLast(child.GetComponent<PatternNode>());
        }
    }

    public void Initiate(Pattern pattern) {
        pattern.ReplacePatternNodeSet(nodes_);

        Vector2 range = GameController.instance.GetLevelBand();
        float side = Random.Range(0, 2) == 0 ? -1.0f : 1.0f;

        transform.position = new Vector3(side * Random.Range(range.x, range.y), transform.position.y + Random.Range(-5.0f, 5.0f));

        pattern.DisaplyPattern();
    }

    private void Update() {
        if ((timer += Time.deltaTime) > GameController.instance.patternTimeShown_) {
            Destroy(gameObject);
        }
    }
}
