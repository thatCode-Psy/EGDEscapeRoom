using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phase
{
    public GameObject[] activateConditions;
    public Texture background;
    public bool final = false;
}


public class PhaseScript : MonoBehaviour
{
   
    public Phase[] phases;
    public int phaseIndex;
    GameObject backwall;
    Renderer backwallRenderer;
    // Start is called before the first frame update
    void Start()
    {
        phaseIndex = 0;
        backwall = GameObject.FindGameObjectWithTag("TestWall");
        backwallRenderer = backwall.GetComponent<Renderer>();
        backwallRenderer.material.mainTexture = phases[0].background;
        //foreach(GameObject trigger in phases[0].activateConditions)
        //{
        //    TriggeringObject script = trigger.GetComponent<TriggeringObject>();
        //    if (script == null)
        //    {
        //        Debug.LogWarning("Using phase manager with GameObject without TriggeringObject");
        //    }
        //    else
        //    {
        //        script.Activate();
        //    }

        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AdvancePhaseCheck())
        {
            //foreach(GameObject trigger in phases[phaseIndex].activateConditions)
            //{
            //    TriggeringObject script = trigger.GetComponent<TriggeringObject>();
            //    script.Deactivate();
            //}
            ++phaseIndex;
            backwallRenderer.material.mainTexture = phases[phaseIndex].background;
            //foreach (GameObject trigger in phases[phaseIndex].activateConditions)
            //{
            //    TriggeringObject script = trigger.GetComponent<TriggeringObject>();
            //    if (script == null)
            //    {
            //        Debug.LogWarning("Using phase manager with GameObject without TriggeringObject");
            //    }
            //    else
            //    {
            //        script.Activate();
            //    }
            //}
        }
    }

    bool AdvancePhaseCheck()
    {
        if(phaseIndex >= phases.Length)
        {
            return false;
        }
        Phase currentPhase = phases[phaseIndex];
        if (currentPhase.final || currentPhase.activateConditions.Length == 0)
        {
            return false;
        }
        foreach(GameObject trigger in currentPhase.activateConditions)
        {
            TriggeringObject script = trigger.GetComponent<TriggeringObject>();
            if(script == null)
            {
                Debug.LogWarning("Using phase manager with GameObject without TriggeringObject");
                return false;
            }
            if (!script.IsTriggered())
            {
                return false;
            }
        }
        
        
        Debug.Log("Advanced Phase");
        return true;
    }

    public void AdvanceToPhase(int phase)
    {
        phaseIndex = phase;
        backwallRenderer.material.mainTexture = phases[phaseIndex].background;
    }
}
