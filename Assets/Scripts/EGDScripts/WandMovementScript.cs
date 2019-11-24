using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandMovementScript : TriggeringObject
{

    public bool triggered;
    bool started;
    GameObject wandObject;
    Vector3 currentWandPosition;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
        started = false;
        wandObject = null;
        currentWandPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("EGDController") != null && !started)
        {
            started = true;
            wandObject = GameObject.FindGameObjectWithTag("Wand");
            currentWandPosition = wandObject.transform.position;
            wandObject.GetComponent<WandScript>().SpawnBall();
        }
        if (started)
        {
            if (currentWandPosition != wandObject.transform.position)
            {
                triggered = true;
            }
        }

    }


    public override bool IsTriggered()
    {
        return triggered;
    }
}
