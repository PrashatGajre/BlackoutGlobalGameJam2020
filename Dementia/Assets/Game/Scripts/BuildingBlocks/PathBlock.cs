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
    public Dictionary<int, Connection> mActiveClosestPoints = new Dictionary<int, Connection>();
    public List<PlayerWaypoint> mPlayerWaypoints;

    public Connection mCurrentConnection = new Connection(null,null);


    [Header("Debug")]
    public bool mTrySnapping;

    public void MoveBlock(Vector3 pPosition)
    {
        mIsMoving = true;
    }

    public void RotateBlock(float pDelta)
    {
        mIsMoving = true;
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
        if(aSelectedConnection.mSelfBlockSnapPoint == null)
        {
            return;
        }
        mCurrentConnection = aSelectedConnection;
        //debug only for now
        transform.position += (aSelectedConnection.mOtherBlockSnapPoint.transform.position - aSelectedConnection.mSelfBlockSnapPoint.transform.position);

    }

    public void OnCollisionEnter(Collision collision)
    {
        PlayerMovement aPlayer = collision.collider.GetComponent<PlayerMovement>();
        if(aPlayer != null)
        {
            WaypointManager.SetActivePathblock(this);
        }
    }

}
