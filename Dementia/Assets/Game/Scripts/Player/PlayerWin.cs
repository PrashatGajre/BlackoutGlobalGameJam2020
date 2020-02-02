using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    public bool mWin = false;
    public GameObject mWinScreen;
    
    void OnTriggerEnter(Collider other)
    {
        PlayerMovement aPlayer = other.GetComponent<PlayerMovement>();
        if(aPlayer != null)
        {
            mWin = true;
            mWinScreen.SetActive(true);
        }
    }
}
