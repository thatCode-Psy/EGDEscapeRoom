using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phase
{
    public GameObject[] activateConditions;
}


public class PhaseScript : MonoBehaviour
{
   
    public Phase[] phases;
    int phaseIndex;
    // Start is called before the first frame update
    void Start()
    {
        phaseIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (AdvancePhaseCheck())
        {

        }
    }

    bool AdvancePhaseCheck()
    {    
        Phase currentPhase = phases[phaseIndex];
        foreach(GameObject trigger in currentPhase.activateConditions)
        {
            ColliderScript script = trigger.GetComponent<ColliderScript>();
            if(script == null)
            {
                Debug.LogWarning("Using phase manager with GameObject without ColliderScript");
                return false;
            }
            if (!script.IsTriggered())
            {
                return false;
            }
        }
        ++phaseIndex;

        return true;
    }
}
