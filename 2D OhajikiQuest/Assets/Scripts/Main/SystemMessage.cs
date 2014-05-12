using UnityEngine;
using System.Collections;

public class SystemMessage : MonoBehaviour {

    Transform score;
    Transform stageObject;
    Transform clearObject;
    Transform defenceObject;
    Transform failureObject;
    Transform battleObject;
    Transform startObject;
    Transform GAMEObject;


    string messageFlag = "WAIT";

    int stageNumber = 1;
    float moveTimer;
    float moveTime = 2.0f;
    float moveSpeed = 2.8f;

    GameObject phaseController;
    GameObject stageController;
    GameObject fadeManager;
    GameObject mainCamera;
    GameObject magicCircle;
    GameObject gui;
    GameObject seManager;
    GameObject bgmPlayer;

    float fadeOutTimer;
    float fadeOutInterval = 1.5f;
    float fadeInTimer;
    float fadeInInterval = 1.5f;
    float massageTimer;
    float massageTime = 1.0f;

    bool isStartedFadeOut = false;
    bool isStartedFadeIn = false;
    bool isMessageStop = false;

    Vector2 topPoint;
    Vector2 bottomPoint;


	void Start () 
    {
        this.phaseController = GameObject.FindWithTag("PhaseController");
        this.stageController = GameObject.FindWithTag("StageController");
        this.fadeManager     = GameObject.FindWithTag("FadeManager");
        this.mainCamera      = GameObject.FindWithTag("MainCamera");
        this.magicCircle     = GameObject.FindWithTag("MagicCircle");
        this.gui             = GameObject.FindWithTag("GUI");
        this.seManager       = GameObject.FindWithTag("SEManager");
        this.bgmPlayer       = GameObject.FindWithTag("BGMPlayer");

        this.score         = this.gui.transform.Find("BottomBord/SCORE");
        this.stageObject   = gameObject.transform.Find("STAGE");
        this.clearObject   = gameObject.transform.Find("CLEAR");
        this.defenceObject = gameObject.transform.Find("DEFENCE");
        this.failureObject = gameObject.transform.Find("FAILURE");
        this.battleObject  = gameObject.transform.Find("BATTLE");
        this.startObject   = gameObject.transform.Find("START");
        this.GAMEObject    = gameObject.transform.Find("GAME");


        this.topPoint    = this.stageObject.transform.position;
        this.bottomPoint = this.clearObject.transform.position;

        Debug.Log("topPoint : " + this.topPoint.x + ", " + this.topPoint.y);
        Debug.Log("bottomPoint : " + this.bottomPoint.x + ", " + this.bottomPoint.y);


        this.moveTimer = this.moveTime;
        this.fadeOutTimer = this.fadeOutInterval;
        this.fadeInTimer = this.fadeInInterval;
        this.massageTimer = this.massageTime;
	}
	
	void Update () 
    {
        switch (this.messageFlag)
        {
            case "CLEAR":
                ClearMessage();
                break;

            case "START":
                BattleStartMessage();
                break;

            case "GAMEOVER":
                GameOverMessage();
                break;

            case "GAMECLEAR":
                GameClearMessage();
                break;
        }
	}

    void ClearMessage()
    {
        this.moveTimer -= Time.deltaTime;
        if (this.moveTimer > 0)
        {
            this.stageObject.transform.Translate(-Vector2.up * this.moveSpeed * Time.deltaTime);
            this.clearObject.transform.Translate(Vector2.up * this.moveSpeed * Time.deltaTime);
        }
        if (this.moveTimer < 0)
        {
            this.fadeOutTimer -= Time.deltaTime;
            if (!this.isStartedFadeOut)
            {
                this.bgmPlayer.SendMessage("Stop");
                float fadeOutSpeed = 0.7f;
                this.fadeManager.SendMessage("OnFadeOutFlag", fadeOutSpeed);
                this.isStartedFadeOut = true;
            }

            if (this.fadeOutTimer < 0)
            {
                if (!this.isStartedFadeIn)
                {
                    this.stageObject.transform.localPosition = this.topPoint;
                    this.clearObject.transform.localPosition = this.bottomPoint;
                    this.magicCircle.SendMessage("InitializedPlayer");
                    this.stageController.SendMessage("NextStage");
                    this.phaseController.SendMessage("ResetTurns");
                    float fadeInSpeed = 0.7f;
                    this.fadeManager.SendMessage("OnFadeInFlag", fadeInSpeed);
                    this.isStartedFadeIn = true;
                }
                this.fadeInTimer -= Time.deltaTime;
                if (this.fadeInTimer < 0)
                {
                    Debug.Log("fadeInTimer 0");
                    this.isStartedFadeOut = false;
                    this.isStartedFadeIn = false;
                    this.moveTimer = this.moveTime;
                    this.fadeOutTimer = this.fadeOutInterval;
                    this.fadeInTimer = this.fadeInInterval;
                    string flag = "START";
                    OnFlag(flag);
                }
            }

        }
    }

