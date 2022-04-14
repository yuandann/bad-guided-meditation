using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using FMODUnity;

public class InflateDeflate : MonoBehaviour
{
    private AlembicStreamPlayer player;
    private Animator animator;

    [SerializeField]
    private StudioEventEmitter inhaleEmitter, exhaleEmitter, outroEmitter;

    public string[] path;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AlembicStreamPlayer>();
        animator = GetComponent<Animator>();

        BreathingEvents.current.onInhale += Inflate;
        BreathingEvents.current.onExhale += Deflate;
    }

    void Update()
    {
        var fmodSystem = RuntimeManager.StudioSystem;
        fmodSystem.getParameterByName("isEnding", out float f1, out float isEndingValue);
        fmodSystem.getParameterByName("isStarted", out float f2, out float isStartedValue);

        if (isEndingValue == 0 && isStartedValue == 1)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (animator.GetBool("isStarted") == false)
                {
                    animator.SetBool("isStarted", true);
                }

                BreathingEvents.current.Inhale();
            }

            else if (Input.GetKeyUp(KeyCode.Space))
                //Deflate();
                BreathingEvents.current.Exhale();
        }

        else if (isEndingValue == 1)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("isEnding", true);
                animator.SetBool("isInhale", false);
                animator.SetBool("isExhale", false);
                animator.SetBool("isStarted", false);
                outroEmitter.Play();
            }
                
        }

        if (Input.GetKeyDown(KeyCode.R))
            Reset();
    }

    private void Deflate()
    {
        //player.LoadFromFile(path[0]);
        animator.SetBool("isInhale", false);
        animator.SetBool("isExhale", true);
        exhaleEmitter.Play();
    }

    private void Inflate()
    {
        //player.LoadFromFile(path[1]);
        animator.SetBool("isExhale", false);
        animator.SetBool("isInhale", true);
        inhaleEmitter.Play();
    }

    private void Reset()
    {
        animator.SetBool("isStarting", false);
        animator.SetBool("isEnd", false);
        animator.SetBool("isReset", true);
        BreathingEvents.current.onInhale += Inflate;
        BreathingEvents.current.onExhale += Deflate;
    }

    public void LoadFile(int fileID)
    {
        player.LoadFromFile(path[fileID]);
    }
}
