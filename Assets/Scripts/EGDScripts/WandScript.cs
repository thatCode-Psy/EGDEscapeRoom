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
        dot = Instantiate(dotPrefab);
        dot.transform.position = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask mask = 1 << 8;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, mask)){
            dot.transform.position = hit.point;
        }
    }
}
