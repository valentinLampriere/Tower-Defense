using UnityEngine;

public class Slower : Tower {
    [Min(0)]
    public float speedMultiplicateur = 0.5f;
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
}
