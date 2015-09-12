using UnityEngine;
using System.Collections.Generic;

public class RandomPattern : MonoBehaviour {
    [SerializeField] private int minPatternSize;
    [SerializeField] private int maxPatternSize;
    [SerializeField] private float frequency;
    
    private LinkedList<GameObject> dots_ = new LinkedList<GameObject>();
    private Dictionary<GameObject, LineRenderer> lineRenderers_ = new Dictionary<GameObject, LineRenderer>();
    private Dictionary<GameObject, LinkedList<GameObject>> adjacencyLists_ = new Dictionary<GameObject, LinkedList<GameObject>>();
    private LinkedList<GameObject> pattern_ = new LinkedList<GameObject>();
    private float timer = 0.0f;
    private Rect bounds = new Rect();

    private void Awake() {
        foreach (Transform child in transform) {
            dots_.AddLast(child.gameObject);
            lineRenderers_.Add(child.gameObject, child.gameObject.GetComponent<LineRenderer>());
            adjacencyLists_.Add(child.gameObject, new LinkedList<GameObject>(child.gameObject.GetComponent<Node>().adjacentNodes_));

            Bounds sprite = child.GetComponent<SpriteRenderer>().sprite.bounds;

            if (child.localPosition.x - sprite.size.x < bounds.xMin) {
                bounds.xMin = child.localPosition.x - sprite.size.x;
            }
            if (child.localPosition.x + sprite.size.x > bounds.xMax) {
                bounds.xMax = child.localPosition.x + sprite.size.x;
            }
            if (child.localPosition.y - sprite.size.y < bounds.yMin) {
                bounds.yMin = child.localPosition.y - sprite.size.y;
            }
            if (child.localPosition.y + sprite.size.y > bounds.yMax) {
                bounds.yMax = child.localPosition.y + sprite.size.y;
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

        //if (timer > 1.0f / frequency) {
        if (Input.GetButtonDown("Fire2")) {
            RandomizePosition();
            ClearPreviousPattern();
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

    private void ClearPreviousPattern() {
        foreach (GameObject dot in dots_) {
            lineRenderers_[dot].SetPosition(1, Vector3.zero);
        }
        pattern_.Clear();
    }

    private void GeneratePattern() {
        int count = Random.Range(minPatternSize, maxPatternSize + 1);

        var dot = dots_.Last;
        for (int index = Random.Range(0, dots_.Count); index > 0; --index, dot = dot.Previous) ;

        pattern_.AddLast(dot.Value);

        for (int i = 0; i < count; ++i) {
            LinkedList<GameObject> otherDots = new LinkedList<GameObject>(adjacencyLists_[dot.Value]);
            LinkedList<GameObject> adjacentDots = new LinkedList<GameObject>();
            foreach (GameObject otherDot in otherDots) {
                if (!pattern_.Contains(otherDot)) {
                    adjacentDots.AddLast(otherDot);
                }
            }

            if (adjacentDots.Count == 0) {
                break;
            }

            var adjacentDot = adjacentDots.Last;
            for (int index = Random.Range(0, adjacentDots.Count); index > 0; --index, adjacentDot = adjacentDot.Previous) ;
            
            lineRenderers_[dot.Value].SetPosition(1, adjacentDot.Value.transform.position - dot.Value.transform.position);
            pattern_.AddLast(adjacentDot.Value);
            dot = dots_.Find(adjacentDot.Value);
        }
    }

    public LinkedList<string> GetPattern() {
        LinkedList<string> pattern = new LinkedList<string>();
        foreach (GameObject dot in pattern_) {
            pattern.AddLast(dot.name);
        }
        return pattern;
    }
}
