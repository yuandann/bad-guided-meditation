using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using FMODUnity;
using UnityEngine.SceneManagement;

public class InflateDeflate : MonoBehaviour
{
    private AlembicStreamPlayer player;
    private Animator animator;

    private float isEndingValue;
    private float isStartedValue;

    [SerializeField]
    private StudioEventEmitter inhaleEmitter, exhaleEmitter, outroEmitter;

    public string[] path;

    [SerializeField]
    private bool inOutToggle = false;

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

        fmodSystem.getParameterByName("isEnding", out float f1, out float out1);
        fmodSystem.getParameterByName("isStarted", out float f2, out float out2);

        isEndingValue = out1;
        isStartedValue = out2;

        if (Input.GetKeyDown(KeyCode.R))
            Reset();
    }

    private void Deflate()
    {
        if (inOutToggle)
        {
            if (isEndingValue == 0 && isStartedValue == 1)
            {
                animator.SetBool("isInhale", false);
                animator.SetBool("isExhale", true);
                if (exhaleEmitter != null)
                    exhaleEmitter.Play();
                inOutToggle = !inOutToggle;
            }
        }
    }

    private void Inflate()
    {
        if (isEndingValue == 0 && isStartedValue == 1)
        {
            inOutToggle = !inOutToggle;
            if(inhaleEmitter != null)
                inhaleEmitter.Play();

            if (animator.GetBool("isStarted") == false)
                animator.SetBool("isStarted", true);
            else
            {
                animator.SetBool("isExhale", false);
                animator.SetBool("isInhale", true);
            }
        }

        else if (isEndingValue == 1)
        {
            animator.SetBool("isEnding", true);
            animator.SetBool("isInhale", false);
            animator.SetBool("isExhale", false);
            animator.SetBool("isStarted", false);
            outroEmitter.Play();
        }
    }

    private void Reset()
    {
      SceneManager.LoadScene(0);
    }

    public void LoadFile(int fileID)
    {
        player.LoadFromFile(path[fileID]);
    }
}
