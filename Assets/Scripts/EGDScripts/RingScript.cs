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

    bool setDefaultAngle;

    // Start is called before the first frame update
    void Start()
    {
        rotatingObject = GameObject.FindGameObjectWithTag(rotatingObjectTag);
        ringNum = 0;
        startTimer = 0f;
        setDefaultAngle = false;
        if (rotatingObject != null)
        {
            float addingAngle = rotatingObject.transform.localEulerAngles.y;
            if(addingAngle < 0f)
            {
                addingAngle += 360f;
            }
            currentDefaultAngle = addingAngle + currentAngle[ringNum];
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (rotatingObject == null)
        {
            Start();
        }
        else if (!IsTriggered())
        {
            if (!setDefaultAngle)
            {
                currentDefaultAngle = currentAngle[ringNum] - rotatingObject.transform.localEulerAngles.y;
                setDefaultAngle = true;
            }
            //Debug.Log(rotatingObject.transform.localEulerAngles.y);
            float addingAngle = rotatingObject.transform.localEulerAngles.y;
            if (addingAngle < 0f)
            {
                addingAngle += 360f;
            }
            float angleCheck = currentDefaultAngle + rotatingObject.transform.localEulerAngles.y;
            currentAngle[ringNum] = angleCheck;
            if (AngleWithinMargin(angleCheck))
            {
                if (startTimer < 0.00001f)
                {
                    startTimer = Time.time;
                }
                else if (Time.time - startTimer >= transitionTimeForTolerance)
                {
                    startTimer = 0f;
                    ++ringNum;
                    if (ringNum < rings.Length)
                    {
                        currentDefaultAngle = currentAngle[ringNum] - rotatingObject.transform.localEulerAngles.y;
                    }

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
