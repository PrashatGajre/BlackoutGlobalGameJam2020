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


    public Animator mAnimator;
    public SpriteRenderer mRenderer;



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
            Vector3 aTempMP = Vector3.zero;
            if(mPreviousWaypoint == null || mCurrentWaypoint == null)
            {
                aTempMP = mMovePosition = transform.position;
            }
            else
            {
                aTempMP = mMovePosition = transform.position + (mCurrentWaypoint.transform.position - mPreviousWaypoint.transform.position) * 2.0f;
            }
            WaypointManager.GetPlayerFinalPosition(ref mMovePosition);
            if(mMovePosition == aTempMP)
            {
                KillPlayer();
            }
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

    public void KillPlayer()
    {
        mDead = true;
        GetComponent<Rigidbody>().isKinematic = false;
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
        SetAnimatorValues((mMovePosition - transform.position).normalized);
        if(mDebug)
        {
            Debug.DrawRay(transform.position, (mMovePosition - transform.position), Color.red);
        }
        if (transform.position.x <= mMovePosition.x + 0.2f && transform.position.x >= mMovePosition.x - 0.2f
        && transform.position.z <= mMovePosition.z + 0.2f && transform.position.z >= mMovePosition.z - 0.2f)
        {
            ResetWaypoint();
        }
    }

    void SetAnimatorValues(Vector3 pMoveVector)
    {
        if(pMoveVector.sqrMagnitude == 0.0f)
        {
            mAnimator.SetFloat("IdleMag", 0.0f);
            mAnimator.SetFloat("Horizontal", 0.0f);
            mAnimator.SetFloat("Vertical", 0.0f);
            return;
        }

        float aAngle = Vector3.SignedAngle(transform.right, pMoveVector,Vector3.up);
        mAnimator.SetFloat("IdleMag", 1.0f);
        mRenderer.flipX = false;
        if (aAngle >= 15f && aAngle <= 15f)
        {
            mAnimator.SetFloat("Horizontal", 0.0f);
            mAnimator.SetFloat("Vertical", 1.0f);

        }
        else if(aAngle >=-165f && aAngle <= 165f)
        {
            mAnimator.SetFloat("Horizontal", 0.0f);
            mAnimator.SetFloat("Vertical", -1.0f);

        }
        else if(aAngle < 0.0f)
        {
            mAnimator.SetFloat("Horizontal", -1.0f);
            mAnimator.SetFloat("Vertical", 0.0f);
            mRenderer.flipX = true;

        }
        else
        {
            mAnimator.SetFloat("Horizontal", 1.0f);
            mAnimator.SetFloat("Vertical", 0.0f);
        }


    }

}
