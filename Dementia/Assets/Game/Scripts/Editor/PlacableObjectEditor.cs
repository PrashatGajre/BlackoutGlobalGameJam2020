using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlacableObject))]
public class PlacableObjectEditor : Editor
{
    private void OnSceneGUI()
    {
        PlacableObject po = (PlacableObject)target;
        if(!po.mUseEditor)
        {
            return;
        }

        foreach (Transform sp in po.snapingPoints)
        {
            sp.position = Handles.DoPositionHandle(sp.position, sp.rotation);
            sp.rotation = Handles.DoRotationHandle(sp.rotation, sp.position);
        }

        for(int i = 0; i < po.wayPoints.Count; i++)
        {
            po.wayPoints[i] = Handles.DoPositionHandle(po.wayPoints[i], Quaternion.identity);
        }

        Handles.color = Color.red;
        Handles.DrawPolyLine(po.wayPoints.ToArray());

        Handles.BeginGUI();
        if (GUILayout.Button("new node"))
        {
            po.snapingPoints.Add(Instantiate(po.spPrefab, po.transform).transform);
        }
        if (GUILayout.Button("new waypoint"))
        {
            po.snapingPoints.Add(Instantiate(po.spPrefab, po.transform).transform);
        }
        Handles.EndGUI();


    }
}
