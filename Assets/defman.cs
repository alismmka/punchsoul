using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class defman : MonoBehaviour
{
    public InputActionProperty gripanimact;
    public GameObject defp;
    public float gripVal;
    void Update()
    {
        gripVal = gripanimact.action.ReadValue<float>();
        if (gripVal < 0.2f)
        {
            defp.SetActive(true);
        }
        else
        {
            defp.SetActive(false);
        }
    }
}
