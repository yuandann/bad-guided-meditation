using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BreathingParameterTrigger : MonoBehaviour
{
    [SerializeField]
    private StudioEventEmitter inhaleEmitter, exhaleEmitter, outroEmitter;

    void Start()
    {
        BreathingEvents.current.onInhale += SetParamInhale;
        BreathingEvents.current.onExhale += SetParamExhale;
    }

    private void SetParamInhale()
    {
        inhaleEmitter.Play();
        exhaleEmitter.Stop();
    }

    private void SetParamExhale()
    {
        exhaleEmitter.Play();
        inhaleEmitter.Stop();
    }
}
