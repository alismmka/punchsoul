using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class PunchVelocity : MonoBehaviour
{
    public TMP_Text velTxt;
    public VelocityContainer velref;

    public GameObject lofx;
    public GameObject lolofx;
    public GameObject hifx;
    public GameObject bloodfx;
    public Transform fxpos;
    public int lodmg = 1;
    public int hidmg = 3;

    public float tempVel;
    public ActionBasedContinuousMoveProvider moveprovref;
   // public InputActionProperty movevel;
    Rigidbody rb;
    //public Vector2 movevelvecref;
    //public ActionBasedController leftHand;
    //public InputHelpers.Axis2D button;
   // public UnityEngine.XR.InputDevice targetDevice;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

      /*  List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();
        InputDeviceCharacteristics leftcontchar = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftcontchar, devices);
        if(devices.Count>0)
        {
            targetDevice = devices[0];
        }*/
    }

    // Update is called once per frame
    void Update()
    {

        //Vector2 newvel;

        //movevelvecref = newvel;

        
       /* if(targetDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 prim2daxval)&& prim2daxval != Vector2.zero)
        {
            Debug.Log(prim2daxval);
        } */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 11)
        {
            tempVel = velref.veln;
            //velTxt.text = tempVel.ToString();

            /*
            if (movevel.action.ReadValue<Vector2>().y < 0.6f)
            {
                tempVel -= 2f;
            }
            */
            if (collision.gameObject.GetComponent<enemymove>())
            {
                enemymove emoveref = collision.gameObject.GetComponent<enemymove>();

                if (emoveref.invulnerable == false)
                {

                    if (tempVel > 1.5f)
                    {
                        emoveref.Edamage(lodmg);
                        Instantiate(lofx, collision.GetContact(0).point, Quaternion.identity);
                        Instantiate(bloodfx, collision.GetContact(0).point, transform.rotation, null);
                        emoveref.staggeramt += 3f;
                        if (emoveref.staggeramt > 5.5f || emoveref.hp < lodmg)
                        {
                            emoveref.PlayHitAnim(FromRight(collision.GetContact(0).point));
                            emoveref.staggeramt = 0f;
                        }

                    }

                    else if (tempVel > 3f)
                    {
                        emoveref.Edamage(hidmg);
                        Instantiate(hifx, collision.GetContact(0).point, Quaternion.identity); // or set tran to collision.GetContact(0).point
                        Instantiate(bloodfx, collision.GetContact(0).point, transform.rotation, null);
                        emoveref.staggeramt += 6f;
                        if (emoveref.staggeramt > 5.5f || emoveref.hp < hidmg)
                        {
                            emoveref.PlayHitAnim(FromRight(collision.GetContact(0).point));
                            emoveref.staggeramt = 0f;
                        }

                    }

                    else if (tempVel < 1f)
                    {
                        Instantiate(lolofx, collision.GetContact(0).point, Quaternion.identity);
                    }
                }
            }

            else
            {
                Instantiate(lolofx, collision.GetContact(0).point, Quaternion.identity);
            }
        }
    }

   

    /* private void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.layer == 11)
         {
             tempVel = velref.veln;
             velTxt.text = tempVel.ToString();
             if (other.gameObject.GetComponent<enemymove>())
                 other.gameObject.GetComponent<enemymove>().PlayHitAnim();
         }
     }*/

    private float FromRight(Vector3 other)
    {
        Vector3 right = transform.right;
        float dot = Vector3.Dot(right.normalized, other.normalized);

        dot = (dot / 2f) + 0.5f;
        return dot;
    }

    IEnumerator Hitstop(float dur)
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(dur);
        Time.timeScale = 1.0f;
    }

}
