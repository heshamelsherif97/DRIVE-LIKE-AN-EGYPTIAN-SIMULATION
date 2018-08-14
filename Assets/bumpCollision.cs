using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Vehicles.Car
{
    public class bumpCollision : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnCollisionEnter(Collision col)
        {
            Debug.Log(col.gameObject.name);
            if (col.gameObject.name.Equals("Car"))
            {
                if (GameObject.Find("Car").GetComponent<Rigidbody>().velocity.magnitude > 10f)
                {
                    Debug.Log("Speed+");
                }
                else
                {
                    Debug.Log("Speed-");
                }
            }
        }

        void OnCollisionStay(Collision other)
        {
            OnCollisionEnter(other);
        }

        void OnCollisionExit(Collision col)
        {
            if (col.gameObject.tag == "Player")
            {
                if (GameObject.Find("Car").GetComponent<Rigidbody>().velocity.magnitude > 10f)
                {
                    Debug.Log("Exit+");
                }
                else
                {
                    Debug.Log("Exit-");
                }
            }
        }
    }

}
