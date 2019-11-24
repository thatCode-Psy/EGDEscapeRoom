using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    bool triggered;
    bool colliding;
    // Start is called before the first frame update
    void Start()
    {
        colliding = false;
        triggered = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colliding && Input.GetMouseButtonUp(0))
        {
            triggered = true;
            spriteRenderer.enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Dot")
        {
            colliding = true;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Dot")
        {
            colliding = false;
        }
    }

    public void Deactivate()
    {
        triggered = false;
        spriteRenderer.enabled = false;
    }

    public bool IsTriggered()
    {
        return triggered;
    }

}
