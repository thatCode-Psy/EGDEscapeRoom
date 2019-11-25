using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggeringObject : MonoBehaviour
{

    protected bool activated = false;


    public abstract bool IsTriggered();

    public void Activate()
    {
        activated = true;
    }
}
