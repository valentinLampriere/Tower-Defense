using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //public GameObject indicators;
    GameObject indicators;
    public float speed = 5f;

    List<Transform> destinations;
    [HideInInspector]
    public int destinationIndex = 0;

    void Start() {
        indicators = GameObject.Find("Indicators");
        destinations = new List<Transform>();
        for (int i = 0; i < indicators.transform.childCount; i++) {
            destinations.Add(indicators.transform.GetChild(i));
        }
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
        Debug.Log(destinations);
        return Vector3.Distance(destinations[destinationIndex].position, transform.position);
    }
}
