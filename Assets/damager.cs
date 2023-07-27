using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class damager : MonoBehaviour
{
    public int dmgamt;
    public XRGrabInteractable grabref;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  /*  private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.GetComponent<Enemy>())
        {
            //if(grabref.isSelected)
            gameObject.GetComponent<Enemy>().enemydamage(dmgamt);//get health systrm of eav=ch limb anfd send dmg call
        }
    }*/
}
