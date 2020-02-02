using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    public bool mWin = false;
    
    void OnTriggerEnter(Collider other)
    {
        PlayerMovement aPlayer = other.GetComponent<PlayerMovement>();
        if(aPlayer != null)
        {
            mWin = true;
        }
    }
}
