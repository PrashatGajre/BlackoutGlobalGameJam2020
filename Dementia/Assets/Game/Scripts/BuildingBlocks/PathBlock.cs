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
    public PlayerWaypoint[] mPlayerWaypoints;

    public Connection mCurrentConnection = new Connection(null,null);
    

    [Header("Debug")]
    public bool mTrySnapping;

    #region MOVEMENT
    private Transform parent;
    private MouseInput mouseInput;
    #endregion
    #region ROTATION
    private Quaternion rotation = Quaternion.identity;
    [Header("Movement/Rotation")]
    [SerializeField] private float rotationAngle = 15.0f;
    #endregion
    private void Awake()
    {
        parent = transform.parent;
        if (gameObject.GetComponent<MouseInput>() == null)
        {
            gameObject.AddComponent<MouseInput>();
        }
        mouseInput = gameObject.GetComponent<MouseInput>();
    }

    public void MoveBlock(Vector3 pPosition)
    {
        if (pPosition != Vector3.zero)
        {
            mIsMoving = true;
            parent.position = new Vector3(pPosition.x, 0, pPosition.z);
        }
    }

    public void RotateBlock()
    {
        mIsMoving = true;
        parent.Rotate(Vector3.up * rotationAngle);
    }

    void Update()
    {
        MoveBlock(mouseInput.MouseDragPosition());
        if (mouseInput.IsMouseRightUp())
        {
            RotateBlock();
        }

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
