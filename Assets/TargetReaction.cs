using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetReaction : MonoBehaviour
{
    public float threshold = 0.02f;
    public Transform target;
    public UnityEvent OnReached;
    bool reached = false;

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.position);

        if(distance<threshold && !reached)
        {
            OnReached.Invoke();
            reached = true;
        }

        else if(distance>=threshold)
        {
            reached = false;
        }
    }
}
