using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    public class CarRecord : MonoBehaviour
    {



        [Header("Sensors")]
        private float sensorLength = 30f;
        private Vector3 frontSensorPos = new Vector3(0f, 0.6f, 1.7f);
        private Vector3 sideSensorPos = new Vector3(0.3f, 0f, 0.3f);
        private float sidewaySensorLength = 30f;

        [Header("State")]
        public string[] state = new string[35];
        public float acc;
        public float steer;
        public float brake;
        public float speed;
        public float rpm;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            sensors();
            setValues();
        }


        void sensors()
        {
            RaycastHit hit;
            Vector3 startPos = transform.position;
            startPos += transform.forward * frontSensorPos.z;
            startPos += transform.up * frontSensorPos.y;

            Vector3 endPos = transform.position;
            endPos -= transform.forward * frontSensorPos.z;
            endPos += transform.up * frontSensorPos.y;

            Vector3 sidePos = transform.position;
            sidePos += transform.up * frontSensorPos.y;

            //front center 
            if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(startPos, hit.point);
                state[0] = hit.distance.ToString();
            }
            else
            {
                state[0] = "30.00";
            }

            //Front right
            startPos += transform.right * sideSensorPos.x;
            if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(startPos, hit.point);
                state[1] = hit.distance.ToString();
            }
            else
            {
                state[1] = "30.00";
            }

            startPos += transform.right * sideSensorPos.x;
            if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(startPos, hit.point);
                state[2] = hit.distance.ToString();
            }
            else
            {
                state[2] = "30.00";
            }

            startPos += transform.right * sideSensorPos.x;
            if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(startPos, hit.point);
                state[3] = hit.distance.ToString();
            }
            else
            {
                state[3] = "30.00";
            }


            //Front Left
            startPos -= transform.right * sideSensorPos.x * 4;
            if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(startPos, hit.point);
                state[4] = hit.distance.ToString();
            }
            else
            {
                state[4] = "30.00";
            }

            startPos -= transform.right * sideSensorPos.x;
            if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(startPos, hit.point);
                state[5] = hit.distance.ToString();
            }
            else
            {
                state[5] = "30.00";
            }

            startPos -= transform.right * sideSensorPos.x;
            if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(startPos, hit.point);
                state[6] = hit.distance.ToString();
            }
            else
            {
                state[6] = "30.00";
            }


            //Back center 
            if (Physics.Raycast(endPos, -transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(endPos, hit.point);
                state[7] = hit.distance.ToString();
            }
            else
            {
                state[7] = "30.00";
            }


            //Back right
            endPos += transform.right * sideSensorPos.x;
            if (Physics.Raycast(endPos, -transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(endPos, hit.point);
                state[8] = hit.distance.ToString();
            }
            else
            {
                state[8] = "30.00";
            }

            endPos += transform.right * sideSensorPos.x;
            if (Physics.Raycast(endPos, -transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(endPos, hit.point);
                state[9] = hit.distance.ToString();
            }
            else
            {
                state[9] = "30.00";
            }

            endPos += transform.right * sideSensorPos.x;
            if (Physics.Raycast(endPos, -transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(endPos, hit.point);
                state[10] = hit.distance.ToString();
            }
            else
            {
                state[10] = "30.00";
            }


            //Back Left
            endPos -= transform.right * sideSensorPos.x * 4;
            if (Physics.Raycast(endPos, -transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(endPos, hit.point);
                state[11] = hit.distance.ToString();
            }
            else
            {
                state[11] = "30.00";
            }

            endPos -= transform.right * sideSensorPos.x;
            if (Physics.Raycast(endPos, -transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(endPos, hit.point);
                state[12] = hit.distance.ToString();
            }
            else
            {
                state[12] = "30.00";
            }

            endPos -= transform.right * sideSensorPos.x;
            if (Physics.Raycast(endPos, -transform.forward, out hit, sensorLength))
            {
                Debug.DrawLine(endPos, hit.point);
                state[13] = hit.distance.ToString();
            }
            else
            {
                state[13] = "30.00";
            }


            //Right SideWay Sensor
            if (Physics.Raycast(sidePos, transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[14] = hit.distance.ToString();
            }
            else
            {
                state[14] = "30.00";
            }

            sidePos += transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[15] = hit.distance.ToString();
            }
            else
            {
                state[15] = "30.00";
            }

            sidePos += transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[16] = hit.distance.ToString();
            }
            else
            {
                state[16] = "30.00";
            }

            sidePos += transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[17] = hit.distance.ToString();
            }
            else
            {
                state[17] = "30.00";
            }

            sidePos -= transform.forward * 0.5f * 4;
            if (Physics.Raycast(sidePos, transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[18] = hit.distance.ToString();
            }
            else
            {
                state[18] = "30.00";
            }

            sidePos -= transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[19] = hit.distance.ToString();
            }
            else
            {
                state[19] = "30.00";
            }

            sidePos -= transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[20] = hit.distance.ToString();
            }
            else
            {
                state[20] = "30.00";
            }

            sidePos -= transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[21] = hit.distance.ToString();
            }
            else
            {
                state[21] = "30.00";
            }

            sidePos = transform.position;
            sidePos += transform.up * frontSensorPos.y;

            //Left SideWay Sensor
            if (Physics.Raycast(sidePos, -transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[22] = hit.distance.ToString();
            }
            else
            {
                state[22] = "30.00";
            }

            sidePos += transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, -transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[23] = hit.distance.ToString();
            }
            else
            {
                state[23] = "30.00";
            }

            sidePos += transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, -transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[24] = hit.distance.ToString();
            }
            else
            {
                state[24] = "30.00";
            }

            sidePos += transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, -transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[25] = hit.distance.ToString();
            }
            else
            {
                state[25] = "30.00";
            }

            sidePos -= transform.forward * 0.5f * 4;
            if (Physics.Raycast(sidePos, -transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[26] = hit.distance.ToString();
            }
            else
            {
                state[26] = "30.00";
            }

            sidePos -= transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, -transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[27] = hit.distance.ToString();
            }
            else
            {
                state[27] = "30.00";
            }

            sidePos -= transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, -transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[28] = hit.distance.ToString();
            }
            else
            {
                state[28] = "30.00";
            }

            sidePos -= transform.forward * 0.5f;
            if (Physics.Raycast(sidePos, -transform.right, out hit, sidewaySensorLength))
            {
                Debug.DrawLine(sidePos, hit.point);
                state[29] = hit.distance.ToString();
            }
            else
            {
                state[29] = "30.00";
            }

        }


        void setValues()
        {
            speed = this.GetComponent<CarController>().CurrentSpeed;
            acc = this.GetComponent<CarController>().AccelInput;
            brake = this.GetComponent<CarController>().BrakeInput;
            steer = this.GetComponent<CarController>().CurrentSteerAngle;
            rpm = this.GetComponent<CarController>().Revs;
            if (speed.ToString().Equals(""))
            {
                state[30] = "0.00";
            }
            else
            {
                state[30] = speed.ToString();
            }
            if (acc.ToString().Equals(""))
            {
                state[31] = "0.00";
            }
            else
            {
                state[31] = acc.ToString();
            }
            if (brake.ToString().Equals(""))
            {
                state[32] = "0.00";
            }
            else
            {
                state[32] = brake.ToString();
            }
            if (steer.ToString().Equals(""))
            {
                state[33] = "0.00";
            }
            else
            {
                state[33] = steer.ToString();
            }
            if (rpm.ToString().Equals(""))
            {
                state[34] = "0.00";
            }
            else
            {
                state[34] = rpm.ToString();
            }
        }
    }
}