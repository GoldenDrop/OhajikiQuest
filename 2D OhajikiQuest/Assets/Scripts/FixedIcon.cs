using UnityEngine;
using System.Collections;

public class FixedIcon : MonoBehaviour {

    public GUIText fiexdIconText_P;
    GUIText fiexdIconText;

	void Start () 
    {
        CreateText();
	}

    void CreateText()
    {
        //Vector2(0.8f, 0.07f)
        Vector2 textPoint = GetTextPoint();
        this.fiexdIconText = Instantiate(this.fiexdIconText_P, textPoint, Quaternion.identity) as GUIText;
        this.fiexdIconText.text = "Free";
    }
    void ChangeText()
    {
        switch (this.fiexdIconText.text)
        {
            case "Fixed":
                this.fiexdIconText.text = "Free";
                break;
            case "Free":
                this.fiexdIconText.text = "Fixed";
                break;
        } 
    }

    Vector2 GetTextPoint()
    {
        Vector2 point = camera.WorldToScreenPoint(transform.position);
        return point;
    }
}
