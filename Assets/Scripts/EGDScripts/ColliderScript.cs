using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : TriggeringObject
{
    public string[] triggerTags;
    public bool triggered;
    Collider col;
    GameObject[] triggerObjects;
    bool started;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider>();
        triggered = false;
        started = false;
        triggerObjects = new GameObject[triggerTags.Length];
        
    }

    // Update is called once per frame
    void Update()
    {
        
            if (GameObject.FindGameObjectWithTag("EGDController") != null && !started && activated)
            {
                started = true;
                for (int i = 0; i < triggerTags.Length; ++i)
                {
                    triggerObjects[i] = GameObject.FindGameObjectWithTag(triggerTags[i]);
                }
            }
            if (started)
            {
                Bounds bounds = col.bounds;
                triggered = false;
                foreach (GameObject triggerObject in triggerObjects)
                {
                    if (bounds.Contains(triggerObject.transform.position))
                    {
                        triggered = true;
                        break;
                    }
                }
            }
        
        
    }

    public override bool IsTriggered()
    {
        return triggered;
    }
}
