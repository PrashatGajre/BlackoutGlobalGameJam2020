using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]Transform pointTo;

    private void Start()
    {
        if (pointTo == null)
        {
            pointTo = LevelManager.Instance.playerWin.transform;
        }
    }

    void Update()
    {
        transform.LookAt(pointTo);
    }
}
