using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phase
{
    public GameObject[] activateConditions;
    public Texture background;
}


public class PhaseScript : MonoBehaviour
{
   
    public Phase[] phases;
    int phaseIndex;
    GameObject backwall;
    Renderer backwallRenderer;
    // Start is called before the first frame update
    void Start()
    {
        phaseIndex = 0;
        backwall = GameObject.FindGameObjectWithTag("TestWall");
        backwallRenderer = backwall.GetComponent<Renderer>();
        backwallRenderer.material.mainTexture = phases[0].background;
        
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
        if(phaseIndex >= phases.Length)
        {
            return false;
        }
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
        
        Debug.Log("Advanced Phase");
        return true;
    }
}
