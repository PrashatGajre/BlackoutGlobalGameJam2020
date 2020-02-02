using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosestLight : MonoBehaviour
{
    [SerializeField]Transform pointTo;
    //private LightObject[] LevelManager.Instance.lightObjects;
    Vector3 closestPosition = new Vector3(100,100,100);

    private void Start()
    {
        //LevelManager.Instance.lightObjects = GameObject.FindObjectsOfType<LightObject>();
        for (int i = 0; i < LevelManager.Instance.lightObjects.Count; i++)
        {
            if (Vector3.Distance(LevelManager.Instance.lightObjects[i].transform.position, transform.position) < Vector3.Distance(closestPosition,transform.position))
            {
                pointTo = LevelManager.Instance.lightObjects[i].transform;
            }
        }
    }

    void Update ()
    {
        closestPosition = new Vector3(100,100,100);
        for (int i = 0; i < LevelManager.Instance.lightObjects.Count; i++)
        {
            //Debug.Log(Vector3.Distance(LevelManager.Instance.lightObjects[i].transform.position, transform.position) + " < " + Vector3.Distance(closestPosition, transform.position));
            if (Vector3.Distance(LevelManager.Instance.lightObjects[i].transform.position, transform.position) < Vector3.Distance(closestPosition, transform.position))
            {
                pointTo = LevelManager.Instance.lightObjects[i].transform;
                closestPosition = pointTo.position;
            }
        }
        transform.LookAt(pointTo);
    }
}
