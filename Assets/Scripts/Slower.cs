using UnityEngine;

public class Slower : Tower {

    [Min(0)]
    public float speedMultiplicateur = 0.5f;
    public int cost = 15;

    [Header("Upgrades")]
    public int upgradeCost = 15;
    public int speedMultiplicateurUpgrade = 100;
    public int rangeUpgrade = 100;

    public int towerCost = 15;

    private void FixedUpdate() {
        for (int i = 0; i < enemies.transform.childCount; i++) {
            Enemy e = enemies.transform.GetChild(i).gameObject.GetComponent<Enemy>();
            if (Vector3.Distance(enemies.transform.GetChild(i).position, transform.position) <= range) {
                if (e != null && !e.isSlowed) {
                    e.isSlowed = true;
                    e.speed *= speedMultiplicateur;
                }
            } else {
                if (e != null && e.isSlowed) {
                    e.isSlowed = false;
                    e.speed /= speedMultiplicateur;
                }
            }
        }
    }

    public override void Upgrade() {
        base.Upgrade();
        if (CanUpgrade()) {
            speedMultiplicateur *= (float)speedMultiplicateurUpgrade / 100;
            range *= (float)rangeUpgrade / 100;
            currentLevel++;
        }
    }

    public override int GetCost() {
        return cost;
    }
    public override int GetUpgradeCost() {
        return upgradeCost;
    }
}
