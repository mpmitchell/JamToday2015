using UnityEngine;

public class Dot : MonoBehaviour {
    private bool active_ = true;

    private void Update() {
        RaycastHit hit;

        if (Input.GetButtonDown("Fire1")) {
            active_ = true;
        }

        if (active_ && Input.GetButton("Fire1")  && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, LayerMask.GetMask("Dot"))) {
            if (hit.collider.gameObject == this.gameObject) {
                active_ = false;
                SendMessageUpwards("HitDot", transform.position);
            }
        }
    }
}
