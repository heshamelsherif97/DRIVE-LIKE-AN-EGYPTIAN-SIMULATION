using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Vehicles.Car
{
    public class walkTo : MonoBehaviour
    {
        public Transform goal;
        public Animator anim;
        public Vector3 player;
        float remainingTime;

        // Use this for initialization
        void Start()
        {
            remainingTime = 8f;
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            agent.destination = goal.position;
            anim = GetComponent<Animator>();
            agent.speed = Random.Range(3, 5);
        }

        void Update()
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            Vector3 distance = this.transform.position - goal.transform.position;
            if (distance.magnitude < 1 || remainingTime <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                anim.SetBool("IsRun", true);
            }
            remainingTime -= Time.deltaTime;
        }
    }
}