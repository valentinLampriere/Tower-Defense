using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Tower {
    protected override void Fire() {
        Enemy e = GetFirstEnemy();
        if(e != null)
            e.TakeDamage(damage);
    }
}
