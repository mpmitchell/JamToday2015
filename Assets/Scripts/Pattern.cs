using UnityEngine;
using System.Collections.Generic;

public class Pattern {
    public LinkedList<PatternNode> pattern = new LinkedList<PatternNode>();
    public LinkedList<PatternNode> playerAttempt = new LinkedList<PatternNode>();

    public bool successful = false;

    public void ReplacePatternNodeSet(LinkedList<PatternNode> nodes) {
        for (var node = pattern.First; node != null; node = node.Next) {
            foreach (var newNode in nodes) {
                if (newNode.number == node.Value.number) {
                    pattern.AddBefore(node, newNode);
                    pattern.Remove(node);
                    node = pattern.Find(newNode);
                }
            }
        }
    }

    public void ReplacePlayerNodeSet(LinkedList<PatternNode> nodes) {
        for (var node = playerAttempt.First; node != null; node = node.Next) {
            foreach (var newNode in nodes) {
                if (newNode.number == node.Value.number) {
                    playerAttempt.AddBefore(node, newNode);
                    playerAttempt.Remove(node);
                    node = playerAttempt.Find(newNode);
                }
            }
        }
    }

    public void DisaplyPattern() {
        for (var node = pattern.First; node.Next != null; node = node.Next) {
            node.Value.lineRenderer.SetPosition(1, node.Next.Value.transform.position - node.Value.transform.position);
        }
    }

    public void DisaplyPlayerPattern() {
        if (playerAttempt.Count > 0) {
            for (var node = playerAttempt.First; node.Next != null; node = node.Next) {
                node.Value.lineRenderer.SetPosition(1, node.Next.Value.transform.position - node.Value.transform.position);
            }
        }
    }
}
