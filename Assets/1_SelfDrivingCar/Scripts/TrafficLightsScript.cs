using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    public class TrafficLightsScript : MonoBehaviour
    {

        public int activeLight;
        float remainingTime;
        Light greenLightComp;
        Light redLightComp;

        private float sensorLength = 11f;
        private Vector3 frontSensorPos = new Vector3(0f, 0f, 0.5f);
        public int type;

        // Use this for initialization
        void Start()
        {
            greenLightComp = this.transform.GetChild(1).GetComponent<Light>();
            redLightComp = this.transform.GetChild(0).GetComponent<Light>();
            remainingTime = 8f;
        }

        void setLights()
        {
            if (activeLight == 0)
            {
                redLightComp.intensity = 10;
                greenLightComp.intensity = 0;
            }
            else
            {
                redLightComp.intensity = 0;
                greenLightComp.intensity = 10;
            }
        }

        void switchLights()
        {
            if (activeLight == 0)
            {
                activeLight = 1;
            }
            else
            {
                activeLight = 0;
            }
        }

        // Update is called once per frame
        void Update()
        {
            setLights();
            if (remainingTime <= 0)
            {
                switchLights();
                remainingTime = 5f;
            }
            else
            {
                remainingTime -= Time.deltaTime;
            }
        }
    }
}