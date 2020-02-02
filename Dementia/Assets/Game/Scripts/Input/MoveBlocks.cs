using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlocks : MonoBehaviour
{
    #region MOVEMENT
    private Transform parent;
    private MouseInput mouseInput;
    #endregion
    #region ROTATION
    private Quaternion rotation = Quaternion.identity;
    [Header("Movement/Rotation")]
    [SerializeField] private float rotationAngle = 15.0f;
    #endregion

    PathBlock pathBlock;

    void Start()
    {
        parent = transform;
        if (gameObject.GetComponent<MouseInput>() == null)
        {
            gameObject.AddComponent<MouseInput>();
        }
        mouseInput = gameObject.GetComponent<MouseInput>();
        pathBlock = gameObject.GetComponentInChildren<PathBlock>();
    }

    public void MoveBlock(Vector3 pPosition)
    {
        if (pPosition != Vector3.zero)
        {
            Destroy(GetComponent<AnimatePlacableObject>());
            pathBlock.mIsMoving = true;
            parent.position = new Vector3(pPosition.x, 0, pPosition.z);
        //pathBlock.mTrySnapping = true;
            parent.localRotation = rotation;
        }
    }

    public void RotateBlock()
    {
        pathBlock.mIsMoving = true;
        parent.Rotate(Vector3.up * rotationAngle);
        rotation = parent.localRotation;
        pathBlock.mTrySnapping = true;
        pathBlock.mIsMoving = false;
    }
    void Update()
    {
        if(pathBlock.mTraversed)
        {
            return;
        }
        if(pathBlock.mNextConnection.mOtherBlockSnapPoint !=null && pathBlock.mCurrentConnection.mOtherBlockSnapPoint != null)
        {
            return;
        }
        MoveBlock(mouseInput.MouseDragPosition());
        if (mouseInput.IsMouseRightUp())
        {
            RotateBlock();
        }
        if (mouseInput.IsMouseLeftUp())
        {
            pathBlock.mTrySnapping = true;
            pathBlock.mIsMoving = false;
        }
    }
}
