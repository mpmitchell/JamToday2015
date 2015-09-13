using UnityEngine;
using System.Collections;

public class DestroyBall : MonoBehaviour {

    public float i = 0;
    bool hit = false;
    int ballType;



	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.tag == "Bad")
        {
            i += Time.deltaTime;
            if (i > 5) { Destroy(gameObject); }
        }
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Paddle")
        {
            if (gameObject.tag == "Good")
            {
                hit = true;
                GameManager.me.state++;
                Debug.Log("hello");
                Destroy(gameObject);
            }
            else if(gameObject.tag=="Bad")
            {
                GameManager.me.lifes--;
            }

        }
    }

}
