using UnityEngine;
using System.Collections.Generic;

public class ShootingStar : MonoBehaviour {
    [SerializeField] private GameObject shootingStar_;
    [SerializeField] private float speed_;

    private Pattern pattern_;
    private LinkedList<PatternNode> nodes_ = new LinkedList<PatternNode>();
    private LinkedListNode<PatternNode> currentTarget_;

    private void Awake() {
        foreach (Transform child in transform) {
            if (child.gameObject != shootingStar_) {
                nodes_.AddLast(child.GetComponent<PatternNode>());
            }
        }
    }

    public void Initiate(Pattern pattern) {
        pattern_ = pattern;
        pattern_.ReplacePatternNodeSet(nodes_);

        Vector2 range = GameController.instance.GetLevelBand();
        float side = Random.Range(0, 2) == 0 ? -1.0f : 1.0f;

        transform.position = new Vector3(side * Random.Range(range.x, range.y), transform.position.y + Random.Range(-5.0f, 5.0f));

        shootingStar_.transform.position = pattern_.pattern.First.Value.transform.position;
        currentTarget_ = pattern_.pattern.First.Next;
    }

    private void Update() {
        Vector2 direction = (currentTarget_.Value.transform.position - shootingStar_.transform.position).normalized;
        shootingStar_.transform.Translate(direction * speed_ * Time.deltaTime);

        if ((currentTarget_.Value.transform.position - shootingStar_.transform.position).magnitude < 0.05f) {
            shootingStar_.transform.position = currentTarget_.Value.transform.position;

            if (currentTarget_.Next != null) {
                currentTarget_ = currentTarget_.Next;
            } else {
                Destroy(gameObject);
            }
        }
    }
}
