using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class gunScript : MonoBehaviour
{
    XRGrabInteractable m_InteractableBase;
    Animator m_Animator;

    public GameObject bullet;
    public Transform shootPoint;

    public float bulletSpeed;

    [SerializeField] ParticleSystem m_GunParticleSystem = null;

    const string k_AnimTriggerDown = "TriggerDown";
    const string k_AnimTriggerUp = "TriggerUp";
    const float k_HeldThreshold = 0.1f;

    float m_TriggerHeldTime;
    bool m_TriggerDown;

    protected void Start()
    {
        m_InteractableBase = GetComponent<XRGrabInteractable>();
        m_Animator = GetComponent<Animator>();
        m_InteractableBase.selectExited.AddListener(DroppedGun);
        m_InteractableBase.activated.AddListener(TriggerPulled);
        m_InteractableBase.deactivated.AddListener(TriggerReleased);
    }

    protected void Update()
    {
        if (m_TriggerDown)
        {
            m_TriggerHeldTime += Time.deltaTime;

            if (m_TriggerHeldTime >= k_HeldThreshold)
            {
                if (!m_GunParticleSystem.isPlaying)
                {
                    m_GunParticleSystem.Play();
                }
            }
        }
    }

    void TriggerReleased(DeactivateEventArgs args)
    {
        m_Animator.SetTrigger(k_AnimTriggerUp);
        m_TriggerDown = false;
        m_TriggerHeldTime = 0f;
        m_GunParticleSystem.Stop();
    }

    void TriggerPulled(ActivateEventArgs args)
    {
        GameObject nubullet = Instantiate(bullet, transform.position, transform.rotation);
        nubullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        m_Animator.SetTrigger(k_AnimTriggerDown);
        m_TriggerDown = true;
    }

    void DroppedGun(SelectExitEventArgs args)
    {
        // In case the gun is dropped while in use.
        m_Animator.SetTrigger(k_AnimTriggerUp);

        m_TriggerDown = false;
        m_TriggerHeldTime = 0f;
        m_GunParticleSystem.Stop();
    }

    public void ShootEvent()
    {
        m_GunParticleSystem.Emit(1);
        
    }

    /*alernative implementation
     public static event Action GunFired;
    public void Fire()
    {
        GetComponent<AudioSource>().Play();
        GameObject spawnedBullet = Instantiate(bulletObj, frontOfGun.position, frontOfGun.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * frontOfGun.forward;
        Destroy(spawnedBullet, 5f);
        GunFired?.Invoke();
    }*/
}
