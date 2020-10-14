using UnityEngine;

public class FlameThrower : Tower {

    public float impactRange = 0.5f;
    public int cost = 25;

    [Header("Upgrades")]
    public int upgradeCost = 12;
    public int impactRangeUpgrade = 100;
    public int damageUpgrade = 100;
    public int rangeUpgrade = 100;

    private void FixedUpdate() {
        Enemy targetEnemy = GetFirstEnemy();
        if (targetEnemy != null)
            for (int i = 0; i < enemies.transform.childCount; i++) {
                if (Vector3.Distance(enemies.transform.GetChild(i).position, targetEnemy.transform.position) <= impactRange) {
                    Enemy e = enemies.transform.GetChild(i).gameObject.GetComponent<Enemy>();
                    if (e != null) {
                        e.TakeDamage(damage);
                    }
                }
            }
    }

    public override void Upgrade() {
        base.Upgrade();
        impactRange *= (float)impactRangeUpgrade / 100;
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
