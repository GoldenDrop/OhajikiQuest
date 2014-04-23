using UnityEngine;
using System.Collections;

public class FadeManager2 : MonoBehaviour {
    Transform blackoutCurtain;
    float fadeSpeed = 0.5f;          // フェイドの速さ
    float opacity = 0.0f;           // 不透明度
    float maxOpacity = 1.0f;        // 最大不透明度
    Color fadeColor;                // テクスチャの色
    bool isFadeIn = false;          // フェードインのフラグ
    bool isFadeOut = false;         // フェードアウトのフラグ

	void Start () 
    {
        this.blackoutCurtain = gameObject.transform.Find("BlackoutCurtain");
        this.fadeColor = new Color(0, 0, 0, 0);
        this.blackoutCurtain.guiTexture.color = this.fadeColor;
        //OnFadeOutFlag(0.8f);
        //OnFadeInFlag(0.8f);
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
        Debug.Log("OnFadeInFlag");
        this.opacity = 1;
        this.fadeSpeed = speed;
        this.isFadeIn = true;
    }

    void OnFadeOutFlag(float speed)
    {
        this.opacity = 0;
        this.fadeSpeed = speed;
        this.isFadeOut = true;
    }

    void FadeIn()
    {
        Debug.Log("FadeIn opacity : " + this.opacity);
        // 時間が経過する度に不透明度を下げる
        this.opacity -= this.fadeSpeed * Time.deltaTime;
        if (this.opacity < 0)
        {
            this.opacity = 0;
            this.isFadeIn = false;
        }
        this.fadeColor.a = this.opacity;
        this.blackoutCurtain.guiTexture.color = this.fadeColor;
        Debug.Log("opacity : " + this.opacity);
    }

    void FadeOut()
    {
        // 時間が経過する度に不透明度を上げる
        this.opacity += this.fadeSpeed * Time.deltaTime;
        if (this.opacity > this.maxOpacity)
        {
            this.opacity = this.maxOpacity;
            this.isFadeOut = false;
            Debug.Log("FadeOut Finsh");
        }
        this.fadeColor.a = this.opacity;
        this.blackoutCurtain.guiTexture.color = this.fadeColor;
    }
}

