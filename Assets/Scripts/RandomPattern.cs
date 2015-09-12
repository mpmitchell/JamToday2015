using UnityEngine;
using System.Collections.Generic;

public class RandomPattern : MonoBehaviour {
    [SerializeField] private float frequency;

    private LinkedList<KeyValuePair<GameObject, LineRenderer>> dots_ = new LinkedList<KeyValuePair<GameObject, LineRenderer>>();
    private LinkedList<KeyValuePair<GameObject, LineRenderer>> pattern_ = new LinkedList<KeyValuePair<GameObject, LineRenderer>>();
    private float timer = 0.0f;
    private Rect bounds = new Rect();

    private void Awake() {
        foreach (Transform child in transform) {
            dots_.AddLast(new KeyValuePair<GameObject, LineRenderer>(child.gameObject, child.GetComponent<LineRenderer>()));
            
            if (child.localPosition.x < bounds.xMin) {
                bounds.xMin = child.localPosition.x;
            }
            if (child.localPosition.x > bounds.xMax) {
                bounds.xMax = child.localPosition.x;
            }
            if (child.localPosition.y < bounds.yMin) {
                bounds.yMin = child.localPosition.y;
            }
            if (child.localPosition.y > bounds.yMax) {
                bounds.yMax = child.localPosition.y;
            }
        }

        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, -Camera.main.transform.position.z));
        Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, -Camera.main.transform.position.z));

        bounds.xMin = topLeft.x - bounds.xMin;
        bounds.yMin = topLeft.y - bounds.yMin;
        bounds.xMax = bottomRight.x - bounds.xMax;
        bounds.yMax = bottomRight.y - bounds.yMax;
    }

    private void Update() {
        timer += Time.deltaTime;

        if (timer > 1.0f / frequency) {
            RandomizePosition();
            GeneratePattern();
            timer = 0.0f;
        }
    }

    private void RandomizePosition() {
        if (Random.Range(0, 2) == 0) {
            transform.position = new Vector3(Random.Range(0, 2) == 0 ? bounds.xMin : bounds.xMax, Random.Range(bounds.yMin, bounds.yMax));
        } else {
            transform.position = new Vector3(Random.Range(bounds.xMin, bounds.xMax), Random.Range(0, 2) == 0 ? bounds.yMin : bounds.yMax);
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

        int max = dots_.Count;
        int count = Random.Range(3, max);

        for (int i = 0; i < count; ++i) {
            dot = dots_.Last;
            for (int index = Random.Range(0, max - i); index > 0; --index, dot = dot.Previous) ;

            if (pattern_.Count != 0) {
                dot.Value.Value.SetPosition(1, pattern_.Last.Value.Key.transform.position - dot.Value.Key.transform.position);
            }

            pattern_.AddLast(dot.Value);
            dots_.Remove(dot.Value);
        }
    }
}
