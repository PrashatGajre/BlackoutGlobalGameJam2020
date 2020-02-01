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
            po.wayPoints[i] = Handles.DoPositionHandle(po.wayPoints[i] + po.transform.position, Quaternion.identity) - po.transform.position;
        }

        Handles.color = Color.red;

        Vector3[] waypointsArray = po.wayPoints.ToArray();

        for (int i = 0; i < waypointsArray.Length; i++)
        {
            waypointsArray[i] = waypointsArray [i] + po.transform.position;
        }

        Handles.DrawPolyLine(waypointsArray);

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
