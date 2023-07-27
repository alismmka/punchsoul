using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistolshoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    public Animator gunAnimator;
    public Transform barrelLocation;
    public Transform casingExitLocation;

    float destroyTimer = 2f;
    float shotPower = 500f;
    float ejectPower = 150f;

    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioClip emptySound;
    public Magazine mag;
    public XRBaseInteractor socketInteractor;

    public bool slides = true;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        socketInteractor.onSelectEntered.AddListener(AddMag);
        socketInteractor.onSelectEntered.AddListener(RemoveMag);
    }

    public void PullTheTrigger()
    {
        gunAnimator.SetTrigger("Fire");

        if (mag && mag.bulletCount > 0 )//&& slides)
        {
            gunAnimator.SetTrigger("Fire");
        }

        else
        {
            source.PlayOneShot(emptySound);
        }
    }

    void Shoot()
    {
        //mag.bulletCount--;
        //Debug.Log("shoot");
        source.PlayOneShot(fireSound);

        if(muzzleFlashPrefab)
        {
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
            Destroy(tempFlash, destroyTimer);
        }

        if(!bulletPrefab)
        {
            return;
        }

        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.up*shotPower);
    }

    void CasingRelease()
    {
        if(!casingExitLocation || !casingPrefab)
        {
            return;
        }

        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        Rigidbody tempRb = tempCasing.GetComponent<Rigidbody>();
        tempRb.AddExplosionForce(Random.Range(ejectPower*0.7f, ejectPower), casingExitLocation.position - casingExitLocation.right,0.1f);
        tempRb.AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
        Destroy(tempCasing, destroyTimer);
    }

    public void AddMag(XRBaseInteractable interactable)
    {
        mag = interactable.transform.gameObject.GetComponent<Magazine>();
        source.PlayOneShot(reloadSound);
        slides = false;
    }
    public void RemoveMag(XRBaseInteractable interactable)
    {
        mag = null;
        source.PlayOneShot(reloadSound);
    }
    public void Slide()
    {
        source.PlayOneShot(reloadSound);
        slides = true;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
