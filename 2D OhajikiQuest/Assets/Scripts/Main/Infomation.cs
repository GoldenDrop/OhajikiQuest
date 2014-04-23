using UnityEngine;
using System.Collections;

public class Infomation : MonoBehaviour {

    Quaternion rotation;
    public float limitAngle = 30.0f;
    float angle;
    float minAngle;
    float maxAngle;
    Vector2 startPosition;
    Vector2 position;
    SpriteRenderer renderer;
    void Start()
    {
        this.renderer = GetComponent<SpriteRenderer>();
        this.startPosition = transform.position;
        this.minAngle = -90 + limitAngle;
        this.maxAngle = 90 - limitAngle;
        ChangeTransparency(0);
    }

    void GetAngle(Vector2 delta)
    {
        this.angle = Mathf.Atan2(delta.x, delta.y) * Mathf.Rad2Deg * -1;

        // 角度制限
        if (this.angle < this.minAngle)
        {
            this.angle = this.minAngle;
        }
        else if (this.angle > this.maxAngle)
        {
            this.angle = this.maxAngle;
        }
        //Debug.Log("(GetAngle) angle = " + this.angle + "°");
    }

    void Turn(float angle)
    {
        this.rotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = rotation;
        // float thetaAngle = (90 - this.angle) * Mathf.Rad2Deg;
        //Vector2 deltaPosition = new Vector2(Mathf.Cos(thetaAngle), Mathf.Sin(thetaAngle));
        //this.position = transform.position;
        //Vector2 turnPosition = this.position - deltaPosition;
        // 上手くいってない　後回し

        //transform.localPosition = turnPosition;
    }

    void RestPositionAndRatation()
    {
        transform.localPosition = this.startPosition;
        this.rotation.eulerAngles = Vector3.zero;
        transform.rotation = rotation;
    }

    void ChangeTransparency(float alpha) // 透明度変更
    {
        Debug.Log("ChangeTransparency Alpha : " + alpha);
        Color infomationColor = new Color(1, 1, 1, alpha);
        this.renderer.color = infomationColor;
    }
}
