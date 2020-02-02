using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mMoveSpeed;

    public float mStartDelay;

    bool mIsMoving = false;

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
            mMovePosition = transform.position + (mCurrentWaypoint.transform.position - mPreviousWaypoint.transform.position) * 2.0f;
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
        if (transform.position.x <= mMovePosition.x + 0.5f && transform.position.x >= mMovePosition.x - 0.5f
        && transform.position.z <= mMovePosition.z + 0.5f && transform.position.z >= mMovePosition.z - 0.5f)
        {
            ResetWaypoint();
        }

    }


}
