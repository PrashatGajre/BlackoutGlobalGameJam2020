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

    List<Light> mAvailableLights = new List<Light>();

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(mAvailableLights.Count <= 0)
            {
                return;
            }
            if(WaypointManager.IsActivePathConnectedToEnemy())
            {
                Light aLight = mAvailableLights[0];
                mAvailableLights.RemoveAt(0);
                aLight.PlaceLight(WaypointManager.GetPlacingPoint());
                WaypointManager.RemoveEnemyConnection();
            }
        }
    }


    public static void AddLight(Light pLight)
    {
        mInstance.mAvailableLights.Add(pLight);
    }

}
