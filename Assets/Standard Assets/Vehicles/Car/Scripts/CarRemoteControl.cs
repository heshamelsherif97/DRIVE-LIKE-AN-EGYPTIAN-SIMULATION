using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class CarRemoteControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        public float SteeringAngle { get; set; }
        public float Acceleration { get; set; }
        public float Brake { get; set; }
        private Steering s;
        public bool crash;
        public bool finished;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
            s = new Steering();
            s.Start();
        }

        void OnCollisionEnter(Collision col)
        {
            Debug.Log(col.gameObject.name);
            if (col.gameObject.tag == "Finish")
            {
                crash = false;
                finished = true;
            }
            else if (col.gameObject.name.Contains("bump"))
            {
                if (GameObject.Find("Car").GetComponent<Rigidbody>().velocity.magnitude > 10f)
                {
                    Debug.Log("bump");
                    crash = true;
                    finished = false;
                }
                else
                {
                    crash = false;
                    finished = false;
                }
            }
            else
            {
                crash = true;
                finished = false;
            }
        }

        void OnCollisionStay(Collision col)
        {
            OnCollisionEnter(col);
        }


        void OnCollisionExit(Collision col)
        {
                crash = false;
                finished = false;
        }

        private void FixedUpdate()
        {
            if (GameObject.Find("Car").transform.position.y < -2f)
            {
                crash = true;
                finished = false;
            }
            else
            {
                crash = false;
                finished = false;
            }

            // If holding down W or S control the car manually
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
            {
                s.UpdateValues();
                m_Car.Move(s.H, s.V, s.V, 0f);
            } else
            {
				m_Car.Move(SteeringAngle, Acceleration, Brake, 0f);
            }
        }
    }
}
