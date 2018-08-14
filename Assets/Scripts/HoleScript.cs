using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{

    Transform hole;

    // Use this for initialization
    void Start()
    {
        hole = gameObject.transform.Find("hole");
        hole.Translate(Random.Range(-4.0f, 4.0f), 0.0f, Random.Range(-29.0f, 29.0f));
    }

    // Update is called once per frame
    void Update()
    {

    }
}