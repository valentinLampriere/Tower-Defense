using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float fireRate = 1f;
    public float range = 3f;


    private GameObject enemies;

    void Start() {
        enemies = GameObject.Find("Enemies");
        StartCoroutine(Fire());
    }

    IEnumerator Fire() {
        Enemy e = GetFirstEnemy();
        if (e != null) {
            Debug.Log(e.name);
        }
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(Fire());
    }

    private Enemy GetFirstEnemy() {
        Enemy firstEnemy;

        if (enemies.transform.childCount == 0)
            return null;

        firstEnemy = enemies.transform.GetChild(0).gameObject.GetComponent<Enemy>();

        for (int i = 0; i < enemies.transform.childCount; i++) {
            if (Vector3.Distance(enemies.transform.GetChild(i).position, transform.position) <= range) {
                Enemy e = enemies.transform.GetChild(i).gameObject.GetComponent<Enemy>();
                if (e != null) {
                    if (e.destinationIndex >= firstEnemy.destinationIndex) {
                        if (e.GetDistanceNextDestination() > firstEnemy.GetDistanceNextDestination()) {
                            firstEnemy = e;
                        }
                    }
                }
            }
        }
        return firstEnemy;
    }
}
