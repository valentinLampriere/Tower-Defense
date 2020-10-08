using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public float damage = 2.5f;
    public float range = 3f;

    protected GameObject enemies;

    protected virtual void Start() {
        Debug.Log("Tower");
        enemies = GameObject.Find("Enemies");
    }

    protected Enemy GetFirstEnemy() {
        Enemy firstEnemy = null;

        if (enemies.transform.childCount == 0)
            return null;
        for (int i = 0; i < enemies.transform.childCount; i++) {
            if (Vector3.Distance(enemies.transform.GetChild(i).position, transform.position) <= range) {
                Enemy e = enemies.transform.GetChild(i).gameObject.GetComponent<Enemy>();
                if (e != null) {
                    if(firstEnemy == null) {
                        firstEnemy = e;
                    } else if (e.GetDestinationIndex() >= firstEnemy.GetDestinationIndex()) {
                        if (e.GetDistanceNextDestination() < firstEnemy.GetDistanceNextDestination()) {
                            firstEnemy = e;
                        }
                    }
                }
            }
        }
        return firstEnemy;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, range);
    }
}
