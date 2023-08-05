using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class defplane : MonoBehaviour
{
    public bool broken = false;
    public GameObject defbreakfx;
    public defman defmanref;

    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("epunch"))
        {
            other.gameObject.GetComponent<enemypunch>().Blockstun();
            defmanref.broken = true;
            defmanref.startdeftimer(gameObject);
            gameObject.SetActive(false);
        }
    }



}
