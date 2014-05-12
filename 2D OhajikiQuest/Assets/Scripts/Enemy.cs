using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int point = 50;
    public float maxHP = 1.0f;
    public GameObject hitPrefab;
    public GameObject explosionPrefab;

    float xOffset = 0.5f;

    Vector3 hpBarFirstScale;

    float hp;

    GameObject gameController;
    GameObject player;
    GameObject gui;
    GameObject seManager;


    Transform score;
    Transform hpBarRed;


	void Start () 
    {
        this.seManager = GameObject.FindWithTag("SEManager");
        this.gameController = GameObject.FindWithTag("GameController");
        this.player = GameObject.FindWithTag("Player");
        this.gui = GameObject.FindWithTag("GUI");
        this.score = this.gui.transform.Find("BottomBord/SCORE");
        this.hpBarRed = gameObject.transform.Find("HpBar/HpBarRed");
        this.hpBarFirstScale = this.hpBarRed.localScale;
        Debug.Log("hpBarFirstScale : " + this.hpBarFirstScale.x + ", " + this.hpBarFirstScale.y + ", " + this.hpBarFirstScale.z);

        this.hp = maxHP;
	}
	
	void Update () {
	
	}


    void ReceivedDamage()
    {
        GameObject effect = null;
        if (this.hp > 0)
        {
            effect = this.hitPrefab;
            --this.hp;
            float n = this.hp / this.maxHP;
            this.hpBarRed.localScale = new Vector3(this.hpBarFirstScale.x * (this.hp / this.maxHP), this.hpBarFirstScale.y, this.hpBarFirstScale.z);
        }
        
    
        if (this.hp == 0)
        {
            effect = this.explosionPrefab;
            Destroy(gameObject);
            this.gameController.SendMessage("UpdateEnemyCount");
            this.score.SendMessage("UpdateScore", this.point);
        }

        Instantiate(effect, transform.position, Quaternion.identity);
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
