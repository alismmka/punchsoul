using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    
    public float hp;
    public GameObject hitfx;
    public GameObject fragfx;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.GetComponent<damager>())
        {
            enemydamage(collision.gameObject.GetComponent<damager>().dmgamt);
        }
    }

    public void enemydamage(int num)
    {// try puttting on collenter that calls this instead and changwe damager only keep dmg amt

        if (hp <= 0)
        {
            Instantiate(fragfx, transform.position, transform.rotation);
            
            Destroy(gameObject);
        }
        else
        {
            Instantiate(hitfx, transform.position, transform.rotation);

            if (anim!=null)
            {
                anim.SetTrigger("hit");
            }

            hp -= num;
        }
        Debug.Log(hp);

    }


}
