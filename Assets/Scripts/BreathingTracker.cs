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
    [SerializeField]
    private float initTimer;
    private float timer;

    [SerializeField]
    private bool canTriggerInhale = true;
    [SerializeField]
    private bool canTriggerExhale = false;

    void Start()
    {
       StartCoroutine(TimedCalculation());
       timer = initTimer;
    }
    
    void Update()
    {
        yPos = spineShoulderVal.jointPosition.y;
        var yPosDelta = yPos - initYPos;

        timer -= Time.deltaTime;

        if (estChange > 0 && timer <= 0)
        {
            timer = initTimer;

            if (yPosDelta > estChange && canTriggerInhale)
            {
                BreathingEvents.current.Inhale();
                canTriggerInhale = false;
                canTriggerExhale = true;
            }
            else if (yPosDelta < estChange && canTriggerExhale)
            {
                BreathingEvents.current.Exhale();
                canTriggerExhale = false;
                canTriggerInhale = true;
            }
        }

    }

    private IEnumerator TimedCalculation()
    {
        var initJointPos = spineShoulderVal.jointPosition;
        initYPos = initJointPos.y;
        yield return new WaitForSeconds(4f);
        estChange = yPos - initYPos;
    }
}
