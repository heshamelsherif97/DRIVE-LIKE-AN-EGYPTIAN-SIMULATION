using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    public class PavementScript : MonoBehaviour
    {

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
                GameObject.Find("Car").GetComponent<CarRemoteControl>().crash= true;
        }

        void OnTriggerStay(Collider other)
        {
            OnTriggerEnter(other);
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
                GameObject.Find("Car").GetComponent<CarRemoteControl>().crash = false;
        }
    }

}