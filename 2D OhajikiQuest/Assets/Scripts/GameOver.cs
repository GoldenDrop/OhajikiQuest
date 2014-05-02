using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
    GameObject phaseController;
    GameObject stageController;
    GameObject fadeManager;
    GameObject mainCamera;
    GameObject gui;
    GameObject systemMessage;
    GameObject seManager;
    GameObject bgmPlayer;


    RaycastHit2D hit;
    int phase = 0;
    GetPhase getPhase; // 現行フェイズ取得クラス
    Transform score;


    float fadeOutTimer;
    float fadeOutInterval = 1.5f;
    float fadeInTimer;
    float fadeInInterval = 1.5f;

    bool isStartedFadeOut = false;
    bool isStartedFadeIn  = false;
    bool onRetry          = false;
    bool onExit           = false;


	void Start () 
    {
        this.getPhase        = gameObject.GetComponent<GetPhase>();
        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.stageController = GameObject.FindWithTag("StageController");
        this.fadeManager     = GameObject.FindWithTag("FadeManager");
        this.mainCamera      = GameObject.FindWithTag("MainCamera");
        this.systemMessage   = GameObject.FindWithTag("SystemMessage");
        this.gui             = GameObject.FindWithTag("GUI");
        this.seManager       = GameObject.FindWithTag("SEManager");
        this.bgmPlayer       = GameObject.FindWithTag("BGMPlayer");

        this.score           = this.gui.transform.Find("BottomBord/SCORE");
        this.fadeOutTimer    = this.fadeOutInterval;
        this.fadeInTimer     = this.fadeInInterval;
	}
	
	void Update () 
    {
        this.phase = this.getPhase.GetNowPhase();
        if (this.phase == 4)
        {
            if (this.onRetry)
            {
                ReTry();
            }

            if (this.onExit)
            {
                GoToResult();
            }

            if (!this.onRetry & !this.onExit)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    // Raycast(光線の出る位置, 光線の向き)
                    this.hit = Physics2D.Raycast(mousePoint, Vector2.zero);
                    if (this.hit)
                    {
                        GameObject selectedObject = this.hit.collider.gameObject;
                        switch (selectedObject.name)
                        {
                            case "ExitButton":
                                Debug.Log("ExitButton Click");
                                this.onExit = true;
                                break;

                            case "RetryButton":
                                Debug.Log("RetryButton Click");
                                this.onRetry = true;
                                break;
                        }
                        string se = "Click";
                        this.seManager.SendMessage("Play", se);
                    }
                }
            }
        }
	}

    void GoToResult()
    {
        // Rsultに値を送る　Score, TtalTurn ClearStage
        // 初期化　Score,PlayerTurn,EnemyTurn,TotalTurn,StageNumber
        this.fadeOutTimer -= Time.deltaTime;
        if (!this.isStartedFadeOut)
        {
            float fadeOutSpeed = 0.7f;
            this.fadeManager.SendMessage("OnFadeOutFlag", fadeOutSpeed);
            this.isStartedFadeOut = true;
        }

        if (this.fadeOutTimer < 0)
        {
            Debug.Log("Retry FadeInStart");
            if (!this.isStartedFadeIn)
            {
                string place = "RESULT";
                this.mainCamera.SendMessage("Move", place);
                // 値をResultに送る
                this.score.SendMessage("SendToResultScore");
                string from = "GAMEOVER";
                this.stageController.SendMessage("SendToResultClearStage", from);
                this.phaseController.SendMessage("SendToResultTotalTurn");

                // 初期化　Score,PlayerTurn, EnemyTurn, TotalTurn ステージ削除・作成
                this.phaseController.SendMessage("ResetTurns");
                this.phaseController.SendMessage("ResetTotalTurn");
                this.score.SendMessage("ResetScore");
                this.stageController.SendMessage("RestStage");
                float fadeInSpeed = 0.7f;
                this.fadeManager.SendMessage("OnFadeInFlag", fadeInSpeed);
                this.isStartedFadeIn = true;
            }
            this.fadeInTimer -= Time.deltaTime;
            if (this.fadeInTimer < 0)
            {
                string selectBGM = "RESULT";
                this.bgmPlayer.SendMessage("Play", selectBGM);
                int phase = 5;
                this.phaseController.SendMessage("SetPhase", phase);
                this.fadeOutTimer = this.fadeOutInterval;
                this.fadeInTimer = this.fadeInInterval;
                this.onExit = false;
                this.isStartedFadeOut = false;
                this.isStartedFadeIn = false;
            }
        }
       
    }

    void ReTry()
    {
        this.fadeOutTimer -= Time.deltaTime;
        if (!this.isStartedFadeOut)
        {
            float fadeOutSpeed = 0.7f;
            this.fadeManager.SendMessage("OnFadeOutFlag", fadeOutSpeed);
            this.isStartedFadeOut = true;
        }

        if (this.fadeOutTimer < 0)
        {
            Debug.Log("Retry FadeInStart");
            if (!this.isStartedFadeIn)
            {
                string place = "STAGE";
                this.mainCamera.SendMessage("Move", place);
                // 初期化　Score,PlayerTurn,EnemyTurn ステージ削除・作成
                this.phaseController.SendMessage("ResetTurns", 0);
                this.score.SendMessage("ResetScore");
                this.stageController.SendMessage("RetryStage");
                float fadeInSpeed = 0.7f;
                this.fadeManager.SendMessage("OnFadeInFlag", fadeInSpeed);
                this.isStartedFadeIn = true;
            }
            this.fadeInTimer -= Time.deltaTime;
            if (this.fadeInTimer < 0)
            {
                int phase = 0;
                this.phaseController.SendMessage("SetPhase", phase);
                this.fadeOutTimer = this.fadeOutInterval;
                this.fadeInTimer = this.fadeInInterval;
                this.onRetry = false;
                this.isStartedFadeOut = false;
                this.isStartedFadeIn = false;
                string flag = "START";
                this.systemMessage.SendMessage("OnFlag", flag);
            }
        }
    }
}
