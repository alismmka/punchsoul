using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class defman : MonoBehaviour
{
    public InputActionProperty gripanimact;
    public InputActionProperty triganimact;
    public GameObject defp;
    defplane defpref;
    public float gripVal;
    public float trigVal;
    WaitForSeconds waitperiod;
    public GameObject defbreakfx;
    public GameObject defbfxpos;
    public bool broken;



    private void Start()
    {
        defpref = defp.GetComponent<defplane>();
        waitperiod = new WaitForSeconds(3f);
    }

    void Update()
    {
        gripVal = gripanimact.action.ReadValue<float>();
        trigVal = triganimact.action.ReadValue<float>();
        if (trigVal>0.5f && gripVal < 0.2f && !broken)
        {
            defp.SetActive(true);
        }
        else //if(trigVal < 0.5f && gripVal > 0.2f)
        {
            defp.SetActive(false);
        }
    }

    public void startdeftimer(GameObject defplref)
    {
        StartCoroutine(DefBreakTimer(defplref));
    }

    public IEnumerator DefBreakTimer(GameObject defpl)
    {
        Debug.Log("brokestart");
        Instantiate(defbreakfx, defpl.transform.position, defpl.transform.rotation, transform);
        yield return new WaitForSeconds(3f);
        Debug.Log("brokebak");
        broken = false;
        defpl.SetActive(true);

    }
}
