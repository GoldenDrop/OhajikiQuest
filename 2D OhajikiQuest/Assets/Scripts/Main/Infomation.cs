using UnityEngine;
using System.Collections;

public class Infomation : MonoBehaviour {

    Quaternion rotation;
    public float limitAngle = 30.0f;
    float minAngle;
    float maxAngle;
    Vector2 startPosition;
    Vector2 position;
    SpriteRenderer infoRenderer;
    void Start()
    {
        this.infoRenderer = GetComponent<SpriteRenderer>();
        this.startPosition = transform.position;
        ChangeTransparency(0);
    }


    void Turn(float angle)
    {
        Debug.Log("Turn angle : " + angle);
        this.rotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = rotation;
        // float thetaAngle = (90 - this.angle) * Mathf.Rad2Deg;
        //Vector2 deltaPosition = new Vector2(Mathf.Cos(thetaAngle), Mathf.Sin(thetaAngle));
        //this.position = transform.position;
        //Vector2 turnPosition = this.position - deltaPosition;
        // 上手くいってない　後回し

        //transform.localPosition = turnPosition;
    }

    /*
    void RestPosition()
    {
        transform.localPosition = this.startPosition;
    }*/

    void ChangeTransparency(float alpha) // 透明度変更
    {
        Debug.Log("ChangeTransparency Alpha : " + alpha);
        Color infomationColor = new Color(1, 1, 1, alpha);
        this.infoRenderer.color = infomationColor;
    }
}
