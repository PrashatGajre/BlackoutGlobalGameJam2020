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

    public void PlaceLight(Vector3 pPosition)
    {
        transform.position = pPosition;
        gameObject.SetActive(true);
        //get enemy from level manager
    }

}
