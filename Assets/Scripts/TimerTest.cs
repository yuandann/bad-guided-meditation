using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    public float testValue;
    private float initValue;
    private float changedValue;
    private float initTimerSet = 4f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimedCalculation());
        timer = initTimerSet;
    }

    // Update is called once per frame
    void Update()
    {
        var change = testValue - initValue;
        timer -= Time.deltaTime;

        if (changedValue > 0 && timer <= 0)
        {
            timer = initTimerSet;

            if (change > changedValue)
                Debug.Log("1");
            if (change < changedValue)
                Debug.Log("2");
            
        }   
    }

    private IEnumerator TimedCalculation()
    {
        initValue = testValue;

        yield return new WaitForSeconds(4f);

        changedValue = testValue - initValue;

        Debug.Log(changedValue);
    }
}
