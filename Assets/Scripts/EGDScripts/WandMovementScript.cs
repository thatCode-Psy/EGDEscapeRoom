using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandMovementScript : TriggeringObject
{

    public bool triggered;
    bool started;
    GameObject wandObject;
    
    public int previousFrameCheckCount;
    Vector3[] previousWandPositions;
    int framesOnScreen;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        started = false;
        wandObject = null;
        
        previousWandPositions = new Vector3[previousFrameCheckCount];
        framesOnScreen = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("EGDController") != null && !started && activated)
        {
            started = true;
            wandObject = GameObject.FindGameObjectWithTag("Wand");
            
           
        }
        if (started)
        {
            AddNewPoint(wandObject.transform.position);
            if (wandObject.transform.position != Vector3.zero && !triggered && wandObject.transform.position.x > -1.47f && MovedXTimes())
            {
                
                triggered = true;
                wandObject.GetComponent<WandScript>().SpawnBall();
            }
        }
        ++framesOnScreen;
        if(framesOnScreen > previousFrameCheckCount)
        {
            framesOnScreen = previousFrameCheckCount;
        }
    }

    void AddNewPoint(Vector3 point)
    {
        if(framesOnScreen < previousFrameCheckCount)
        {
            previousWandPositions[framesOnScreen] = point;
        }
        else
        {
            for(int i = 0; i < previousFrameCheckCount - 1; ++i)
            {
                previousWandPositions[i] = previousWandPositions[i + 1];
            }
            previousWandPositions[previousFrameCheckCount - 1] = point;
        }
    }

    bool MovedXTimes()
    {
        if(framesOnScreen < previousFrameCheckCount)
        {
            return false;
        }
        for(int i = 0; i < previousFrameCheckCount - 1; ++i)
        {
            if(previousWandPositions[i] == previousWandPositions[i + 1])
            {
                return false;
            }
        }
        return true;
    }

    public override bool IsTriggered()
    {
        return triggered;
    }
}
