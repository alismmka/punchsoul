using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int pHP;
    public CharacterController charc;
    public float hurtdur;
    public MeshRenderer hurtvigref;
    //public Transform forwardir;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("epunch"))
        {
            other.gameObject.GetComponent<enemypunch>().punchcol.enabled = false;
            PDamage();
        }
    }
    public void PDamage()
    {
        pHP -= 5;
        charc.Move(-transform.forward * 2 * Time.deltaTime);
        StartCoroutine(HurtVig(hurtdur));
    }

    IEnumerator HurtVig(float dur)
    {
        hurtvigref.enabled = true;
        yield return new WaitForSeconds(dur);
        hurtvigref.enabled =false;
    }

}
