using UnityEngine;

public class Stop : MonoBehaviour {
    private float timer = 0.0f;

    public void Show() {
        gameObject.SetActive(true);
    }

    private void Update() {
        if ((timer += Time.deltaTime) > GameController.instance.delay_) {
            gameObject.SetActive(false);
        }
    }
}
