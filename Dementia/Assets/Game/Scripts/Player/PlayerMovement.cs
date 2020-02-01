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

    void Start()
    {
        mMovePosition = transform.position;
        mPreviousWaypoint = null;
        mCurrentWaypoint = null;
    }

    void Update()
    {
        if (!mIsMoving)
        {
            if (mStartDelay <= 0.0f)
            {
                mIsMoving = true;
                mCurrentWaypoint = WaypointManager.GetNextWaypoint(mCurrentWaypoint == null,transform.position);
            }
            mStartDelay -= Time.deltaTime;
            return;
        }
        transform.position += (mMovePosition - transform.position) * Time.deltaTime * mMoveSpeed;
        if (transform.position.x <= mMovePosition.x + 0.5f && transform.position.x >= mMovePosition.x - 0.5f
        && transform.position.z <= mMovePosition.z + 0.5f && transform.position.z >= mMovePosition.z - 0.5f)
        {
            PlayerWaypoint aWayPoint = WaypointManager.GetNextWaypoint(mCurrentWaypoint == null,transform.position);
            if (aWayPoint == null)
            {
                mMovePosition = transform.position + (mCurrentWaypoint.transform.position - mPreviousWaypoint.transform.position) * 10.0f;
                WaypointManager.GetPlayerFinalPosition(ref mMovePosition);
            }
            else
            {
                mPreviousWaypoint = mCurrentWaypoint;
                mCurrentWaypoint = aWayPoint;
            }
        }

    }


}
