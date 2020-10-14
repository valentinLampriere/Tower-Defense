using System.Collections;
using UnityEngine;

public class Canon : Tower
{
    public float fireRate = 2f;
    public int cost = 20;

    [Header("Upgrades")]
    public int upgradeCost = 10;
    public int fireRateUpgrade = 100;
    public int damageUpgrade = 100;
    public int rangeUpgrade = 100;

    protected override void Start() {
        base.Start();
        StartCoroutine(Fire());
    }
    
    protected IEnumerator Fire() {
        Enemy e = GetFirstEnemy();
        if (e != null) {
            e.TakeDamage(damage);
            Debug.DrawRay(transform.position, e.transform.position - transform.position, Color.yellow, 0.08f);
        }
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(Fire());
    }

    public override void Upgrade() {
        base.Upgrade();
        fireRate *= (float)fireRateUpgrade / 100;
        damage *= (float)damageUpgrade / 100;
        range *= (float)rangeUpgrade / 100;
    }

    public override int GetCost() {
        return cost;
    }
    public override int GetUpgradeCost() {
        return upgradeCost;
    }
}
