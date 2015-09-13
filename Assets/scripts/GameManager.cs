using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int lifes = 1;
    public bool gameOver = false;
    public int state = 0;
    public GameObject firstRocket;
    public GameObject currentRocket;
    public int i;
    public int rocket = 1;
    public bool rocketswitch = false;

    public static GameManager me = null;

	// Use this for initialization
	void Update () {
        if (lifes <= 0)
        {
            gameOver = true;
            lifes = 1;
        }
	}
	
	// Update is called once per frame
	void Awake () {
        me = this;
	}
}
