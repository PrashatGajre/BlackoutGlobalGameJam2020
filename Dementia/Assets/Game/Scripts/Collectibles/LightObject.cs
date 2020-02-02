using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    public bool mPlaced;
    public bool mCollected = false;


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
        mPlaced = true;
        transform.position = pPosition;
        gameObject.SetActive(true);
    }

}
