using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int amountEnemies = 5;
    public GameObject enemy;

    private Transform firstIndicator;
    private GameObject enemies;

    private int enemiesSpawned = 0;

    // Start is called before the first frame update
    void Start() {
        firstIndicator = GameObject.Find("Indicators").transform.GetChild(0);
        enemies = GameObject.Find("Enemies");
        if(firstIndicator != null && enemies != null)
            StartCoroutine(DelayEnemies());
    }

    IEnumerator DelayEnemies() {
        Vector3 pos = firstIndicator.position;
        Instantiate(enemy, pos, Quaternion.identity, enemies.transform);
        enemiesSpawned++;
        yield return new WaitForSeconds(0.5f);
        if (enemiesSpawned < amountEnemies)
            StartCoroutine(DelayEnemies());
    }
}
