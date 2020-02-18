using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : TriggeringObject
{
    public string rotatingObjectTag;

    public GameObject[] rings;

    public float[] currentAngle;

    public float tolerance;

    public float transitionTimeForTolerance;

    GameObject rotatingObject;

    int ringNum;

    float startTimer;

    float currentDefaultAngle;

    // Start is called before the first frame update
    void Start()
    {
        rotatingObject = GameObject.FindGameObjectWithTag(rotatingObjectTag);
        ringNum = 0;
        startTimer = 0f;
        currentDefaultAngle = rotatingObject.transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsTriggered())
        {
            float angleCheck = rotatingObject.transform.localEulerAngles.y + currentDefaultAngle;
            currentAngle[ringNum] = angleCheck;
            if (AngleWithinMargin(angleCheck))
            {
                if(startTimer < 0.00001f)
                {
                    startTimer = Time.time;
                }
                else if(Time.time - startTimer >= transitionTimeForTolerance)
                {
                    startTimer = 0f;
                    ++ringNum;
                    currentDefaultAngle = rotatingObject.transform.localEulerAngles.y;
                }
            }
        }
    }

    bool AngleWithinMargin(float angle)
    {
        while(angle > 180f)
        {
            angle -= 360f;
        }
        while(angle <= -180f)
        {
            angle += 360f;
        }
        return Mathf.Abs(angle) < tolerance;
    }

    public override bool IsTriggered()
    {
        return ringNum >= rings.Length;
    }
}
