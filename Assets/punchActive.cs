using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class punchActive : MonoBehaviour
{
    public GameObject PunchObj;
    [SerializeField] private InputActionReference GripRef;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GripRef.action.IsPressed() == true)
        {
            PunchObj.SetActive(true);
        }
        else
        {
            PunchObj.SetActive(false);
        }
    }
}
