using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Min(0)]
    public int totalGold = 0;
    [Min(1)]
    public int baseHP = 20;

    public List<Wave> waves;
    [HideInInspector]
    public int waveIndex;

    public Canon canon;
    public FlameThrower flameThrower;
    public Slower slower;

    private GameObject towers;

    private Tower choosenTower;

    private Text goldText;
    private Text healthText;

    // Start is called before the first frame update
    void Start() {
        waveIndex = 0;
        towers = GameObject.Find("Towers");
        goldText = GameObject.Find("GoldValue").GetComponent<Text>();
        goldText.text = totalGold.ToString();
        healthText = GameObject.Find("HealthValue").GetComponent<Text>();
        healthText.text = baseHP.ToString();

        NextWave();
    }

    public void NextWave() {
        if (waves.Count == 0) return;
        Wave w = Instantiate(waves[waveIndex], GameObject.Find("Waves").transform).GetComponent<Wave>();
        w.Init(this);
        waveIndex++;
    }

    public void BaseTakeDamage(int amountDamage) {
        baseHP -= amountDamage;
        healthText.text = baseHP.ToString();
        if (baseHP <= 0) {
            Debug.Break();
        }
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100)) {
                if (choosenTower != null) {
                    if (hit.transform.parent.name == "Board") {
                        if (totalGold - choosenTower.GetCost() < 0) {
                            choosenTower = null;
                            return;
                        }
                        Debug.Log(choosenTower.gameObject.transform.localScale.y);
                        Vector3 towerPos = new Vector3(hit.transform.position.x, hit.transform.localScale.y / 2 + 0.1f, hit.transform.position.z);
                        Instantiate(choosenTower, towerPos, Quaternion.identity, towers.transform);
                        UpdateGold(-choosenTower.GetCost());
                    }
                    choosenTower = null;
                }
                if (hit.transform.parent.name == "Towers") {
                    Tower tower = hit.transform.gameObject.GetComponent<Tower>();
                    
                    if (tower != null && tower.CanUpgrade()) {
                        if (totalGold - tower.GetUpgradeCost() < 0) {
                            return;
                        }
                        tower.Upgrade();
                        UpdateGold(-tower.GetUpgradeCost());
                    }
                }
            }
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
