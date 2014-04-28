using UnityEngine;
using System.Collections;

public class GUI : MonoBehaviour {

    public GUIText fiexdIconText_P;
    GUIText fixedIconText;
    Transform fixedIcon;
    GameObject nextButton;
    GameObject retryButton;
    GameObject resultButton;
    GameObject titleButton;


    void Start()
    {
        this.fixedIcon = transform.Find("FixedIcon");
        CreateText();
    }

    void CreateText()
    {
        Vector2 fixedIconPoint = GetViewportPoint(this.fixedIcon.transform.position);
        this.fixedIconText = Instantiate(this.fiexdIconText_P, fixedIconPoint, Quaternion.identity) as GUIText;
        this.fixedIconText.text = "Free";
    }
    void FixedIconChangeText()
    {
        switch (this.fixedIconText.text)
        {
            case "Fixed":
                this.fixedIconText.text = "Free";
                break;
            case "Free":
                this.fixedIconText.text = "Fixed";
                break;
        }
    }


    // 座標変換
    Vector2 GetViewportPoint(Vector2 position)
    {
        //Debug.Log("WorldPoint : " + position);
        Vector2 point = camera.WorldToViewportPoint(position);
        //Debug.Log("ViewportPoint : " + point);
        return point;
    }
}
