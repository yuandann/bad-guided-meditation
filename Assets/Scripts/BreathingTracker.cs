using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class BreathingTracker: MonoBehaviour
{
    [SerializeField]
    private SpineShoulderTrack spineShoulderVal;

    private float yPos;
    private float initYPos;

    [SerializeField]
    private float estChange;

    private bool canTriggerInhale = true;
    private bool canTriggerExhale = false;

    void Start()
    {
       StartCoroutine(TimedCalculation());
    }
    
    void Update()
    {
        yPos = spineShoulderVal.jointPosition.y;
        var yPosDelta = yPos - initYPos;

        if (estChange > 0)
        {
            if (yPosDelta > estChange && canTriggerInhale)
            {
                BreathingEvents.current.Inhale();
                canTriggerInhale = false;
                canTriggerExhale = true;
                Debug.Log(yPosDelta);
            }
            if (yPosDelta < estChange && canTriggerExhale)
            {
                BreathingEvents.current.Exhale();
                canTriggerExhale = false;
                canTriggerInhale = true;
                StartCoroutine(SetInitPos());
            }

            if (yPos < initYPos)
                initYPos = yPos;
        }

    }

    private IEnumerator TimedCalculation()
    {
        initYPos = spineShoulderVal.jointPosition.y;
        yield return new WaitForSeconds(1f);
        estChange = Mathf.Abs(yPos - initYPos);
    }
    private IEnumerator SetInitPos()
    {
        yield return new WaitForSeconds(2f);
        initYPos = spineShoulderVal.jointPosition.y;
        Debug.Log(initYPos);
    }
}
