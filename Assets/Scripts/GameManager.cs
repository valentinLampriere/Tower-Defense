using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Min(0)]
    public int amountEnemies = 5;
    public GameObject enemy;
    [Min(0)]
    public int totalGold = 0;
    [Min(1)]
    public int baseHP = 20;

    public Canon canon;
    public FlameThrower flameThrower;
    public Slower slower;

    private Transform firstIndicator;
    private GameObject enemies;
    private GameObject towers;

    private Tower choosenTower;

    private int enemiesSpawned = 0;

    private Text goldText;

    // Start is called before the first frame update
    void Start() {
        firstIndicator = GameObject.Find("Indicators").transform.GetChild(0);
        enemies = GameObject.Find("Enemies");
        towers = GameObject.Find("Towers");
        if (firstIndicator != null && enemies != null)
            StartCoroutine(DelayEnemies());
        goldText = GameObject.Find("GoldValue").GetComponent<Text>();
        goldText.text = totalGold.ToString();
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

    void Update() {
        if (choosenTower != null && Input.GetMouseButtonUp(0)) {
            Debug.Log(choosenTower);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(choosenTower, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity, towers.transform);
        }
    }

    public void UpdateGold(int goldAdd) {
        totalGold += goldAdd;
        goldText.text = totalGold.ToString();
    }

    public void AddCanon() {
        choosenTower = canon;
    }
    public void AddFlameThrower() {
        choosenTower = flameThrower;
    }
    public void AddSlower() {
        choosenTower = slower;
    }
}
