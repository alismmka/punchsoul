using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WhipTip : MonoBehaviour
{
    [SerializeField] private InputActionReference pullActionReference;
    [SerializeField] private InputActionReference shootActionReference;
    [SerializeField] private InputActionReference jumpActionReference;


    [SerializeField] private float pullSpeed;
    [SerializeField] private float shootSpeed;

    [SerializeField] private Transform gripref;
    [SerializeField] private GameObject tipRef;
    [SerializeField] private Transform targetTran;
    [SerializeField] private Transform grappletargettran;

    [SerializeField] private Rigidbody tipRB;
    [SerializeField] private CharacterController charC;

    [SerializeField] private bool grappled;
    [SerializeField] private bool sword;
    [SerializeField] private Transform swordtran;
    [SerializeField] private GameObject swordobj;
    [SerializeField] private GameObject flameobj;







    // Start is called before the first frame update
    void Start()
    {
        shootActionReference.action.performed += OnWhipShoot;

    }

    // Update is called once per frame
    void Update()
    {
        if(grappled)
        {
            tipRef.transform.position = grappletargettran.position;

            Vector3 targetmov = (grappletargettran.position - charC.transform.position);
            targetmov = targetmov.normalized * pullSpeed;
            charC.Move(targetmov*Time.deltaTime);
        }

        if (sword)
        {
            tipRef.transform.SetPositionAndRotation(swordtran.position, swordtran.rotation);
            tipRB.isKinematic = true;
        }

        if (!sword)
        {
            swordobj.SetActive(false);
            tipRB.isKinematic = false;
        }

        if (pullActionReference.action.triggered == true) //used to be action.IsPressed()
        {
            

            if(sword)
            {
                tipRB.isKinematic = false;
                swordobj.SetActive(false);
                sword = false;
            }
            else
            {
                swordobj.SetActive(true);
                tipRB.isKinematic = true;
                sword = true;
            }


            /*if (tipRB.isKinematic == true)
            {
                 failed grapple logic
                // transform.position = Vector3.Lerp(transform.position, tipRef.transform.position, Time.deltaTime * pullSpeed * 2);
                Vector3 dir = (tipRef.transform.position - charC.transform.position).normalized;
                charC.Move(dir * pullSpeed * 2); 
            }
            else
                tipRef.transform.position = Vector3.Lerp(tipRef.transform.position, targetTran.position, Time.deltaTime * pullSpeed); */
        }

        if (jumpActionReference.action.IsPressed() == true)
        {
            grappled = false;
        }
    }

    private void OnWhipShoot(InputAction.CallbackContext obj)
    {
        grappled = false;

        if (sword == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(gripref.position, - gripref.up, out hit))
            {
                if(hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 11)
                {

                    grappletargettran = hit.collider.transform;
                    grappled = true;
                }
            }
        }

        if (sword == true)
        {
            if (flameobj.activeSelf == false)
            {
                flameobj.SetActive(true);
            }
            else if (flameobj.activeSelf == true)
            {
                flameobj.SetActive(false);
            }
            // tipRef.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, Camera.main.transform.position.z) * shootSpeed, ForceMode.Impulse);
        }
    }

    
}
