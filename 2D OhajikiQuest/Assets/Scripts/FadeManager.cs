using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour {
    public GUITexture fadePrefab;   // テクスチャのPrefab
    GUITexture fade;
    float fadeSpeed = 0.5f;          // フェイドの速さ
    float opacity = 0.0f;           // 不透明度
    float maxOpacity = 1.0f;        // 最大不透明度
    Color fadeColor;                // テクスチャの色
    bool isFadeIn = false;          // フェードインのフラグ
    bool isFadeOut = false;         // フェードアウトのフラグ

	void Start () 
    {
        CreateFade();
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

    void CreateFade()
    {
        // テクスチャの色を黒,透明にする
        this.fadeColor = Color.black;
        this.fadeColor.a = this.opacity;

        // テクスチャの生成　大きさは画面サイズにする
        this.fade = Instantiate(fadePrefab, Vector2.zero, Quaternion.identity) as GUITexture;
        this.fade.transform.position = Vector3.zero;
        this.fade.transform.localScale = Vector3.zero;
        this.fade.guiTexture.pixelInset = new Rect(Screen.width, Screen.height, -Screen.width, -Screen.height);

        // 色を反映させる
        this.fade.guiTexture.color = this.fadeColor;
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
        this.fade.guiTexture.color = this.fadeColor;
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
        this.fade.guiTexture.color = this.fadeColor;
        if (this.opacity > this.maxOpacity)
        {
            this.isFadeOut = false;
        }
    }
}
