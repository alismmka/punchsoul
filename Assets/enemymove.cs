using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class enemymove : MonoBehaviour
{
    public Transform playertran;
    public Vector3 playerdist;
    public float playerdistfloat;
    public float enemyspeed;
    public float atkrange;
    public float hp;
    public float maxhp;
    bool hpvis = false;
    public Image hpimg;
    public Image hpbakimg;
    public Animator anim;
    public GameObject rag;
    public bool giant;
    public bool invulnerable = false;
    public float invultime = 0.1f;
    


    // Start is called before the first frame update
    void Start()
    {
        playertran = FindObjectOfType<Player>().transform;
        maxhp = hp;
        hpimg.DOFade(0.01f, 0f);
        hpbakimg.DOFade(0.01f, 0f);
    } 

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(playertran.position.x, this.transform.position.y, playertran.position.z);
        playerdistfloat = Vector3.Distance(transform.position, playertran.position);
        playerdist = (transform.position - playertran.position).normalized;

        if (playerdistfloat < 4f)
        {
            this.transform.LookAt(targetPostition); //redundat for below chsange for testing
            anim.SetBool("run", true);

            if (playerdistfloat > atkrange)
            {
                //  this.transform.LookAt(targetPostition);
            }



            if (playerdistfloat < atkrange)
            {
                anim.SetBool("run", false);
                anim.SetTrigger("attack");
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("run"))// && anim.GetCurrentAnimatorStateInfo(1).normalizedTime < 1.0f)
            {

                transform.position = Vector3.Lerp(transform.position, targetPostition, Time.deltaTime * enemyspeed);

            }
        }

        else
        {
            anim.SetBool("run", false);
            anim.SetTrigger("idle");
        }

       
        
    }

   

    public void PlayHitAnim(float dir)
    {

        if (invulnerable == true)
        {
            return;
        }
        else
        {
            anim.SetFloat("hitblend", dir);
            anim.SetTrigger("hit");
            //anim.ResetTrigger("hit");
            invulnerable = true;
            StartCoroutine(Invulntimer(invultime));
           
        }
    }

    public void Edamage(int amt)
    {
        switchhpvis();   
        if (invulnerable == true)
        {
            return;
        }

        else
        {
            if (hp <= 0 && !giant)
            {
                //GetComponent<CapsuleCollider>().enabled = false;
                Instantiate(rag, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                hp -= amt;
               // hpimg.fillAmount = Mathf.Clamp(hp / maxhp, 0, 1f);
                float duration = 0.75f * (hp / maxhp);
                hpimg.DOFillAmount(hp / maxhp, duration);
            }
        }
    }

    public void switchhpvis()
    {
        if (hpvis == false)
        {
            hpimg.DOFade(0f, 0.75f);
            hpbakimg.DOFade(0f, 0.5f);
            hpvis = true;
        }
        else
            return;

    }

    IEnumerator Invulntimer(float t)
    {
        invulnerable = true;
        yield return new WaitForSeconds(t);
        invulnerable = false;
    }
}
