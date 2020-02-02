using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyRangeDetector mRangeDetector;


    void OnTriggerEnter(Collider other)
    {
        if(mRangeDetector.mPlayer == null)
        {
            return;
        }
        if(mRangeDetector.mPlayer.gameObject != other.gameObject)
        {
            return;
        }

        if(mRangeDetector.mPlayer.mDead)
        {
            return;
        }

        mRangeDetector.mPlayer.KillPlayer();
    }

}
