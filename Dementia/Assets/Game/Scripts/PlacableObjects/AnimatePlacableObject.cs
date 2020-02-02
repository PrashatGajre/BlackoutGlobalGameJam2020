using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlacableObject : MonoBehaviour
{
    public float hoverSpeedMax = 3.0f;
    public float rotationSpeedMax;
    public float changeDirrectionTime;
    public float yPos = 4;

    private float hoverSpeed;
    private Vector3 rotationSpeed;

    private float counter = 0;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        hoverSpeed = Random.Range(0, hoverSpeedMax);
        rotationSpeed = new Vector3(Random.Range(-rotationSpeedMax, rotationSpeedMax), Random.Range(-rotationSpeedMax, rotationSpeedMax), Random.Range(-rotationSpeedMax, rotationSpeedMax));
    }

    void Update()
    {
        counter += Time.deltaTime;
        if(counter > changeDirrectionTime)
        {
            hoverSpeed *= -1;
            counter = 0;
        }
        transform.Translate(new Vector3(0, hoverSpeed * Time.deltaTime, 0));
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
