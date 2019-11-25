using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OSCEventListener : MonoBehaviour
{


    public Transform hat1Root;
    public Transform hat2Root;
    public Transform hat3Root;
    public Transform hat4Root;
    public Transform wandRoot;
    private static OSCEventListener osc;
    public static OSCEventListener OSC
    {
        get
        {
            if (osc == null)
                osc = GameObject.FindObjectOfType<OSCEventListener>();
            return osc;
        }
    }

    // Use this for initialization
    void Start()
    {
        OSCHandler.Instance.Init(); //init OSC
                                    //projectionRoot = GameObject.Find("ProjectionRoot").transform;
                                    //pointerRoot = GameObject.Find("PointerRoot").GetComponent<PointerControl>();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (CustomNetworkManager.direction == "center")
        {
            /*
			OSCHandler.Instance.UpdateLogs();

			List<UnityOSC.OSCPacket> packets=OSCHandler.Instance.Servers["HeadTracker"].packets;
			
			if (packets.Count>0)
			Debug.Log (packets[0].Address);
			
			*/
            OSCHandler.Instance.UpdateLogs();
            List<string> server_messages = OSCHandler.Instance.Servers["HeadTracker"].log;
            foreach (string msg in server_messages)
            {
                //Debug.Log (msg);
                //parse message and update tracker position
                string[] words = msg.Split(' ');


                //Debug.Log(words.Length);
                if (words.Length == 23) //stupid macs
                {

                    //convert Vicon coordinates to Unity coordinates
                    if (hat1Root)
                    {
                        hat1Root.localPosition = new Vector3(float.Parse(words[4]), float.Parse(words[6]), float.Parse(words[5]));
                    }
                    if (hat2Root)
                    {
                        hat2Root.localPosition = new Vector3(float.Parse(words[7]), float.Parse(words[9]), float.Parse(words[8]));
                    }
                    if (hat3Root)
                    {
                        hat3Root.localPosition = new Vector3(float.Parse(words[10]), float.Parse(words[12]), float.Parse(words[11]));
                    }
                    if (hat4Root)
                    {
                        hat4Root.localPosition = new Vector3(float.Parse(words[13]), float.Parse(words[15]), float.Parse(words[14]));
                    }
                    if (wandRoot)
                    {
                        wandRoot.localPosition = new Vector3(float.Parse(words[16]), float.Parse(words[18]), float.Parse(words[17]));
                        wandRoot.localEulerAngles = new Vector3(-float.Parse(words[20]), -float.Parse(words[19]), float.Parse(words[21]));
                    }
                }
                else if (words.Length == 24) //windows (best)
                {
                    //convert Vicon coordinates to Unity coordinates
                    if (hat1Root)
                    {
                        hat1Root.localPosition = new Vector3(float.Parse(words[5]), float.Parse(words[7]), float.Parse(words[6]));
                    }
                    if (hat2Root)
                    {
                        hat2Root.localPosition = new Vector3(float.Parse(words[8]), float.Parse(words[10]), float.Parse(words[9]));
                    }
                    if (hat3Root)
                    {
                        hat3Root.localPosition = new Vector3(float.Parse(words[11]), float.Parse(words[13]), float.Parse(words[12]));
                    }
                    if (hat4Root)
                    {
                        hat4Root.localPosition = new Vector3(float.Parse(words[14]), float.Parse(words[16]), float.Parse(words[15]));
                    }
                    if (wandRoot)
                    {
                        wandRoot.localPosition = new Vector3(float.Parse(words[17]), float.Parse(words[19]), float.Parse(words[18]));
                        wandRoot.localEulerAngles = new Vector3(-float.Parse(words[21]), -float.Parse(words[20]), float.Parse(words[22]));
                    }
                }
                else
                {
                    Debug.LogError(":(");
                }
 


                // hat2Root.localEulerAngles = new Vector3(-float.Parse(words[15]), -float.Parse(words[14]), float.Parse(words[16]));
                Vector3 pos = hat1Root.localPosition;
                pos *= 3.28084f;
                GameObject.FindObjectOfType<Text>().text = pos.ToString();
                //14, 15, 16 = H, P, R
                // rotate X = pitch
                // rotate Y = heading
                // rotate Z = roll
                
                /*
                if (hat3Root)
                {
                    hat3Root.localPosition = new Vector3(float.Parse(words[17]), float.Parse(words[19]), float.Parse(words[18]));
                }
                if (hat4Root)
                {
                    hat4Root.localPosition = new Vector3(float.Parse(words[23]), float.Parse(words[25]), float.Parse(words[24]));
                }*/
            }



        }
    }

}
