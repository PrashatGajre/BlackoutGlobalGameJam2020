using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeDetector : MonoBehaviour
{
    public PlayerMovement mPlayer;
    public float mSpeed;
    public bool mDead;


    void Update()
    {
        if(mPlayer == null)
        {
            return;
        }

        if(!LevelManager.Instance.mPlay)
        {
            return;
        }
        transform.position += (mPlayer.transform.position - transform.position) * mSpeed * Time.deltaTime;

    }


    void OnTriggerEnter(Collider other)
    {
        PlayerMovement aPlayer = other.GetComponent<PlayerMovement>();
        if(aPlayer != null)
        {
            mPlayer = aPlayer;
            if(!LevelManager.Instance.mTutDone)
            {
                LevelManager.Instance.StartTutorial();
            }
            LevelManager.Instance.OnEnemyAlarmOn(transform);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(mPlayer == null)
        {
            PlayerMovement aPlayer = other.GetComponent<PlayerMovement>();
            if (aPlayer != null)
            {
                mPlayer = aPlayer;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            mPlayer = null;
            LevelManager.Instance.OnEnemyAlarmOff(transform);
        }
    }
}
