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

    public GameObject BigTri1;
    public GameObject BigTri2;
    public GameObject SmallTri1;
    public GameObject SmallTri2;
    public GameObject MediumTri;
    public GameObject Square;
    public GameObject Parallelogram;

    public int numMatches;
    public int numRequiredMatches;
    public float tolerance;
    public string puzzleNames;
    public bool puzzleSolved = false;

    private List<List<DataPoint>> dataPoints = new List<List<DataPoint>>();

    void Start()
    {
        //BigTri1 = GameObject.Find("BigTri1");
        //BigTri2 = GameObject.Find("BigTri2");
        //SmallTri1 = GameObject.Find("SmallTri1");
        //SmallTri2 = GameObject.Find("SmallTri2");
        //MediumTri = GameObject.Find("MediumTri");
        //Square = GameObject.Find("Square");
        //Parallelogram = GameObject.Find("Parallelogram");

        string[] puzzleList = puzzleNames.Split(',');

        foreach (string puzzleName in puzzleList)
        {
            StreamReader reader = new StreamReader("Assets/Text/" + puzzleName + ".txt");

            List<DataPoint> data = new List<DataPoint>();

            while (!reader.EndOfStream)
            {
                string str = reader.ReadLine();
                //print(str);
                string[] strs2 = str.Split(' ');
                DataPoint p;
                p.name1 = strs2[0];
                p.name2 = strs2[1];
                p.distance = float.Parse(strs2[2]);
                data.Add(p);
            }

            dataPoints.Add(data);
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
        foreach (List<DataPoint> pointSet in dataPoints)
        {
            int partMatches = 0;

            foreach (DataPoint dp in pointSet)
            {
                GameObject g1 = Get(dp.name1);
                GameObject g2 = Get(dp.name2);
                float dist = (g1.transform.position - g2.transform.position).magnitude;
                float diffDist = Mathf.Abs(1.0f - (dist / dp.distance));
                if (diffDist < tolerance)
                {
                    partMatches++;
                }
            }

            numMatches = Mathf.Max(numMatches, partMatches);
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
            return BigTri1;
        }
        if (num == 1)
        {
            return BigTri2;
        }
        if (num == 2)
        {
            return SmallTri1;
        }
        if (num == 3)
        {
            return SmallTri2;
        }
        if (num == 4)
        {
            return MediumTri;
        }
        if (num == 5)
        {
            return Square;
        }
        if (num == 6)
        {
            return Parallelogram;
        }
        return null;
    }

    GameObject Get(string s)
    {
        if (s.Equals("BigTri1"))
        {
            return BigTri1;
        }
        if (s.Equals("BigTri2"))
        {
            return BigTri2;
        }
        if (s.Equals("SmallTri1"))
        {
            return SmallTri1;
        }
        if (s.Equals("SmallTri2"))
        {
            return SmallTri2;
        }
        if (s.Equals("MediumTri"))
        {
            return MediumTri;
        }
        if (s.Equals("Square"))
        {
            return Square;
        }
        if (s.Equals("Parallelogram"))
        {
            return Parallelogram;
        }
        return null;
    }
}
