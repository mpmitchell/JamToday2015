using UnityEngine;
using System.Collections.Generic;

public class RandomPattern : MonoBehaviour {


    private LinkedList<KeyValuePair<GameObject, LineRenderer>> dots_ = new LinkedList<KeyValuePair<GameObject, LineRenderer>>();
    private LinkedList<KeyValuePair<GameObject, LineRenderer>> pattern_ = new LinkedList<KeyValuePair<GameObject, LineRenderer>>();

    private void Awake() {
        foreach (Transform child in transform) {
            dots_.AddLast(new KeyValuePair<GameObject, LineRenderer>(child.gameObject, child.GetComponent<LineRenderer>()));
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Fire2")) {
            GeneratePattern();
        }
    }

    private void GeneratePattern() {
        var dot = pattern_.Last;
        for (int index = pattern_.Count; index > 0; --index) {
            dot.Value.Value.SetPosition(1, Vector3.zero);
            dots_.AddLast(dot.Value);
            dot = dot.Previous;
        }
        pattern_.Clear();

        int count = Random.Range(3, 9);

        for (int i = 0; i < count; ++i) {
            dot = dots_.Last;
            for (int index = Random.Range(0, 9 - i); index > 0; --index, dot = dot.Previous) ;

            if (pattern_.Count != 0) {
                dot.Value.Value.SetPosition(1, pattern_.Last.Value.Key.transform.position - dot.Value.Key.transform.position);
            }

            pattern_.AddLast(dot.Value);
            dots_.Remove(dot.Value);
        }
    }
}
