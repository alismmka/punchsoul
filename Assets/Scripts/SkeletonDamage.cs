using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDamage : MonoBehaviour
{
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //if(!anim)
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WhipTip>())
        {
            anim.SetTrigger("Hurt");
        }

        if (other.gameObject.GetComponent<Gauntlet>())
        {
            anim.SetTrigger("Frag");
        }
    }


}
