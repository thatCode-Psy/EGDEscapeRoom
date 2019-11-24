using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManagerScript : MonoBehaviour
{
    public GameObject[] targets;
    public int[] goalTargets;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int activeCount = 0;
        int correctCount = 0;
        for(int index = 0; index < targets.Length; ++index)
        {
            TargetScript targetScript = targets[index].GetComponent<TargetScript>();
            if (targetScript.IsTriggered())
            {
                ++activeCount;
                if (Contains(index))
                {
                    ++correctCount;
                }
                if(activeCount == goalTargets.Length)
                {
                    break;
                }
            }
            
        }
        if(correctCount == goalTargets.Length)
        {
            foreach(GameObject target in targets)
            {
                target.SetActive(false);
            }
            GameObject.FindGameObjectWithTag("PhaseManager").GetComponent<PhaseScript>().AdvanceToPhase(4);
            
        }
        else if(activeCount == goalTargets.Length)
        {
            foreach(GameObject target in targets)
            {
                TargetScript targetScript = target.GetComponent<TargetScript>();
                targetScript.Deactivate();
            }
        }
    }

    bool Contains(int i)
    {
        foreach(int targetIndex in goalTargets)
        {
            if(i == targetIndex)
            {
                return true;
            }
        }
        return false;
    }
}
