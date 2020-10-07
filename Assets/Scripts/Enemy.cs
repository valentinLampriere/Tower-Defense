using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 5f;
    public float hp = 10;
    public int lootedGold = 10;

    List<Transform> destinations;
    GameObject indicators;
    GameManager manager;

    private int destinationIndex = 0;
    private float maxHP;

    void Start() {
        indicators = GameObject.Find("Indicators");
        destinations = new List<Transform>();
        for (int i = 0; i < indicators.transform.childCount; i++) {
            destinations.Add(indicators.transform.GetChild(i));
        }
        maxHP = hp;
    }
    public void Init(GameManager manager) {
        this.manager = manager;
    }

    void Update() {
        if (Vector3.Distance(destinations[destinationIndex].position, transform.position) < 0.2f) {
            destinationIndex++;
            if (destinationIndex >= indicators.transform.childCount) {
                Destroy(gameObject);
                return;
            }
        }
        Vector3 direction = (destinations[destinationIndex].position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public float GetDistanceNextDestination() {
        return Vector3.Distance(destinations[destinationIndex].position, transform.position);
    }

    public void TakeDamage(float amountDamage) {
        hp -= amountDamage;
        if(hp <= 0) {
            manager.UpdateGold(lootedGold);
            Destroy(gameObject);
        }
    }

    public int GetDestinationIndex() {
        return destinationIndex;
    }

    public float GetHpPercentage() {
        return (hp / maxHP);
    }
}
