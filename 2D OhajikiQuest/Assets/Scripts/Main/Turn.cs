using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour {
    
    int turn = 0;
    GameObject oneDigit;
    GameObject twoDigit;

    float xOffset = 0.125f;
    float yOffset = 0.35f;


	void Start () 
    {
        TurnToGameObject();
        //string n = "0";
        //CreateNumber(1, n);
        //CreateNumber(2, n);
	}

    void UpdteTurn(int turnNumber)
    {
        this.turn = turnNumber;
        TurnToGameObject();
    }

    void TurnToGameObject()
    {
        DestroyNumbers();
        string turnString = string.Format("{0:D2}", this.turn);
        for (int i = 0; i < turnString.Length; i++)
        {
            // i桁の数字を取る
            string number = turnString.Substring((turnString.Length - 1) - i, 1);
            CreateNumber(i + 1, number);
        }

    }

    void DestroyNumbers()
    {
        Destroy(this.oneDigit);
        Destroy(this.twoDigit);
    }

    void CreateNumber(int digit, string number) 
    {
        string path = "Prefabs/Number/Stage's/" + number;
        Vector2 oneDigitPoint = new Vector2(transform.position.x + xOffset, transform.position.y - yOffset);
        Vector2 twoDigitPoint = new Vector2(transform.position.x - xOffset, transform.position.y - yOffset);
        GameObject onePrefab;
        GameObject twoPrefab;
        switch (digit)
        {
            case 1:
                onePrefab = Resources.Load(path) as GameObject;
                this.oneDigit = Instantiate(onePrefab, oneDigitPoint, Quaternion.identity) as GameObject;
                this.oneDigit.transform.parent = gameObject.transform;
                break;
            case 2:
                twoPrefab = Resources.Load(path) as GameObject;
                this.twoDigit = Instantiate(twoPrefab, twoDigitPoint, Quaternion.identity) as GameObject;
                this.twoDigit.transform.parent = gameObject.transform;
                break;
        }
    }
}

