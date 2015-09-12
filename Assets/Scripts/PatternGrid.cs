using UnityEngine;
using System.Collections.Generic;

public class PatternGrid : MonoBehaviour {
    private GameObject gameData_;

    private GameObject previousDot_ = null;
    private LinkedList<string> pattern_ = new LinkedList<string>();
    private RandomPattern randomPattern_;

    private LinkedList<GameObject> dots_ = new LinkedList<GameObject>();
    private Dictionary<GameObject, bool> activeDots_ = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, LineRenderer> lineRenderers_ = new Dictionary<GameObject, LineRenderer>();

    private bool done_ = false;

    private void Awake() {
        gameData_ = GameObject.FindGameObjectWithTag("GameController");
        randomPattern_ = GameObject.FindGameObjectWithTag("Pattern").GetComponent<RandomPattern>();

        Rect bounds = new Rect();

        foreach (Transform child in transform) {
            dots_.AddLast(child.gameObject);
            activeDots_.Add(child.gameObject, true);
            lineRenderers_.Add(child.gameObject, child.gameObject.GetComponent<LineRenderer>());

            Bounds sprite = child.GetComponent<SpriteRenderer>().sprite.bounds;

            if (child.localPosition.x - sprite.size.x < bounds.xMin) {
                bounds.xMin = child.localPosition.x - sprite.size.x;
            }
            if (child.localPosition.x + sprite.size.x > bounds.xMax) {
                bounds.xMax = child.localPosition.x + sprite.size.x;
            }
        }

        gameData_.GetComponent<GameData>().halfLength_ = bounds.size.x / 2.0f;
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            foreach (GameObject dot in dots_) {
                activeDots_[dot] = true;
                lineRenderers_[dot].SetPosition(1, Vector3.zero);
            }
            previousDot_ = null;
            pattern_.Clear();
        } else if (Input.GetButton("Fire1")) {
            RaycastHit hit;
            
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Dot"))) {
                if (activeDots_[hit.collider.gameObject]) {
                    activeDots_[hit.collider.gameObject] = false;

                    if (previousDot_ != null) {
                        lineRenderers_[hit.collider.gameObject].SetPosition(1, previousDot_.transform.position - hit.collider.gameObject.transform.position);
                    }

                    previousDot_ = hit.collider.gameObject;
                    pattern_.AddLast(hit.collider.gameObject.name);
                }
            }
        } else if (!done_ && Input.GetButtonUp("Fire1") && pattern_.Count > 1) {
            var randomPattern = randomPattern_.GetPattern();
            
            if (pattern_.Count == randomPattern.Count) {
                int correct = 0;

                var dot = pattern_.First;
                var randomDot = randomPattern.First;

                for (int i = 0; correct == 0 && i < pattern_.Count; ++i) {
                    if (!dot.Value.Equals(randomDot.Value)) {
                        correct = 1;
                    } else {
                        dot = dot.Next;
                        randomDot = randomDot.Next;
                    }
                }

                if (correct == 1) {
                    dot = pattern_.First;
                    randomDot = randomPattern.Last;

                    for (int i = 0; correct == 1 && i < pattern_.Count; ++i) {
                        if (!dot.Value.Equals(randomDot.Value)) {
                            correct = 2;
                        } else {
                            dot = dot.Next;
                            randomDot = randomDot.Previous;
                        }
                    }
                }

                if (correct == 2) {
                    gameData_.SendMessage("Fail");
                } else {
                    gameData_.SendMessage("Score");
                }
            } else {
                gameData_.SendMessage("Fail");
            }

            done_ = true;
        }
    }

    private void NewPattern() {
        done_ = false;
    }
}
