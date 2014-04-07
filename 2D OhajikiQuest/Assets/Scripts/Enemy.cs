﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject hpBarPrefab;
    public int maxHP = 4;
    public float hpBarW = 0.15f;
    public float hpBarH = 0.10f;
    public float xOffset = 0.03f;
    public float yOffset = 0.05f;

	void Start () 
    {
        CreateHPBar();
	}
	
	void Update () {
	
	}

    void CreateHPBar()
    {
        Vector2 firstPoint = new Vector2(0 - (this.hpBarW * 2 + this.xOffset * 2), this.yOffset + 0.3f);

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
}
