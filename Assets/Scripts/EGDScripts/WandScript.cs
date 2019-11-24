using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandScript : MonoBehaviour
{
    public GameObject dotPrefab;

    GameObject dot;
   

    // Start is called before the first frame update
    void Start()
    {
        dot = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(dot != null)
        {
            LayerMask mask = 1 << 8;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask))
            {
                dot.transform.position = hit.point;
            }
        } 
        
    }

    public void SpawnBall()
    {
        dot = Instantiate(dotPrefab);
        dot.transform.position = Vector3.zero;
    }
}
