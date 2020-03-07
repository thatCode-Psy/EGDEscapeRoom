using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyScript : MonoBehaviour
{
	public GameObject player1;
	public GameObject player2;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player1.transform.position += 0.05f * Vector3.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            player1.transform.position += 0.05f * Vector3.down;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            player2.transform.position += 0.05f * Vector3.up;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            player2.transform.position += 0.05f * Vector3.down;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("hello");
    }
}
