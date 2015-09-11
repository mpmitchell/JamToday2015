using UnityEngine;

public class PatternGrid : MonoBehaviour {
    private int index_ = 0;

    private LineRenderer lineRenderer_;

    private void Awake() {
        lineRenderer_ = GetComponent<LineRenderer>();
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            lineRenderer_.SetVertexCount(1);
            index_ = 0;
        }
    }

    private void HitDot(Vector3 position) {
        lineRenderer_.SetVertexCount(index_ + 1);
        lineRenderer_.SetPosition(index_++, position);
    }
}
