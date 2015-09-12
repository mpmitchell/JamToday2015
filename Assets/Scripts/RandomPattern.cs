using UnityEngine;
using System.Collections.Generic;

public class RandomPattern : MonoBehaviour {
    private GameData gameData_;

    [SerializeField] private int minPatternSize_;
    [SerializeField] private int maxPatternSize_;
    [SerializeField] private float frequency_;
    
    private LinkedList<GameObject> dots_ = new LinkedList<GameObject>();
    private Dictionary<GameObject, LineRenderer> lineRenderers_ = new Dictionary<GameObject, LineRenderer>();
    private Dictionary<GameObject, LinkedList<GameObject>> adjacencyLists_ = new Dictionary<GameObject, LinkedList<GameObject>>();
    private LinkedList<GameObject> pattern_ = new LinkedList<GameObject>();
    private float timer_ = 0.0f;
    private Rect bounds_ = new Rect();
    private float length_;

    private void Awake() {
        gameData_ = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameData>();

        foreach (Transform child in transform) {
            dots_.AddLast(child.gameObject);
            lineRenderers_.Add(child.gameObject, child.gameObject.GetComponent<LineRenderer>());
            adjacencyLists_.Add(child.gameObject, new LinkedList<GameObject>(child.gameObject.GetComponent<Node>().adjacentNodes_));

            Bounds sprite = child.GetComponent<SpriteRenderer>().sprite.bounds;

            if (child.localPosition.x - sprite.size.x < bounds_.xMin) {
                bounds_.xMin = child.localPosition.x - sprite.size.x;
            }
            if (child.localPosition.x + sprite.size.x > bounds_.xMax) {
                bounds_.xMax = child.localPosition.x + sprite.size.x;
            }
            if (child.localPosition.y - sprite.size.y < bounds_.yMin) {
                bounds_.yMin = child.localPosition.y - sprite.size.y;
            }
            if (child.localPosition.y + sprite.size.y > bounds_.yMax) {
                bounds_.yMax = child.localPosition.y + sprite.size.y;
            }
        }

        length_ = bounds_.size.x;

        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, -Camera.main.transform.position.z));
        Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, -Camera.main.transform.position.z));

        bounds_.xMin = topLeft.x - bounds_.xMin;
        bounds_.yMin = topLeft.y - bounds_.yMin;
        bounds_.xMax = bottomRight.x - bounds_.xMax;
        bounds_.yMax = bottomRight.y - bounds_.yMax;
    }

    private void Update() {
        timer_ += Time.deltaTime;

        if (timer_ > 1.0f / frequency_) {
            //if (Input.GetButtonDown("Fire2")) {
            RandomizePosition();
            ClearPreviousPattern();
            GeneratePattern();
            timer_ = 0.0f;
        }
    }

    private void RandomizePosition() {
        float max = Mathf.Min(gameData_.halfLength_ + (gameData_.GetLevel() + 1) * length_, bounds_.xMax);

        transform.position = new Vector3(Random.Range(gameData_.halfLength_ + gameData_.GetLevel() * length_, max), Random.Range(bounds_.yMin, bounds_.yMax));
    }

    private void ClearPreviousPattern() {
        foreach (GameObject dot in dots_) {
            lineRenderers_[dot].SetPosition(1, Vector3.zero);
        }
        pattern_.Clear();
    }

    private void GeneratePattern() {
        int count = Random.Range(minPatternSize_, maxPatternSize_ + 1);

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
