using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallScript : MonoBehaviour
{
    public GameObject ballPrefab;
    public RectTransform spawnZone;
    RectTransform secondSpawnZone;
    public GameObject secondspawn;
    public GameObject parentObject;
    public GameObject parentObject2;
    public float timer = 0;
    public int i;
    public int zone;
    public GameObject gameOverScreen;
    List<GameObject> ballsList;
    public float gameTimer;
    public float gameTimerLimit = 60;
    public GameObject GameOverTimerScreen;
    int level = 2;
    int ballType;
    public GameObject[] goodObjects;
    public GameObject[] badObjects;
    public GameObject paddle;


	// Use this for initialization
	void Start () {
        i = Random.Range(1, 4);
        zone = Random.Range(0, 2);
        secondSpawnZone = secondspawn.GetComponent<RectTransform>();
        ballsList = new List<GameObject>();
        if (level == 2)
        {
            spawnZone.transform.position = new Vector3(spawnZone.transform.position.x + spawnZone.rect.height/6, spawnZone.transform.position.y);
            secondSpawnZone.transform.position = new Vector3(secondSpawnZone.transform.position.x - secondSpawnZone.rect.height/6, secondSpawnZone.transform.position.y);
        }
	}
	
	// Update is called once per frame
	void Update () {
        gameTimer += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= i)
        {
            SpawnBalls();
            timer = 0;
            i = Random.Range(1, 3);
            zone = Random.Range(0, 2);
        }
        if (GameManager.me.gameOver)
        {
            GameOver();
        }
        if (gameTimer >= gameTimerLimit)
        {
            GameOver();
        }
	}

    void SpawnBalls()
    {
        ballType = Random.Range(0, 2);
        if (ballType == 0)
        {
            GameObject newItem = Instantiate(goodObjects[Random.Range(0,goodObjects.Length)]) as GameObject;
            //Blue Ball
            gameObject.tag = "Good";
            aMethod(newItem);
        }
        else
        {
            GameObject newItem = Instantiate(badObjects[Random.Range(0, badObjects.Length)]) as GameObject;
            //Red Ball4
            gameObject.tag = "Bad";
            aMethod(newItem);
        }
    }

    void aMethod(GameObject newItem)
    {
        ballsList.Add(newItem);
        Rigidbody2D rb = newItem.GetComponent<Rigidbody2D>();
        if (zone == 0)
        {
            newItem.transform.SetParent(parentObject.transform, false);
            newItem.transform.localPosition = new Vector3(0, 0, 0);
            // newItem.transform.localScale = new Vector3(3f, 0.5f, 1);
            newItem.transform.localPosition = new Vector3(Random.Range(spawnZone.rect.x, spawnZone.rect.x + 10), Random.Range(spawnZone.rect.y, spawnZone.rect.y + 10), 0);
            rb.AddForce(new Vector2(Random.Range(50, 75), Random.Range(-75, 75)));
        }
        else
        {
            newItem.transform.SetParent(parentObject2.transform, false);
            newItem.transform.localPosition = new Vector3(0, 0, 0);
            //  newItem.transform.localScale = new Vector3(3f, 0.5f, 1);
            newItem.transform.localPosition = new Vector3(Random.Range(secondspawn.transform.localPosition.x, secondspawn.transform.localPosition.x - 10), Random.Range(secondspawn.transform.localPosition.y, secondspawn.transform.localPosition.y - 10), 0);
            rb.AddForce(new Vector2(Random.Range(-50, -75), Random.Range(-75, 75)));
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
       // Cursor.visible = true;
        gameOverScreen.SetActive(true);
        
    }

    void GameOverTime()
    {
        Time.timeScale = 0;
       // Cursor.visible = true;
        GameOverTimerScreen.SetActive(true);
    }

    public void RetryButton()
    {
        if (GameManager.me.currentRocket !=null) { GameManager.me.currentRocket.SetActive(false); }
        GameManager.me.firstRocket.SetActive(true);
        GameManager.me.gameOver = false;
        GameManager.me.state = 0;
        GameManager.me.lifes = 1;
        //Cursor.visible = false;
        Time.timeScale = 1;
        foreach (GameObject ball in ballsList)
        {
            Destroy(ball);
        }
        Debug.Log("here");


        gameOverScreen.SetActive(false);
    }

    public void MainMenuButton()
    {

    }
}
