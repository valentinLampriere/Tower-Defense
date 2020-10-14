using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    //public int amountEnemies = 1;
    //public GameObject enemyType;
    public List<GameObject> enemies;
    public float delayBeforeWave = 2f;
    public float intervalBetweenEnemies = 0.5f;

    private GameObject enemiesParent;
    private Transform firstIndicator;
    private int enemyIndex = 0;

    private GameManager manager;

    public void Init(GameManager gm) {
        manager = gm;
        firstIndicator = GameObject.Find("Indicators").transform.GetChild(0);
        enemiesParent = GameObject.Find("Enemies");
        if (firstIndicator != null && enemiesParent != null)
            StartCoroutine(DelayEnemies());
    }

    IEnumerator DelayEnemies() {
        yield return new WaitForSeconds(delayBeforeWave);
        StartCoroutine(SetIntervalEnemies());
    }

    IEnumerator SetIntervalEnemies() {
        Vector3 pos = firstIndicator.position;
        GameObject g = Instantiate(enemies[enemyIndex], pos, Quaternion.identity, enemiesParent.transform);
        g.name = "enemy" + enemies[enemyIndex].transform.childCount;
        enemyIndex++;
        if (enemyIndex < enemies.Count) {
            yield return new WaitForSeconds(intervalBetweenEnemies);
            StartCoroutine(SetIntervalEnemies());
        } else {
            if (manager.waveIndex < manager.waves.Count) {
                manager.NextWave();
            }
        }
    }
}
