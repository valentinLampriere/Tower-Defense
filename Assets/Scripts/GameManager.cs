using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int amountEnemies = 5;
    public GameObject enemy;
    public int totalGold = 0;
    public int baseHP = 20;

    private Transform firstIndicator;
    private GameObject enemies;

    private int enemiesSpawned = 0;

    private Text goldText;

    // Start is called before the first frame update
    void Start() {
        firstIndicator = GameObject.Find("Indicators").transform.GetChild(0);
        enemies = GameObject.Find("Enemies");
        if(firstIndicator != null && enemies != null)
            StartCoroutine(DelayEnemies());
        goldText = GameObject.Find("GoldValue").GetComponent<Text>();
    }

    IEnumerator DelayEnemies() {
        Vector3 pos = firstIndicator.position;
        GameObject g = Instantiate(enemy, pos, Quaternion.identity, enemies.transform);
        g.GetComponent<Enemy>().Init(this);
        g.name = "enemy" + enemies.transform.childCount;
        enemiesSpawned++;
        yield return new WaitForSeconds(0.5f);
        if (enemiesSpawned < amountEnemies)
            StartCoroutine(DelayEnemies());
    }

    public void BaseTakeDamage(int amountDamage) {
        baseHP -= amountDamage;
        if (baseHP <= 0) {
            Debug.Break();
        }
    }

    public void UpdateGold(int goldAdd) {
        totalGold += goldAdd;
        goldText.text = totalGold.ToString();
    }
}
