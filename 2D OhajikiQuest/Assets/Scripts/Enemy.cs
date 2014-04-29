using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject hpBarPrefab;
    public int point = 50;
    public int maxHP = 4;
    public float hpBarW = 0.15f;
    public float hpBarH = 0.10f;
    public float xOffset = 0.03f;
    public float yOffset = 0.05f;

    int hp;

    GameObject gameController;
    GameObject player;
    GameObject gui;


    Transform score;


	void Start () 
    {
        this.gameController = GameObject.FindWithTag("GameController");
        this.player = GameObject.FindWithTag("Player");
        this.gui = GameObject.FindWithTag("GUI");
        this.score = this.gui.transform.Find("BottomBord/SCORE");
        this.hp = maxHP;
        CreateHPBar();
	}
	
	void Update () {
	
	}

    void CreateHPBar()
    {
        Vector2 firstPoint = new Vector2(transform.position.x - (this.hpBarW * 2 + this.xOffset * 2), 0.4f + transform.position.y);//0.3f);

        for (int i = 0; i < this.maxHP; i++)
        {
            int row = Mathf.FloorToInt(i / 4);
            int col = i;
            if (row > 0) // 2段目以降
            {
                col = i - row * 4;
            }

            Vector2 nextPoint = new Vector2(col * (this.hpBarW + this.xOffset), row * (this.hpBarH + this.yOffset));
            GameObject hp = Instantiate(hpBarPrefab, firstPoint + nextPoint, Quaternion.identity) as GameObject;
            hp.transform.parent = gameObject.transform;
        }
    }

    void ReceivedDamage()
    {
        if (this.hp > 0)
        {
            Destroy(transform.GetChild(this.hp - 1).gameObject);
            --this.hp;
        }
        
        if (this.hp == 0)
        {
            Destroy(gameObject);
            //this.player.SendMessage("OnClearFlag");
            this.gameController.SendMessage("UpdateEnemyCount");
            this.score.SendMessage("UpdateScore", this.point);
        }
    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        switch (hit.gameObject.tag)
        {
            case "Player":
                ReceivedDamage();
                break;
        }
    }
}
