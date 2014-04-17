using UnityEngine;
using System.Collections;

public class SceneMoveTest : MonoBehaviour {
    float timer = 3.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Application.LoadLevel("Game");
        }
	}
}
