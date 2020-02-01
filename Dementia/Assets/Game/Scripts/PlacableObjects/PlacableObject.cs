using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableObject : MonoBehaviour
{
    public List<Transform> snapingPoints;
    public List<Vector3> wayPoints;
    public GameObject spPrefab;

    public bool mUseEditor = false;

}

