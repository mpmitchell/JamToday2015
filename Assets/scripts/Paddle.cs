using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

    Vector3 mousePos;
    public Transform paddle;
    Camera camera;
    Rigidbody2D rb;
   // Sprite[] rocketStates;
    public GameObject[] rocketStates;
    SpriteRenderer sprite;
    int currentState = -1;
  public  int rocket = 0;
    public Sprite[] rocketFire;
    float power = 0;
    public GameObject trackpos;
    public GameObject trackPosParent;
    int i = 0;
    public AudioClip burst;
    public AudioClip upgrade;
    public AudioSource sound;
    public GameObject upgradeObject;
	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
       // Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
        Vector2 pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2, Screen.height / 2));
        transform.position = new Vector3(pos.x, pos.y, 0);
        sprite = gameObject.GetComponent<SpriteRenderer>();
      //  sprite.sprite = rocketStates[0];
        sound = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {
        //if (!Cursor.visible)
        //{
        //    mousePos = Input.mousePosition;
        //    mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //    paddle.transform.position = new Vector2(transform.position.x, mousePos.y);
        //}
        if (GameManager.me.rocketswitch)
        {
           AudioSource au = upgradeObject.GetComponent<AudioSource>();
            au.PlayOneShot(upgrade);
            Debug.Log(GameManager.me.rocketswitch);
          //  sound.PlayOneShot(upgrade);
            GameManager.me.rocketswitch = false;
        }
        if (Input.GetKey(KeyCode.B))
        {
            rb.AddForce(new Vector2(0,15));
            sprite.sprite = rocketFire[1];
            power += Time.deltaTime;
            sound.PlayOneShot(burst);
            if (power > 0.08f)
            {
                sprite.sprite = rocketFire[2];
            }
        }
        else
        {
            sprite.sprite = rocketFire[0];
            power = 0;
            sound.Stop();
        }
        if (GameManager.me.rocket < 3)
        {
            if (GameManager.me.state >= 1)
            {
                //trackpos.transform.SetParent(trackPosParent.transform, false);
                GameManager.me.rocketswitch = true;
                rocketStates[GameManager.me.i + 1].transform.position = trackpos.transform.position;
                trackpos.transform.SetParent(rocketStates[GameManager.me.i + 1].transform, false);
                trackpos.transform.localPosition = new Vector3(0, 0, 0);
                currentState++;
                gameObject.SetActive(false);
                rocketStates[GameManager.me.i + 1].SetActive(true);
                GameManager.me.currentRocket = rocketStates[currentState + 1];
                GameManager.me.state = 0;
                GameManager.me.rocket++;
                GameManager.me.i++;
                GameManager.me.lifes++;

            }
        }

	}
}
