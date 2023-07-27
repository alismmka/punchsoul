using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autokill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timedkill());
    }

    IEnumerator Timedkill()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
