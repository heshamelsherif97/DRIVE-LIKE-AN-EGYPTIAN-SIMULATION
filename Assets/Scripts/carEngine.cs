using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carEngine : MonoBehaviour
{

    public GameObject path;
    private List<Transform> listt;
    public int current = 0;
    public float maxAngle = 45f;
    public WheelCollider fl;
    public WheelCollider fr;
    public WheelCollider rl;
    public WheelCollider rr;
    public float currentSpeed;
    public float maxSpeed = 220f;
    public float maxTorque = 100f;
    public bool Braking = false;
    public float deacceleration = 3000f;

    [Header("Sensors")]
    public float sensorLength = 9f;
    public Vector3 frontSensorPos = new Vector3(0f, -0.7f, 2.5f);
    public Vector3 sideSensorPos = new Vector3(1f, 0f, 0f);
    public Vector3 sidewaySensorPos = new Vector3(0f, 0.4f, 0f);
    public float angleSensor = 20f;
    private bool avoiding = false;
    public float targetSteer = 0;
    public float turnSpeed = 400f;
    public float sidewaySensorLength = 5f;
    public bool reversing = false;
    public float reverCounter = 0.0f;
    public float waitToReverse = 4f;
    public float reverFor = 2.5f;
    public float respawnWait = 5;
    public float respawnCounter = 0.0f;
    public float death = 10.0f;


    // Use this for initialization
    void Start()
    {
        Transform[] array = path.GetComponentsInChildren<Transform>();
        listt = new List<Transform>();

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != path.transform)
            {
                listt.Add(array[i]);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sensors();
        applySteer();
        drive();
        checkWaypoint();
        isBraking();
        targetSteering();
        DestroyCar();
    }

    void DestroyCar()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < 3f)
        {
            death -= Time.deltaTime;
        }
        if(death <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void sensors()
    {
        RaycastHit hit;
        Vector3 startPos = transform.position;
        startPos += transform.forward * frontSensorPos.z;
        startPos += transform.up * frontSensorPos.y;
        Vector3 sidePos = transform.position;
        float avoidMultiplier = 0;
        avoiding = false;

        //Braking Sensor
        if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
        {
            if (hit.transform.tag != null && hit.transform.tag != "Terrain" || !hit.transform.name.Contains("Chara_4Hero"))
            {
                Debug.DrawLine(startPos, hit.point);
                avoiding = true;
                fl.brakeTorque = deacceleration;
                fr.brakeTorque = deacceleration;
            }
        }
        else
        {
            fl.brakeTorque = 0;
            fr.brakeTorque = 0;
        }

        //Front right
        startPos += transform.right * sideSensorPos.x;
        if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
        {
            if (hit.transform.tag != null && hit.transform.tag != "Terrain" && !hit.transform.name.Contains("Chara_4Hero"))
            {
                Debug.DrawLine(startPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 1f;
            }
        }
        //Front angle right
        else if (Physics.Raycast(startPos, Quaternion.AngleAxis(angleSensor, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.transform.tag != null && hit.transform.tag != "Terrain" && !hit.transform.name.Contains("Chara_4Hero"))
            {
                Debug.DrawLine(startPos, hit.point);
                avoiding = true;
                avoidMultiplier -= 0.5f;
            }
        }

        //Front Left
        startPos -= transform.right * sideSensorPos.x * 2;
        if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
        {
            if (hit.transform.tag != null && hit.transform.tag != "Terrain" && !hit.transform.name.Contains("Chara_4Hero"))
            {
                Debug.DrawLine(startPos, hit.point);
                avoiding = true;
                avoidMultiplier += 1f;
            }
        }

        //Front angle left
        else if (Physics.Raycast(startPos, Quaternion.AngleAxis(-angleSensor, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (hit.transform.tag != null && hit.transform.tag != "Terrain" && !hit.transform.name.Contains("Chara_4Hero"))
            {
                Debug.DrawLine(startPos, hit.point);
                avoiding = true;
                avoidMultiplier += 0.5f;
            }
        }

        if (Physics.Raycast(transform.position, transform.right, out hit, sidewaySensorLength))
        {
            if (hit.transform.tag != "Terrain" && !hit.transform.name.Contains("Chara_4Hero"))
            {
                avoiding = true;
                avoidMultiplier -= 0.5f;
                Debug.DrawLine(transform.position, hit.point, Color.white);
            }
        }


        //Left SideWay Sensor
        if (Physics.Raycast(transform.position, -transform.right, out hit, sidewaySensorLength))
        {
            if (hit.transform.tag != "Terrain" && !hit.transform.name.Contains("Chara_4Hero"))
            {
                avoiding = true;
                avoidMultiplier += 0.5f;
                Debug.DrawLine(transform.position, hit.point, Color.white);
            }
        }

        startPos = transform.position;
        startPos += transform.forward * frontSensorPos.z;
        startPos += transform.up * frontSensorPos.y;
        //front center sensor
        if (avoidMultiplier == 0)
        {
            if (Physics.Raycast(startPos, transform.forward, out hit, sensorLength))
            {
                if (hit.transform.tag != null && hit.transform.tag != "Terrain" && !hit.transform.name.Contains("Chara_4Hero"))
                {
                    if (hit.normal.x < 0)
                    {
                        avoidMultiplier = -1;
                    }
                    else
                    {
                        avoidMultiplier = 1;
                    }
                    Debug.DrawLine(startPos, hit.point);
                }
            }
        }

        if (GetComponent<Rigidbody>().velocity.magnitude < 0.1 && !reversing)
        {
            reverCounter += Time.deltaTime;
            if (reverCounter >= waitToReverse)
            {
                reverCounter = 0;
                reversing = true;
            }
        }
        else if (!reversing)
        {
            reverCounter = 0;
        }


        if (reversing)
        {
            avoidMultiplier *= -1;
            reverCounter += Time.deltaTime;
            if (reverCounter >= reverFor)
            {
                reverCounter = 0;
                reversing = false;
            }
        }

        if (avoiding)
        {
            targetSteer = maxAngle * avoidMultiplier;
        }

    }

    void isBraking()
    {
        if (Braking)
        {
            fl.brakeTorque = deacceleration;
            fr.brakeTorque = deacceleration;
        }
        else
        {
            fl.brakeTorque = 0;
            fr.brakeTorque = 0;
        }
    }

    void checkWaypoint()
    {
        if (Vector3.Distance(transform.position, listt[current].position) < 8f)
        {
            if (current == listt.Count - 1)
            {
                Destroy(this.gameObject);
            }
            else
            {
                current++;
            }
        }
    }

    void drive()
    {
        currentSpeed = 2 * Mathf.PI * fl.radius * fl.rpm * 60 / 1000;
        if (currentSpeed < maxSpeed && !Braking)
        {
            if (!reversing)
            {
                fl.motorTorque = maxTorque;
                fr.motorTorque = maxTorque;
            }
            else
            {
                fl.motorTorque = -maxTorque;
                fr.motorTorque = -maxTorque;
            }
            fl.brakeTorque = 0;
            fr.brakeTorque = 0;
        }
        else
        {
            fl.motorTorque = 0f;
            fr.motorTorque = 0f;
            fl.brakeTorque = deacceleration;
            fr.brakeTorque = deacceleration;
        }
    }

    public void applySteer()
    {
        if (!avoiding)
        {
            Vector3 relative = transform.InverseTransformPoint(listt[current].position);
            relative /= relative.magnitude;
            float newSteer = (relative.x / relative.magnitude) * maxAngle;
            targetSteer = newSteer;
        }
    }

    public void targetSteering()
    {
        fl.steerAngle = Mathf.Lerp(fl.steerAngle, targetSteer, Time.deltaTime * turnSpeed);
        fr.steerAngle = Mathf.Lerp(fl.steerAngle, targetSteer, Time.deltaTime * turnSpeed);
    }
}