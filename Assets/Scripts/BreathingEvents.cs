using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreathingEvents : MonoBehaviour
{
    public static BreathingEvents current = null;

    void Awake()
    {
        current = this;
    }


    public event Action onInhale;
    public void Inhale()
    {
        if(onInhale != null)
        {
            onInhale();
        }
    }

    public event Action onExhale;
    public void Exhale()
    {
        if(onExhale != null)
        {
            onExhale();
        }
    }
}
