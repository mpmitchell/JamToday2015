using UnityEngine;
using System.Collections.Generic;

public class PatternNode : MonoBehaviour {
    public int number;
    public bool isActive = true;
    public LineRenderer lineRenderer;
    public PatternNode[] adjacentNodes;

    private void Awake() {
        number = int.Parse(gameObject.name);
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void ConnectTo(PatternNode node) {
        lineRenderer.SetPosition(1, node.transform.position - transform.position);
    }

    public LinkedList<PatternNode> GetActiveAdjacentNodes() {
        LinkedList<PatternNode> returnList = new LinkedList<PatternNode>();

        foreach (var node in adjacentNodes) {
            if (node.isActive) {
                returnList.AddLast(node);
            }
        }

        return returnList;
    }
}
