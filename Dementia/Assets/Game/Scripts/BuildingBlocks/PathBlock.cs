using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Connection
{
    public SnapPoint mSelfBlockSnapPoint;
    public SnapPoint mOtherBlockSnapPoint;
    
    public Connection(SnapPoint pSelfBlock,SnapPoint pOtherBlock)
    {
        mSelfBlockSnapPoint = pSelfBlock;
        mOtherBlockSnapPoint = pOtherBlock;
    }
}

public class PathBlock : MonoBehaviour
{
    public bool mIsMoving = false;
    public bool mTraversed = false;
    public bool mIsEnemy = false;
    public bool mIsFirst = false;
    public Dictionary<int, Connection> mActiveClosestPoints = new Dictionary<int, Connection>();
    public PlayerWaypoint[] mPlayerWaypoints;

    public Connection mCurrentConnection = new Connection(null,null);
    public Connection mNextConnection = new Connection(null, null);

    public static Dictionary<int, Connection> mActiveConnections = new Dictionary<int, Connection>();

    [Header("Debug")]
    public bool mTrySnapping;
    
    void Start()
    {
        if(mPlayerWaypoints.Length == 0)
        {
            Debug.LogErrorFormat("No Waypoints Set for Block {0}", gameObject.name);
        }
        for(int aI = 0; aI < mPlayerWaypoints.Length; aI ++)
        {
            mPlayerWaypoints[aI].mWaypointIx = aI;
        }
    }

    void Update()
    {
        if(mTrySnapping)
        {
            SnapBlock();
            mTrySnapping = false;
        }
    }

    public void SnapBlock()
    {
        if(mActiveClosestPoints.Count <= 0)
        {
            return;
        }

        float aDistance = float.MaxValue;
        Connection aSelectedConnection = new Connection(null, null);
        foreach(Connection aConnection in mActiveClosestPoints.Values)
        {
            Vector3 aPath = aConnection.mOtherBlockSnapPoint.transform.position - aConnection.mSelfBlockSnapPoint.transform.position;
            float aSqMag = aPath.sqrMagnitude;
            if (aDistance > aSqMag)
            {
                aDistance = aSqMag;
                aSelectedConnection = aConnection;
            }
        }
        mActiveClosestPoints.Clear();
        foreach (Connection aConnection in PathBlock.mActiveConnections.Values)
        {
            aConnection.mSelfBlockSnapPoint.gameObject.GetComponent<MeshRenderer>().enabled = false;
            aConnection.mOtherBlockSnapPoint.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        mActiveConnections.Clear();
        if(aSelectedConnection.mSelfBlockSnapPoint == null)
        {
            return;
        }
        mCurrentConnection = aSelectedConnection;
        WaypointManager.AddNewConnection(mCurrentConnection);
        //debug only for now
        Vector3 aEndGoal = aSelectedConnection.mOtherBlockSnapPoint.transform.position;
        Vector3 aStartGoal = aSelectedConnection.mSelfBlockSnapPoint.transform.position;
        aEndGoal.y = aStartGoal.y = 0;
        transform.parent.position += (aEndGoal - aStartGoal);

    }

    public void OnCollisionEnter(Collision collision)
    {
        if(mCurrentConnection.mOtherBlockSnapPoint == null && !mIsFirst)
        {
            return;
        }
        PlayerMovement aPlayer = collision.collider.GetComponent<PlayerMovement>();
        if(aPlayer != null)
        {
            WaypointManager.SetActivePathblock(this);
        }
    }

}
