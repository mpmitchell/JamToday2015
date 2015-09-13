using UnityEngine;
using System.Collections;

public class ballScript : MonoBehaviour {
    public GameObject ballPrefab;
    public RectTransform spawnZone;
	// Use this for initialization
	void Start () {
        Debug.Log(spawnZone.rect);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
