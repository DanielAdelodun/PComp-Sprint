using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstecle : MonoBehaviour
{
    public GameObject[] Obstecle;
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void spawn() {
        for (var i = 0; i < Lane.timeStamps.Count; i++)
        {
            Instantiate(Obstecle[Random.Range(0, Obstecle.Length)], transform.TransformPoint(new Vector3(0, 0, (float)Lane.timeStamps[i])), Quaternion.identity);
        }
    }
}
