using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mMoveSpeed;

    public float mStartDelay;

    bool mIsMoving = false;

    public bool mDead = false;

    PlayerWaypoint mCurrentWaypoint;
    PlayerWaypoint mPreviousWaypoint;
    Vector3 mMovePosition;

    [Header("Debug")]
    public bool mDebug = false;

    void Start()
    {
        mMovePosition = transform.position;
        mPreviousWaypoint = null;
        mCurrentWaypoint = null;
        WaypointManager.SetActivePlayer(this);
    }

    public void ResetWaypoint()
    {
        PlayerWaypoint aWayPoint = WaypointManager.GetNextWaypoint(mCurrentWaypoint, mPreviousWaypoint, transform.position);
        if (aWayPoint == null)
        {
            if(mPreviousWaypoint == null || mCurrentWaypoint == null)
            {
                mMovePosition = transform.position;
            }
            else
            {
                mMovePosition = transform.position + (mCurrentWaypoint.transform.position - mPreviousWaypoint.transform.position) * 2.0f;
            }
            WaypointManager.GetPlayerFinalPosition(ref mMovePosition);
            mCurrentWaypoint = null;
            mPreviousWaypoint = null;
        }
        else
        {
            mPreviousWaypoint = mCurrentWaypoint;
            mCurrentWaypoint = aWayPoint;
            mMovePosition = mCurrentWaypoint.transform.position;
        }
    }

    void Update()
    {
        if(mDead)
        {
            return;
        }
        if (!mIsMoving)
        {
            if (mStartDelay <= 0.0f)
            {
                mIsMoving = true;
                mCurrentWaypoint = WaypointManager.GetNextWaypoint(mCurrentWaypoint,mPreviousWaypoint,transform.position);
                mMovePosition = mCurrentWaypoint.transform.position;
            }
            mStartDelay -= Time.deltaTime;
            return;
        }
        transform.position += (mMovePosition - transform.position) * Time.deltaTime * mMoveSpeed;
        if(mDebug)
        {
            Debug.DrawRay(transform.position, (mMovePosition - transform.position), Color.red);
        }
        if (transform.position.x <= mMovePosition.x + 0.2f && transform.position.x >= mMovePosition.x - 0.2f
        && transform.position.z <= mMovePosition.z + 0.2f && transform.position.z >= mMovePosition.z - 0.2f)
        {
            ResetWaypoint();
        }

        if(!Physics.Raycast(transform.position, -Vector3.up,50.0f))
        {
            GetComponent<Rigidbody>().isKinematic = false;
            mDead = true;
        }


    }


}
