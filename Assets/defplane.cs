using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class defplane : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("epunch"))
        {
            other.gameObject.GetComponent<enemypunch>().Blockstun();
        }
    }

    
}
