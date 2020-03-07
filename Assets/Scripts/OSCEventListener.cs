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

    public Transform stick1Root;
    public Transform stick2Root;
    public Transform stick3Root;

    public Transform bigTri1Root;
    public Transform bigTri2Root;
    public Transform mediumTriRoot;
    public Transform smallTri1Root;
    public Transform smallTri2Root;
    public Transform parallelogramRoot;
    public Transform squareRoot;


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


    void SetTransform(Transform fancyTransform, string[] messageParts, int index)
    {
        if (!fancyTransform)
        {
            Debug.Log("TRANSFORM NOT SET FOR INDEX " + index);
            return;
        }

        //words will have 1 less argument on mac than on windows, so we have to shift all of the
        //indexes over in this case. :(
        //stupid macs (michael)
        int s = 0;
        if (SystemInfo.operatingSystem.Contains("Mac"))
        {
            s = 1;
        }


        int i = 6 * index - s + 5;


        fancyTransform.localPosition = new Vector3(float.Parse(messageParts[i + 0]),
                                              float.Parse(messageParts[i + 2]),
                                              float.Parse(messageParts[i + 1]));
                                                   
        fancyTransform.localEulerAngles = new Vector3(float.Parse(messageParts[i + 4]),
                                                        float.Parse(messageParts[i + 3]),
                                                        float.Parse(messageParts[i + 5]));
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


                SetTransform(hat1Root, words, 0);
                SetTransform(hat2Root, words, 1);
                SetTransform(hat3Root, words, 2);
                SetTransform(hat4Root, words, 3);

                SetTransform(stick1Root, words, 4);
                SetTransform(stick2Root, words, 5);
                SetTransform(stick3Root, words, 6);

                SetTransform(bigTri1Root, words, 7);
                SetTransform(bigTri2Root, words, 8);
                SetTransform(mediumTriRoot, words, 9);
                SetTransform(smallTri1Root, words, 10);
                SetTransform(smallTri2Root, words, 11);
                SetTransform(parallelogramRoot, words, 12);
                SetTransform(squareRoot, words, 13);

                ////convert Vicon coordinates to Unity coordinates
                //if (hat1Root)
                //{
                //    hat1Root.localPosition = new Vector3(float.Parse(words[5 - s]), float.Parse(words[7 - s]), float.Parse(words[6 - s]));
                //}
                //if (hat2Root)
                //{
                //    hat2Root.localPosition = new Vector3(float.Parse(words[8 - s]), float.Parse(words[10 - s]), float.Parse(words[9 - s]));
                //    hat2Root.localEulerAngles = new Vector3(-float.Parse(words[27 - s]), -float.Parse(words[26 - s]), float.Parse(words[28 - s]));
                //}
                //if (hat3Root)
                //{
                //    hat3Root.localPosition = new Vector3(float.Parse(words[11 - s]), float.Parse(words[13 - s]), float.Parse(words[12 - s]));
                //    hat3Root.localEulerAngles = new Vector3(-float.Parse(words[24 - s]), -float.Parse(words[23 - s]), float.Parse(words[25 - s]));
                //}
                //if (hat4Root)
                //{
                //    hat4Root.localPosition = new Vector3(float.Parse(words[14 - s]), float.Parse(words[16 - s]), float.Parse(words[15 - s]));
                //}
                //if (wandRoot)
                //{
                //    wandRoot.localPosition = new Vector3(float.Parse(words[17 - s]), float.Parse(words[19 - s]), float.Parse(words[18 - s]));
                //    wandRoot.localEulerAngles = new Vector3(-float.Parse(words[21 - s]), -float.Parse(words[20 - s]), float.Parse(words[22 - s]));
                //}




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
