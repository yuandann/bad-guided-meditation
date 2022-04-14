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

    void Start()
    {
       StartCoroutine(TimedCalculation());
       timer = initTimer;
    }
    
    void LateUpdate()
    {
        yPos = spineShoulderVal.jointPosition.y;
        var yPosDelta = yPos - initYPos;

        timer -= Time.deltaTime;

        if (estChange > 0 && timer <= 0)
        {
            timer = initTimer;

            if (yPosDelta > estChange)
            {
                BreathingEvents.current.Inhale();
            }
            else if (yPosDelta < estChange)
            {
                BreathingEvents.current.Exhale();
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
