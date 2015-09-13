using UnityEngine;
using System.Collections.Generic;

public class PatternGenerator : MonoBehaviour {
    private LinkedList<PatternNode> nodes_ = new LinkedList<PatternNode>();

    private void Start() {
        foreach (Transform child in transform) {
            nodes_.AddLast(child.GetComponent<PatternNode>());
        }

        for (int i = 0; i < GameController.instance.patternCount_; ++i) {
            GeneratePattern();
        }
    }

    private void GeneratePattern() {
        Pattern pattern = new Pattern();

        int count = Random.Range(GameController.instance.minPatternSize_, GameController.instance.maxPatternSize_ + 1);
        
        var node = nodes_.Last;
        for (int index = Random.Range(0, nodes_.Count); index > 0; --index, node = node.Previous) ;

        node.Value.isActive = false;
        pattern.pattern.AddLast(node.Value);

        for (int i = 0; i < count; ++i) {
            LinkedList<PatternNode> adjacentNodes = node.Value.GetActiveAdjacentNodes();

            if (adjacentNodes.Count < 0) {
                break;
            }

            var adjacentNode = adjacentNodes.Last;
            for (int index = Random.Range(0, adjacentNodes.Count); index > 0; --index, adjacentNode = adjacentNode.Previous) ;
            
            pattern.pattern.AddLast(adjacentNode.Value);
            node = nodes_.Find(adjacentNode.Value);
            node.Value.isActive = false;
        }

        GameController.instance.RegisterPattern(pattern);
        for (node = nodes_.First; node != null; node.Value.isActive = true, node = node.Next) ;
    }
}
