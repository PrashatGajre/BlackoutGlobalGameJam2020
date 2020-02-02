using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyRangeDetector mRangeDetector;

    void OnTriggerEnter(Collider other)
    {
        if(!mRangeDetector.mDead)
        {
            CheckLight(other);
        }
        if(!mRangeDetector.mDead)
        {
            CheckPlayer(other);
        }
    }

    void CheckLight(Collider other)
    {
        LightObject aLight = other.GetComponent<LightObject>();
        if(aLight ==null)
        {
            return;
        }
        mRangeDetector.mDead = true;
        mRangeDetector.gameObject.SetActive(false);
        LevelManager.Instance.OnEnemyAlarmOff(transform.parent);
    }

    void CheckPlayer(Collider other)
    {
        if (mRangeDetector.mPlayer == null)
        {
            return;
        }
        if (mRangeDetector.mPlayer.gameObject != other.gameObject)
        {
            return;
        }

        if (mRangeDetector.mPlayer.mDead)
        {
            return;
        }

        mRangeDetector.mPlayer.KillPlayer();
    }

}
