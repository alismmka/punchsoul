using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class handstopper : MonoBehaviour
{

    public bool r;
    public IKTargetFollowVRRig ikref;
    public defman defmanref;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            if(r && defmanref.gripVal > 0.2f)
            {
                ikref.istrackingrhand = false;
            }
            else if (defmanref.gripVal < 0.2f)
            {
                ikref.istrackinglhand = false;

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            if (r && defmanref.gripVal < 0.2f)
            {
                ikref.istrackingrhand = true;
            }
            else if (defmanref.gripVal > 0.2f)
            {
                ikref.istrackinglhand = true;

            }
        }
    }
}
