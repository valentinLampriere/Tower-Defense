using UnityEngine;

public class FlameThrower : Tower {

    public float impactRange = 0.5f;
    public int cost = 25;

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

    public override int GetCost() {
        return cost;
    }
}
