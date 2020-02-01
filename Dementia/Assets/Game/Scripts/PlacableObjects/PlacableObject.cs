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

        foreach(Vector3 v in wayPoints)
        {
            GameObject work = new GameObject();
            work.transform.position = v + transform.position;
            work.transform.parent = this.transform;
            work.AddComponent<PlayerWaypoint>();
            mPathBlock.mPlayerWaypoints.Add(work.GetComponent<PlayerWaypoint>());
        }
    }

}

