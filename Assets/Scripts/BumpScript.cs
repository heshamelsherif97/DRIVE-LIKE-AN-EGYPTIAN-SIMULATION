using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpScript : MonoBehaviour
{

    Transform bump;

    // Use this for initialization
    void Start()
    {
        bump = gameObject.transform.Find("bump");
        bump.Translate(0.0f, 0.0f, Random.Range(-28.0f, 28.0f));
    }
    // Update is called once per frame
    void Update()
    {

    }
}