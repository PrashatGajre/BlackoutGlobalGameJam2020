using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    static CollectibleManager mInstance;

    void Awake()
    {
        if(mInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        mInstance = this;
    }

    List<LightObject> mAvailableLights = new List<LightObject>();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(mAvailableLights.Count <= 0)
            {
                return;
            }
            List<EnemyRangeDetector> aEnemiesAfterPlayer = new List<EnemyRangeDetector>();
            foreach(EnemyRangeDetector aEnemies in LevelManager.Instance.enemyRangeDetectors)
            {
                if(aEnemies.mPlayer != null)
                {
                    aEnemiesAfterPlayer.Add(aEnemies);
                }
            }

            if(aEnemiesAfterPlayer.Count > 0)
            {
                LightObject aObject = mAvailableLights[0];
                mAvailableLights.RemoveAt(0);
                aObject.PlaceLight(aEnemiesAfterPlayer);
            }
        }
    }


    public static void AddLight(LightObject pLight)
    {
        mInstance.mAvailableLights.Add(pLight);
    }

}
