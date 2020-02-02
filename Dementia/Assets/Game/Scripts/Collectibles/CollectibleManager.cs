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
            if(WaypointManager.IsActivePathConnectedToEnemy())
            {
                LightObject aLight = mAvailableLights[0];
                mAvailableLights.RemoveAt(0);
                aLight.PlaceLight(WaypointManager.GetPlacingPoint());
                WaypointManager.RemoveEnemyConnection();
            }
        }
    }


    public static void AddLight(LightObject pLight)
    {
        mInstance.mAvailableLights.Add(pLight);
    }

}
