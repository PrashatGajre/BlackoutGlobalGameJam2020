using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mMoveSpeed;
    
    public float mStartDelay;

    bool mIsMoving = false;

    PlayerWaypoint mCurrentWaypoint;

    void Update()
    {
        if(!mIsMoving)
        {
            if (mStartDelay <= 0.0f)
            {
                mIsMoving = true;
            }
            mStartDelay -= Time.deltaTime;
            return;
        }

        if(mCurrentWaypoint != null)
        {
            transform.position += (mCurrentWaypoint.transform.position - transform.position) * Time.deltaTime * mMoveSpeed;
        }

    }


}
