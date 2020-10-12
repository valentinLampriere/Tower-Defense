using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 5f;
    public float hp = 10;
    public int lootedGold = 10;
    public float damagePerSeconds = 1;

    [HideInInspector]
    public bool isSlowed = false;

    List<Transform> destinations;
    GameObject indicators;

    private int destinationIndex = 0;
    private float maxHP;
    private bool reachBase = false;

    private GameManager manager;

    void Start() {
        manager = GameObject.Find("Board").GetComponent<GameManager>();
        indicators = GameObject.Find("Indicators");
        destinations = new List<Transform>();
        for (int i = 0; i < indicators.transform.childCount; i++) {
            destinations.Add(indicators.transform.GetChild(i));
        }
        maxHP = hp;
    }

    protected IEnumerator AttackBase() {
        manager.BaseTakeDamage(1);
        yield return new WaitForSeconds(1 / damagePerSeconds);
        StartCoroutine(AttackBase());
    }

    void Update() {
        if (!reachBase) {
            if (Vector3.Distance(destinations[destinationIndex].position, transform.position) < 0.05f) {
                destinationIndex++;
                if (destinationIndex >= indicators.transform.childCount) {
                    StartCoroutine(AttackBase());
                    reachBase = true;
                    return;
                }
            }
            Vector3 direction = (destinations[destinationIndex].position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    public float GetDistanceNextDestination() {
        if (reachBase) {
            return 0;
        }
        return Vector3.Distance(destinations[destinationIndex].position, transform.position);
    }

    public void TakeDamage(float amountDamage) {
        Color c = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = new Color(c.r * GetHpPercentage(), c.g * GetHpPercentage(), c.b * GetHpPercentage());

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
