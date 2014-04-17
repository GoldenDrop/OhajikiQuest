using UnityEngine;
using System.Collections;

public class FadeManager2 : MonoBehaviour {
    GameObject blackoutCurtainObject;   // 暗幕オブジェクト
    Transform blackoutCurtain;
    float fadeSpeed = 0.5f;          // フェイドの速さ
    float opacity = 1.0f;           // 不透明度
    float maxOpacity = 1.0f;        // 最大不透明度
    Color fadeColor;                // テクスチャの色
    bool isFadeIn = false;          // フェードインのフラグ
    bool isFadeOut = false;         // フェードアウトのフラグ

	void Start () 
    {
        this.blackoutCurtainObject = GameObject.FindWithTag("BlackoutCurtain");
        this.blackoutCurtain = this.blackoutCurtainObject.transform.Find("BlackoutCurtain");
        this.fadeColor = Color.black;
        OnFadeInFlag(0.8f);
	}
	
	void Update () 
    {
        if (this.isFadeIn)
        {
            FadeIn();
        }

        if (this.isFadeOut)
        {
            FadeOut();
        }
	}

    

    void OnFadeInFlag(float speed)
    {
        this.fadeSpeed = speed;
        this.isFadeIn = true;
    }

    void OnFadeOutFlag(float speed)
    {
        this.fadeSpeed = speed;
        this.isFadeOut = true;
    }

    void FadeIn()
    {
        // 時間が経過する度に不透明度を下げる
        this.opacity -= this.fadeSpeed * Time.deltaTime;
        this.fadeColor.a = this.opacity;
        this.blackoutCurtain.guiTexture.color = this.fadeColor;
        if (this.opacity < 0)
        {
            this.isFadeIn = false;
        }
    }

    void FadeOut()
    {
        // 時間が経過する度に不透明度を上げる
        this.opacity += this.fadeSpeed * Time.deltaTime;
        this.fadeColor.a = this.opacity;
        this.blackoutCurtain.guiTexture.color = this.fadeColor;
        if (this.opacity > this.maxOpacity)
        {
            this.isFadeOut = false;
        }
    }
}

