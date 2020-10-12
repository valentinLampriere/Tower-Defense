﻿using System.Collections;
using UnityEngine;

public class Canon : Tower {
    public float fireRate = 1f;
    public int cost = 20;

    protected override void Start() {
        base.Start();
        StartCoroutine(Fire());
    }
    
    protected IEnumerator Fire() {
        Enemy e = GetFirstEnemy();
        if (e != null)
            e.TakeDamage(damage);
        

        yield return new WaitForSeconds(fireRate);
        StartCoroutine(Fire());
    }

    public override int GetCost() {
        return cost;
    }
}
