using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemypunch : MonoBehaviour
{
    public CapsuleCollider punchcol;
    public Animator eanim;
    public bool canatk = true;
    public float atktimer;
    // Start is called before the first frame update
    void Start()
    {
        //punchcol = GetComponent<CapsuleCollider>();
        //eanim = GetComponentInParent<Animator>();
        //punchcol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {/*
        if (eanim.GetCurrentAnimatorStateInfo(0).IsName("attack") && canatk)
        {
            StartCoroutine(Epunchactiv());
            canatk = false;
        }

        if(canatk == false)
        {
            atktimer -= Time.fixedDeltaTime;
        }

        if(atktimer<=0)
        {
            canatk = true;
            atktimer = 0.3f;
        }*/
    }

    /*IEnumerator Epunchactiv()
    {
        yield return new WaitForSeconds(0.35f);
        if(punchcol!= null)
        {
            punchcol.enabled = true;
            yield return new WaitForSeconds(0.2f);
            punchcol.enabled = false;

        }
    }*/

    public void Blockstun()
    {
        eanim.SetTrigger("hit");
        punchcol.enabled = false;

    }


}
