using UnityEngine;
using System.Collections.Generic;

public class TutorialController : MonoBehaviour {
    [SerializeField] private GameObject scene1_;
    [SerializeField] private GameObject scene2_;
    [SerializeField] private GameObject scene3_;
    [SerializeField] private GameObject scene4_;

    [SerializeField] private GameObject scene1Button_;
    
    [SerializeField] private PatternNode[] scene2ExamplePattern_;
    [SerializeField] private GameObject scene2Button_;

    [SerializeField] private GameObject scene3Button_;
    [SerializeField] private GameObject scene4Button_;

    private Dictionary<GameObject, PatternNode> nodes_ = new Dictionary<GameObject, PatternNode>();
    private LinkedList<PatternNode> pattern_ = new LinkedList<PatternNode>();

    private int currentScene_ = 1;

    private void Start() {
        foreach (Transform child in transform) {
            nodes_.Add(child.gameObject, child.GetComponent<PatternNode>());
        }
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            pattern_.Clear();
            foreach (PatternNode node in nodes_.Values) {
                node.lineRenderer.SetPosition(1, Vector3.zero);
                node.isActive = true;
            }
        } else if (Input.GetButton("Fire1")) {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("PatternNodes"))) {
                if (nodes_[hit.collider.gameObject].isActive) {
                    nodes_[hit.collider.gameObject].isActive = false;

                    if (pattern_.Count > 0) {
                        nodes_[hit.collider.gameObject].ConnectTo(pattern_.Last.Value);
                    }

                    pattern_.AddLast(nodes_[hit.collider.gameObject]);
                }
            }
        } else if (Input.GetButtonUp("Fire1")) {
            if (currentScene_ == 1) {
                scene1Button_.SetActive(true);
            } else if (currentScene_ == 2 && pattern_.Count == scene2ExamplePattern_.Length) {
                int correct = 0;

                var node = pattern_.First;
                for (int i = 0; i < scene2ExamplePattern_.Length; ++i, node = node.Next) {
                    if (node.Value.number != scene2ExamplePattern_[i].number) {
                        correct = 1;
                        break;
                    }
                }

                if (correct == 1) {
                    node = pattern_.Last;
                    for (int i = 0; i < scene2ExamplePattern_.Length; ++i, node = node.Previous) {
                        if (node.Value.number != scene2ExamplePattern_[i].number) {
                            correct = 2;
                            break;
                        }
                    }
                }

                if (correct != 2) {
                    scene2Button_.SetActive(true);
                }
            }
        }
    }

    public void Scene1Button() {
        scene1_.SetActive(false);
        scene2_.SetActive(true);

        for (int i = 1; i < scene2ExamplePattern_.Length; ++i) {
            scene2ExamplePattern_[i].lineRenderer.SetPosition(1, scene2ExamplePattern_[i - 1].transform.position - scene2ExamplePattern_[i].transform.position);
        }

        currentScene_ = 2;
    }

    public void Scene2Button() {
        scene2_.SetActive(false);
        scene3_.SetActive(true);

        currentScene_ = 3;
    }

    public void Scene3Button() {
        scene3_.SetActive(false);
        scene4_.SetActive(true);

        currentScene_ = 4;
    }

    public void Scene4Button() {
        Application.LoadLevel("Star Game");
    }
}
