using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
    Transform buttonText;
	
	void Start () 
    {
        this.buttonText = transform.GetChild(0);
	}

    void SetText (string str)
    {
        string button = str;
        this.buttonText.guiText.text = button;
    }

    void MoveText (Vector2 point)
    {
        Debug.Log("MoveText");
        Vector3 textPoint = new Vector3(point.x, point.y, 0);
        this.buttonText.localPosition = textPoint;
    }
}
