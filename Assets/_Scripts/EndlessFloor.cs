using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessFloor : MonoBehaviour
{
    public Transform player;
    public GameObject floorPrefab;
    public GameObject[] floors;
    
    private float offset;
    private float scale;
    private int currentFloor = 1;
    private int totalFloors = 3;

    void Start()
    {
        floors = new GameObject[totalFloors];
        scale = floorPrefab.transform.localScale.z;
        offset = 0;
    }

    void nextFloor() {
        offset += scale;
        var newPosition = new Vector3(0, 0, offset);
        floors[currentFloor] = Instantiate(floorPrefab, newPosition, Quaternion.identity);
        currentFloor = (currentFloor + 1) % totalFloors;
    }

    void clearPrevious() {
        int lastFloor = (currentFloor + (totalFloors)) % totalFloors;
        if (floors[lastFloor] != null) {
            Destroy(floors[lastFloor], 0);
        }
    }

    void Update()
    {
        // Player is facing forwards into positive Z dimension
        float threshold = offset + (scale/2.0f) * 0.4f;
        if (player.position.z > threshold) {
            nextFloor();
            clearPrevious();
        }
    }    
}
