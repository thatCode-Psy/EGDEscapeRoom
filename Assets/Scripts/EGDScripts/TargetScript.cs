using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    bool triggered;
    bool colliding;

    GameObject dot = null;
    float dotRadius;
    float targetRadius;
    // Start is called before the first frame update
    void Start()
    {
        colliding = false;
        triggered = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetRadius = GetComponent<SphereCollider>().radius * transform.lossyScale.x;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(dot == null)
        {
            dot = GameObject.FindGameObjectWithTag("Dot");
            if(dot != null)
            {
                dotRadius = dot.GetComponent<SphereCollider>().radius * dot.transform.lossyScale.x;
            }
        }
        
        if (Colliding() && Input.GetMouseButtonUp(0))
        {
            triggered = true;
            spriteRenderer.enabled = true;
        }
    }

    private bool Colliding()
    {
        if(dot == null)
        {
            return false;
        }
        return Vector3.Distance(dot.transform.position, transform.position) < dotRadius + targetRadius;
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
