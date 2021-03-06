﻿using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    int score = 0;
    GameObject oneDigit;
    GameObject twoDigit;
    GameObject threeDigit;
    GameObject fourDigit;
    GameObject fiveDigit;
    GameObject result;
    float xOffset = 0.25f;
    float yOffset = 0.35f;


	void Start () 
    {
        this.result = GameObject.FindWithTag("Result");
        UpdateScore(0);
	}

    void UpdateScore(int point)
    {
        this.score += point;
        ScoreToGameObject();
    }

    void ScoreToGameObject()
    {
        DestroyNumbers();
        string scoreString = string.Format("{0:D5}", this.score);
        
        for (int i = 0; i < scoreString.Length; i++)
        {
            // i桁の数字を取る
            string number = scoreString.Substring((scoreString.Length - 1) - i, 1);
            CreateNumber(i + 1, number);
        }

    }

    void DestroyNumbers()
    {
        Destroy(this.oneDigit);
        Destroy(this.twoDigit);
        Destroy(this.threeDigit);
        Destroy(this.fourDigit);
        Destroy(this.fiveDigit);
    }

    void CreateNumber(int digit, string number) 
    {
        string path = "Prefabs/Number/Stage's/" + number;
        Vector2 firstPoint = new Vector2(transform.position.x + 2 * xOffset, transform.position.y - yOffset);
        GameObject onePrefab;
        GameObject twoPrefab;
        GameObject threePrefab;
        GameObject fourPrefab;
        GameObject fivePrefab;
        switch (digit)
        {
            case 1:
                onePrefab = Resources.Load(path) as GameObject;
                this.oneDigit = Instantiate(onePrefab, firstPoint, Quaternion.identity) as GameObject;
                this.oneDigit.transform.parent = gameObject.transform;
                break;
            case 2:
                twoPrefab = Resources.Load(path) as GameObject;
                this.twoDigit = Instantiate(twoPrefab, firstPoint - new Vector2(xOffset, 0), Quaternion.identity) as GameObject;
                this.twoDigit.transform.parent = gameObject.transform;
                break;
            case 3:
                threePrefab = Resources.Load(path) as GameObject;
                this.threeDigit = Instantiate(threePrefab, firstPoint - new Vector2(2 * xOffset, 0), Quaternion.identity) as GameObject;
                this.threeDigit.transform.parent = gameObject.transform;
                break;
            case 4:
                fourPrefab = Resources.Load(path) as GameObject;
                this.fourDigit = Instantiate(fourPrefab, firstPoint - new Vector2(3 * xOffset, 0), Quaternion.identity) as GameObject;
                this.fourDigit.transform.parent = gameObject.transform;
                break;
            case 5:
                fivePrefab = Resources.Load(path) as GameObject;
                this.fiveDigit = Instantiate(fivePrefab, firstPoint - new Vector2(4 * xOffset, 0), Quaternion.identity) as GameObject;
                this.fiveDigit.transform.parent = gameObject.transform;
                break;
        }
    }

    void ResetScore()
    {
        this.score = 0;
        UpdateScore(0);
    }

    void SendToResultScore()
    {
        this.result.SendMessage("CatchScore", this.score);
    }
}
