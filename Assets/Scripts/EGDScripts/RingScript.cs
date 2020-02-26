using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingScript : TriggeringObject
{
    public string rotatingObjectTag1;
    public string rotatingObjectTag2;

    public GameObject[] rings;

    public float[] currentAngle;

    public float tolerance;

    public float transitionTimeForTolerance;

    GameObject rotatingObject1;
    GameObject rotatingObject2;

    int ringNum;

    float startTimer;

    float currentDefaultAngle;

    bool setDefaultAngle;

    // Start is called before the first frame update
    void Start()
    {
        rotatingObject1 = GameObject.FindGameObjectWithTag(rotatingObjectTag1);
        rotatingObject2 = GameObject.FindGameObjectWithTag(rotatingObjectTag2);
        ringNum = 0;
        startTimer = 0f;
        setDefaultAngle = false;
        if (rotatingObject1 != null)
        {
            float addingAngle = rotatingObject1.transform.localEulerAngles.y;
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

        if (rotatingObject1 == null)
        {
            Start();
        }
        else if (!IsTriggered())
        {
            if (!setDefaultAngle)
            {
                currentDefaultAngle = currentAngle[ringNum] - rotatingObject1.transform.localEulerAngles.y;
                setDefaultAngle = true;
            }
            //Debug.Log(rotatingObject.transform.localEulerAngles.y);
            float addingAngle = rotatingObject1.transform.localEulerAngles.y;
            if (addingAngle < 0f)
            {
                addingAngle += 360f;
            }

            float angleCheck;
            if(ringNum % 2 == 0)
            {
                angleCheck = currentDefaultAngle + rotatingObject1.transform.localEulerAngles.y;
            }
            else
            {
                angleCheck = currentDefaultAngle + rotatingObject2.transform.localEulerAngles.y;
            }
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
                        if(ringNum % 2 == 0)
                        {
                            currentDefaultAngle = currentAngle[ringNum] - rotatingObject1.transform.localEulerAngles.y;
                        }
                        else
                        {
                            currentDefaultAngle = currentAngle[ringNum] - rotatingObject2.transform.localEulerAngles.y;
                        }
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
