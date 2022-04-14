    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;


    public class BackgroundShift : MonoBehaviour
    {

        [SerializeField]
        private Material background;

        private Color32 color1 = new Color32(215, 189, 255, 255);
        private Color32 color2 = new Color32(140, 84, 245, 255);

        private Color32 altColor1;
        private Color32 altColor2;

        private Color32 initColor1;
        private Color32 initColor2;

        private Color32 lerpedColor1;
        private Color32 lerpedColor2;

        public float lerpTime;

    // Start is called before the first frame update
    void Start()
    {

        background.SetColor("_Color1", color1);
        background.SetColor("_Color2", color2);

        lerpedColor1 = color1;
        lerpedColor2 = color2;
        initColor1 = color1;
        initColor2 = color2;

        BreathingEvents.current.onInhale += ShiftPink;
        BreathingEvents.current.onExhale += ShiftBlue;

    }

        // Update is called once per frame
    void Update()
    {
        background.SetColor("_Color1", lerpedColor1);
        background.SetColor("_Color2", lerpedColor2);
    }


    private void ShiftPink()
    { 
       altColor1 = new Color32(230, 189, 255, 255);
       altColor2 = new Color32(180, 117, 245, 255);
       StartCoroutine(LerpColor());
    }

    private void ShiftBlue()
    {
       altColor1 = new Color32(192, 189, 255, 255);
       altColor2 = new Color32(80, 117, 245, 255);
       StartCoroutine(LerpColor());
    }

    private IEnumerator LerpColor()
    {
        var t = 0f;

        while (t < lerpTime)
        {
            t += Time.deltaTime;
            lerpedColor1 = Color32.Lerp(initColor1, altColor1, t / lerpTime);
            lerpedColor2 = Color32.Lerp(initColor2, altColor2, t / lerpTime);
            yield return null;
        }
        initColor1 = altColor1;
        initColor2 = altColor2;
    }


    //private IEnumerator RampUp(string param, float startValue, float endValue, float fadeTime)
    //{

    //    while (startValue < endValue)
    //    {
    //        startValue += Time.deltaTime / fadeTime;
    //        background.SetFloat(param,startValue);
    //        yield return null;
    //    }
    //}

    //private IEnumerator RampDown(string param, float startValue, float endValue, float fadeTime)
    //    {
    //        while (startValue > endValue)
    //        {
    //            startValue -= startValue * Time.deltaTime / fadeTime;
    //            background.SetFloat(param, startValue);
    //            yield return null;
    //        }
    //    }

    }
