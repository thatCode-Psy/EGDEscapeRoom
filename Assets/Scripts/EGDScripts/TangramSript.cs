using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


struct DataPoint
{
    public string name1;
    public string name2;
    public float distance;
}


public class TangramSript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bigtri1;
    public GameObject bigtri2;
    public GameObject smalltri1;
    public GameObject smalltri2;
    public GameObject mediumtri;
    public GameObject square;
    public GameObject parallelogram;

    public int numMatches;
    public int numRequiredMatches;
    public float tolerance;
    public string puzzleName;
    public bool puzzleSolved = false;

    private List<DataPoint> dataPoints = new List<DataPoint>();

    void Start()
    {
        bigtri1 = GameObject.Find("bigtri1");
        bigtri2 = GameObject.Find("bigtri2");
        smalltri1 = GameObject.Find("smalltri1");
        smalltri2 = GameObject.Find("smalltri2");
        mediumtri = GameObject.Find("mediumtri");
        square = GameObject.Find("square");
        parallelogram = GameObject.Find("parallelogram");

        StreamReader reader = new StreamReader("Assets/Text/" + puzzleName + ".txt");

        while (!reader.EndOfStream)
        {
            string str = reader.ReadLine();
            print(str);
            string[] strs2 = str.Split(' ');
            DataPoint p;
            p.name1 = strs2[0];
            p.name2 = strs2[1];
            p.distance = float.Parse(strs2[2]);
            dataPoints.Add(p);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Output();
        }

        numMatches = 0;

        foreach (DataPoint dp in dataPoints)
        {
            GameObject g1 = Get(dp.name1);
            GameObject g2 = Get(dp.name2);
            float dist = (g1.transform.position - g2.transform.position).magnitude;
            float diffDist = Mathf.Abs(1.0f - (dist / dp.distance));

            if (diffDist < tolerance)
            {
                numMatches++;
            }
        }

        if (numMatches >= numRequiredMatches)
        {
            puzzleSolved = true;
        }
    }

    void Output()
    {
        StreamWriter writer = new StreamWriter("Assets/Text/out.txt", true);
        for (int x = 0; x < 7; x++)
        {
            for (int y = x + 1; y < 7; y++)
            {
                GameObject g1 = Get(x);
                GameObject g2 = Get(y);
                writer.WriteLine(g1.name + " " + g2.name + " " + (g1.transform.position - g2.transform.position).magnitude);
            }
        }

        writer.Close();
    }


    GameObject Get(int num)
    {
        if (num == 0)
        {
            return bigtri1;
        }
        if (num == 1)
        {
            return bigtri2;
        }
        if (num == 2)
        {
            return smalltri1;
        }
        if (num == 3)
        {
            return smalltri2;
        }
        if (num == 4)
        {
            return mediumtri;
        }
        if (num == 5)
        {
            return square;
        }
        if (num == 6)
        {
            return parallelogram;
        }
        return null;
    }

    GameObject Get(string s)
    {
        if (s.Equals("bigtri1"))
        {
            return bigtri1;
        }
        if (s.Equals("bigtri2"))
        {
            return bigtri2;
        }
        if (s.Equals("smalltri1"))
        {
            return smalltri1;
        }
        if (s.Equals("smalltri2"))
        {
            return smalltri2;
        }
        if (s.Equals("mediumtri"))
        {
            return mediumtri;
        }
        if (s.Equals("square"))
        {
            return square;
        }
        if (s.Equals("parallelogram"))
        {
            return parallelogram;
        }
        return null;
    }
}