    void GameOverMessage()
    {
        Debug.Log("GameOverMessage");
        this.moveTimer -= Time.deltaTime;
        if (this.moveTimer > 0)
        {
            this.defenceObject.transform.Translate(-Vector2.up * this.moveSpeed * Time.deltaTime);
            this.failureObject.transform.Translate(Vector2.up * this.moveSpeed * Time.deltaTime);
        }
        if (this.moveTimer < 0)
        {
            this.fadeOutTimer -= Time.deltaTime;
            if (!this.isStartedFadeOut)
            {
                this.bgmPlayer.SendMessage("Stop");
                float fadeOutSpeed = 0.7f;
                this.fadeManager.SendMessage("OnFadeOutFlag", fadeOutSpeed);
                this.isStartedFadeOut = true;
            }

            if (this.fadeOutTimer < 0)
            {
                if (!this.isStartedFadeIn)
                {
                    this.magicCircle.SendMessage("InitializedPlayer");
                    this.defenceObject.transform.localPosition = this.topPoint;
                    this.failureObject.transform.localPosition = this.bottomPoint;
                    string place = "GAMEOVER";
                    this.mainCamera.SendMessage("Move", place);
                    float fadeInSpeed = 0.7f;
                    this.fadeManager.SendMessage("OnFadeInFlag", fadeInSpeed);
                    this.isStartedFadeIn = true;
                }
                this.fadeInTimer -= Time.deltaTime;
                if (this.fadeInTimer < 0)
                {
                    this.isStartedFadeOut = false;
                    this.isStartedFadeIn = false;
                    this.moveTimer = this.moveTime;
                    this.fadeOutTimer = this.fadeOutInterval;
                    this.fadeInTimer = this.fadeInInterval;
                    this.phaseController.SendMessage("SetPhase", (int)Phase.PresentPhase.GameOver);
                    this.messageFlag = "WAIT";
                }
            }

        }      
    }

    void BattleStartMessage()
    {
        this.moveTimer -= Time.deltaTime;
        if (this.moveTimer > 0)
        {
            this.battleObject.transform.Translate(-Vector2.up * this.moveSpeed * Time.deltaTime);
            this.startObject.transform.Translate(Vector2.up * this.moveSpeed * Time.deltaTime);
        }
        if (this.moveTimer < 0)
        {
            this.massageTimer -= Time.deltaTime;
            if (this.massageTimer < 0)
            {
                this.seManager.SendMessage("Play", SE.SE1);
                this.battleObject.transform.localPosition = this.topPoint;
                this.startObject.transform.localPosition = this.bottomPoint;
                this.phaseController.SendMessage("SetPhase", (int)Phase.PresentPhase.Player);
                this.messageFlag = "WAIT";
                this.moveTimer = this.moveTime;
                this.massageTimer = this.massageTime;
            }
        }
    }

    void GameClearMessage()
    {
        this.moveTimer -= Time.deltaTime;
        if (this.moveTimer > 0)
        {
            this.GAMEObject.transform.Translate(-Vector2.up * this.moveSpeed * Time.deltaTime);
            this.clearObject.transform.Translate(Vector2.up * this.moveSpeed * Time.deltaTime);
        }
        if (this.moveTimer < 0)
        {
            this.fadeOutTimer -= Time.deltaTime;
            if (!this.isStartedFadeOut)
            {
                this.bgmPlayer.SendMessage("Stop");
                float fadeOutSpeed = 0.7f;
                this.fadeManager.SendMessage("OnFadeOutFlag", fadeOutSpeed);
                this.isStartedFadeOut = true;
            }

            if (this.fadeOutTimer < 0)
            {
                if (!this.isStartedFadeIn)
                {
                    this.magicCircle.SendMessage("InitializedPlayer");
                    this.GAMEObject.transform.localPosition = this.topPoint;
                    this.clearObject.transform.localPosition = this.bottomPoint;
                    string place = "RESULT";
                    this.mainCamera.SendMessage("Move", place);

                    // 値をResultに送る
                    this.score.SendMessage("SendToResultScore");
                    string from = "GAMECLEAR";
                    this.stageController.SendMessage("SendToResultClearStage", from);
                    this.phaseController.SendMessage("SendToResultTotalTurn");

                    // 初期化　Score,PlayerTurn, EnemyTurn, TotalTurn  ステージ削除・作成
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
                    this.bgmPlayer.SendMessage("Play", BGM.BGM7);
                    this.isStartedFadeOut = false;
                    this.isStartedFadeIn = false;
                    this.moveTimer = this.moveTime;
                    this.fadeOutTimer = this.fadeOutInterval;
                    this.fadeInTimer = this.fadeInInterval;
                    this.phaseController.SendMessage("SetPhase", (int)Phase.PresentPhase.Result);
                    this.messageFlag = "WAIT";
                }
            }

        }      
    }

    void OnFlag(string flag)
    {
        Debug.Log("messageFlag = " + flag);
        this.messageFlag = flag;
        string se = "";
        float volume = 0;

        switch (this.messageFlag)
        {
            case "CLEAR":
                se = SE.SE11;
                volume = 0.05f;
                break;

            case "GAMEOVER":
                se = SE.SE5;
                volume = 0.05f;
                break;

            case "GAMECLEAR":
                se = SE.SE4;
                volume = 0.05f;
                break;   
            case "START":
                string selectBGM = SelectStageBGM(this.stageNumber);
                this.bgmPlayer.SendMessage("Play", selectBGM);
                volume = 0.2f;
                break;
        }

        this.bgmPlayer.SendMessage("SetVolume", volume);
        this.seManager.SendMessage("Play", se);
    }

    void CatchStageNumber(int number)
    {
        this.stageNumber = number;
    }

    string SelectStageBGM(int stage)
    {
        string bgm = "";
        switch (stage)
        {
            case 1:
                bgm = BGM.BGM1;
                break;
            case 2:
                bgm = BGM.BGM2;
                break;
            case 3:
                bgm = BGM.BGM3;
                break;
            case 4:
                bgm = BGM.BGM5;
                break;
        }

        return bgm;
    }
}
