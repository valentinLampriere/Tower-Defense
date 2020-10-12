using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Min(0)]
    public int amountEnemies = 5;
    [Min(0)]
    public int totalGold = 0;
    [Min(1)]
    public int baseHP = 20;

    public List<Wave> waves;

    public Canon canon;
    public FlameThrower flameThrower;
    public Slower slower;

    private GameObject towers;

    private Tower choosenTower;

    private Text goldText;
    private Text healthText;

    // Start is called before the first frame update
    void Start() {
        towers = GameObject.Find("Towers");
        goldText = GameObject.Find("GoldValue").GetComponent<Text>();
        goldText.text = totalGold.ToString();
        healthText = GameObject.Find("HealthValue").GetComponent<Text>();
        healthText.text = baseHP.ToString();

        foreach (Wave w in waves) {
            Instantiate(w, GameObject.Find("Waves").transform);
        }
    }

    public void BaseTakeDamage(int amountDamage) {
        baseHP -= amountDamage;
        healthText.text = baseHP.ToString();
        if (baseHP <= 0) {
            Debug.Break();
        }
    }

    void Update() {
        if (choosenTower != null && Input.GetMouseButtonDown(0)) {
            if (totalGold - choosenTower.GetCost() < 0) {
                choosenTower = null;
                return;
            }
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                if(hit.transform.parent.name == "Board") {
                    Vector3 towerPos = new Vector3(hit.transform.position.x, towers.transform.position.y, hit.transform.position.z);
                    Instantiate(choosenTower, towerPos, Quaternion.identity, towers.transform);
                    UpdateGold(-choosenTower.GetCost());
                }
            }
            choosenTower = null;
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            choosenTower = null;
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
