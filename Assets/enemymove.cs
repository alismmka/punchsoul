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
    public float staggeramt;
    public float maxstaggeramt;
    bool hpvis = false;
    public Image hpimg;
    public Image staggerimg;
    public Image hpbakimg;
    public Animator anim;
    public GameObject rag;
    public int ragforce;
    public bool giant;
    public bool invulnerable = false;
    public float invultime = 0.1f;
    public float ragoffset = 0.1f;
    


    // Start is called before the first frame update
    void Start()
    {
        playertran = FindObjectOfType<Player>().transform;
        maxhp = hp;
        //hpimg.DOFade(0.01f, 0f);
        //hpbakimg.DOFade(0.01f, 0f);
    } 

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(playertran.position.x, this.transform.position.y, playertran.position.z);
        playerdistfloat = Vector3.Distance(transform.position, playertran.position);
        playerdist = (transform.position - playertran.position).normalized;

        if (playerdistfloat < 4f)
        {
            //this.transform.LookAt(targetPostition); //redundat for below chsange for testing
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
                this.transform.LookAt(targetPostition);
                transform.position = Vector3.Lerp(transform.position, targetPostition, Time.deltaTime * enemyspeed);
            }
        }

        else
        {
            anim.SetBool("run", false);
            anim.SetTrigger("idle");
        }

       if(staggeramt>0)
        {
            staggeramt -= Time.fixedDeltaTime;
            staggerimg.fillAmount = Mathf.Clamp(staggeramt / maxstaggeramt, 0, 1f);
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
       // switchhpvis();   
        if (invulnerable == true)
        {
            return;
        }

        else
        {
            if (hp <= 0 && !giant)
            {
                //Vector3 ragpos = new Vector3(transform.position.x, transform.position.y, transform.position.z + ragoffset);
                GetComponent<CapsuleCollider>().enabled = false;
                GameObject ragpref = Instantiate(rag, transform.position, transform.rotation);
               if(anim.GetFloat("hitblend")>0.7f)
                {
                    ragpref.GetComponentInChildren<Rigidbody>().AddForce(transform.right*ragforce, ForceMode.Impulse);
                }
               else if(anim.GetFloat("hitblend") < 0.3f)
                {
                    ragpref.GetComponentInChildren<Rigidbody>().AddForce(-transform.right*ragforce, ForceMode.Impulse);
                }
               else
                {
                    ragpref.GetComponentInChildren<Rigidbody>().AddForce(-transform.forward*ragforce, ForceMode.Impulse);
                }
                Destroy(gameObject);
            }
            else
            {
                hp -= amt;
                hpimg.fillAmount = Mathf.Clamp(hp / maxhp, 0, 1f);
                float duration = 0.75f * (hp / maxhp);
                hpimg.DOFillAmount(hp / maxhp, duration);
            }
        }
    }

    public void switchhpvis()
    {
        if (hpvis == false)
        {
            hpvis = true;
            hpimg.DOFade(0f, 0.75f);
            hpbakimg.DOFade(0f, 0.5f);
            
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
