using UnityEngine;

public class Dot : MonoBehaviour {
    private bool active_ = true;

    private LineRenderer lineRenderer_;

    private void Awake() {
        lineRenderer_ = GetComponent<LineRenderer>();
    }

    private void Update() {
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1")) {
            lineRenderer_.SetPosition(1, Vector3.zero);
            active_ = true;
        }

        if (active_ && Input.GetButton("Fire1")  && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Dot"))) {
            if (hit.collider.gameObject == gameObject) {
                active_ = false;
                SendMessageUpwards("HitDot", gameObject);
            }
        }
    }

    private void Connect(GameObject otherDot) {
        lineRenderer_.SetPosition(1, otherDot.transform.position - transform.position);
    }
}
