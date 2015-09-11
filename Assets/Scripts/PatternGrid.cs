using UnityEngine;
using System.Collections.Generic;

public class PatternGrid : MonoBehaviour {
    private LinkedList<GameObject> pattern_ = new LinkedList<GameObject>();

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            pattern_.Clear();
        }
    }

    private void HitDot(GameObject dot) {
        if (pattern_.Count != 0) {
            dot.SendMessage("Connect", pattern_.Last.Value);
        }

        pattern_.AddLast(dot);
    }
}
