using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class Glitcher : MonoBehaviour
{
    public AnalogGlitch glitch;
    public float glitchFactor = 10f;

    void Update()
    {

        if (Input.GetMouseButtonUp(0))
            StartCoroutine(DeGlitch());
    }

    public void AddGlitch()
    {

        if(glitch.scanLineJitter < 1 || glitch.colorDrift < 1)
        {
            glitch.scanLineJitter += Time.deltaTime / glitchFactor;
            glitch.colorDrift += Time.deltaTime / glitchFactor;
        }


    }

    public IEnumerator DeGlitch()
    {
       while(glitch.scanLineJitter > 0 || glitch.colorDrift > 0)
        {
            glitch.scanLineJitter -= Time.deltaTime / 5f;
            glitch.colorDrift -= Time.deltaTime / 5f;
            yield return null;
        }
    }
}
