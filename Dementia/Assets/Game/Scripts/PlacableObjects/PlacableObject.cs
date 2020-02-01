using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableObject : MonoBehaviour
{
    public List<Transform> snapingPoints;
    public List<Vector3> wayPoints;
    public GameObject spPrefab;

    public bool mUseSnapingEditor = false;
    public bool mUseWaypointsEditor = false;

    private PathBlock mPathBlock;

    private void Awake()
    {
        mPathBlock = GetComponent<PathBlock>();

        if(mPathBlock == null)
        {
            return;
        }

        mPathBlock.mPlayerWaypoints = new PlayerWaypoint[wayPoints.Count];

        for (int i = 0; i< wayPoints.Count; i++)
        {
            GameObject work = new GameObject();
            work.transform.position = wayPoints[i] + transform.position;
            work.transform.parent = this.transform;
            work.AddComponent<PlayerWaypoint>();
            mPathBlock.mPlayerWaypoints[i] = work.GetComponent<PlayerWaypoint>();
        }
        
    }

}

