using UnityEngine;
using System.Collections;

public class Infomation : MonoBehaviour {

    Quaternion rotation;
    public float limitAngle = 30.0f;
    SpriteRenderer infoRenderer;


    void Start()
    {
        this.infoRenderer = GetComponent<SpriteRenderer>();
        ChangeTransparency(0);
    }


    void Turn(float angle)
    {
        Debug.Log("Turn angle : " + angle);
        this.rotation.eulerAngles = new Vector3(0, 0, angle);
        transform.rotation = rotation;
    }


    void ChangeTransparency(float alpha) // 透明度変更
    {
        Debug.Log("ChangeTransparency Alpha : " + alpha);
        Color infomationColor = new Color(1, 1, 1, alpha);
        this.infoRenderer.color = infomationColor;
    }
}
