using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (AudioSource))]
    public class WheelEffects : MonoBehaviour
    {
        public Transform SkidTrailPrefab;
        public static Transform skidTrailsDetachedParent;
        public ParticleSystem skidParticles;
        public bool skidding { get; private set; }
        public bool PlayingAudio { get; private set; }


        private AudioSource m_AudioSource;
        private Transform m_SkidTrail;
        private WheelCollider m_WheelCollider;


        private void Start()
        {

            m_WheelCollider = GetComponent<WheelCollider>();
            m_AudioSource = GetComponent<AudioSource>();
            PlayingAudio = false;

        }

        public void EmitTyreSmoke()
        {
        }

        void FixedUpdate()
        {
            WheelHit hit;
            if (m_WheelCollider.GetGroundHit(out hit))
            {
                if(hit.collider.gameObject.tag == "Bump")
                {
                    if(GameObject.Find("Car").GetComponent<CarController>().CurrentSpeed > 15f)
                    {
                        Debug.Log("Speed AWi");
                        GameObject.Find("Car").GetComponent<CarRemoteControl>().crash = true;
                    }
                    else
                    {
                        Debug.Log("Speed Wati");
                        GameObject.Find("Car").GetComponent<CarRemoteControl>().crash = false;
                    }
                }
            }
        }



    public void PlayAudio()
        {
            m_AudioSource.Play();
            PlayingAudio = true;
        }


        public void StopAudio()
        {
            m_AudioSource.Stop();
            PlayingAudio = false;
        }


        public IEnumerator StartSkidTrail()
        {
            skidding = true;
            m_SkidTrail = Instantiate(SkidTrailPrefab);
            while (m_SkidTrail == null)
            {
                yield return null;
            }
            m_SkidTrail.parent = transform;
            m_SkidTrail.localPosition = -Vector3.up*m_WheelCollider.radius;
        }


        public void EndSkidTrail()
        {
            if (!skidding)
            {
                return;
            }
            skidding = false;
            m_SkidTrail.parent = skidTrailsDetachedParent;
            Destroy(m_SkidTrail.gameObject, 10);
        }
    }
}
