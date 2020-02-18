using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RingScript))]
public class RingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        RingScript script = target as RingScript;
        DrawDefaultInspector();

        for(int i = 0; i < script.rings.Length; ++i)
        {
            if(i < script.currentAngle.Length)
            {
                script.rings[i].transform.localEulerAngles = new Vector3(0, script.currentAngle[i], 0);
            }
            else
            {
                break;
            }
        }
    }
}
