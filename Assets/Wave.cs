using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    public int amountEnemies = 1;
    public GameObject enemyType;
    public float delay = 2f;
    public float interval = 0.5f;

    private GameObject enemies;
    private Transform firstIndicator;

    private int enemiesSpawned = 0;

    public void Start() {
        firstIndicator = GameObject.Find("Indicators").transform.GetChild(0);
        enemies = GameObject.Find("Enemies");
        if (firstIndicator != null && enemies != null)
            StartCoroutine(DelayEnemies());
    }

    IEnumerator DelayEnemies() {
        yield return new WaitForSeconds(delay);
        StartCoroutine(SetIntervalEnemies());
    }

    IEnumerator SetIntervalEnemies() {
        Vector3 pos = firstIndicator.position;
        GameObject g = Instantiate(enemyType, pos, Quaternion.identity, enemies.transform);
        g.name = "enemy" + enemies.transform.childCount;
        enemiesSpawned++;
        yield return new WaitForSeconds(interval);
        if (enemiesSpawned < amountEnemies)
            StartCoroutine(SetIntervalEnemies());
    }
}
