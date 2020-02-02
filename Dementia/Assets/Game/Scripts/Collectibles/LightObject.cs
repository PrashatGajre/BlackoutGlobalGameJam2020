using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    public bool mPlaced;
    public bool mCollected = false;
    public float mSpeed;
    public EnemyRangeDetector mEnemy;

    void Update()
    {
        if(mEnemy != null && !mEnemy.mDead)
        {
            transform.position += Time.deltaTime * mSpeed * (mEnemy.transform.position - transform.position);
        }
        if(mEnemy != null && mEnemy.mDead)
        {
            mPlaced = true;
            gameObject.SetActive(false);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(mCollected)
        {
            return;
        }

        mCollected = true;
        CollectibleManager.AddLight(this);
        gameObject.SetActive(false);
    }

    public void PlaceLight(List<EnemyRangeDetector> pEnemiesAfterPlayer)
    {
        EnemyRangeDetector aClosestEnemy = null;
        float aDist = float.MaxValue;
        foreach(EnemyRangeDetector aEnemy in pEnemiesAfterPlayer)
        {
            float aDistance = (aEnemy.transform.position - aEnemy.mPlayer.transform.position).sqrMagnitude;
            if(aDistance < aDist)
            {
                aClosestEnemy = aEnemy;
            }
        }

        mEnemy = aClosestEnemy;
        transform.position = mEnemy.mPlayer.transform.position + (mEnemy.transform.position - mEnemy.mPlayer.transform.position).normalized * 2.0f;
        gameObject.SetActive(true);

    }

}
