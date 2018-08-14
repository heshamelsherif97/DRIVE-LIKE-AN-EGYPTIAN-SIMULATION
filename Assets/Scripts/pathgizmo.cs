using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathgizmo : MonoBehaviour {

    private List<Transform> listt = new List<Transform>();
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Transform[] array = GetComponentsInChildren<Transform>();
        listt = new List<Transform>();

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != transform)
            {
                listt.Add(array[i]);
            }
        }

        for (int i = 1; i < listt.Count; i++)
        {
            Vector3 current = listt[i].position;
            Vector3 previous = Vector3.zero;
            previous = listt[i - 1].position;
            Gizmos.DrawLine(previous, current);
            Gizmos.DrawSphere(current, 0.3f);
        }
    }
}
