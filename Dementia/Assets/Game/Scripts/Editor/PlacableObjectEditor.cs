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

        if (po.mUseSnapingEditor)
        {
            foreach (Transform sp in po.snapingPoints)
            {
                sp.position = Handles.DoPositionHandle(sp.position, sp.rotation);
                sp.rotation = Handles.DoRotationHandle(sp.rotation, sp.position);
            }
        }

        if(po.mUseWaypointsEditor)
        {
            for (int i = 0; i < po.wayPoints.Count; i++)
            {
                po.wayPoints[i] = (Handles.DoPositionHandle(po.wayPoints[i] + po.transform.position, Quaternion.identity) - po.transform.position);
            }

            Handles.color = Color.red;

            Vector3[] waypointsArray = po.wayPoints.ToArray();

            for (int i = 0; i < waypointsArray.Length; i++)
            {
                waypointsArray[i] = waypointsArray[i] + po.transform.position;
            }

            Handles.DrawPolyLine(waypointsArray);
        }

        Handles.BeginGUI();
        if (po.mUseSnapingEditor)
        {
            if (GUILayout.Button("new node"))
            {
                po.snapingPoints.Add(Instantiate(po.spPrefab, po.transform).transform);
            }
        }
        if (po.mUseWaypointsEditor)
        {
            if (GUILayout.Button("new waypoint"))
            {
                po.wayPoints.Add(new Vector3(3, 0, 3));
            }
        }
        Handles.EndGUI();



    }
}
