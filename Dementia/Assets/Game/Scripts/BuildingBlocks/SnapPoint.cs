using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class SnapPoint : MonoBehaviour
{
    [HideInInspector]
    public PathBlock mParentBlock;

    void Start()
    {
        Init();
    }

    void Init()
    {
        Transform aParent = transform.parent;
        if (aParent == null)
        {
            Debug.LogErrorFormat("There is no parent to snap point : {0}", this.gameObject.name);
            return;
        }
        mParentBlock = aParent.gameObject.GetComponent<PathBlock>();
        if (mParentBlock == null)
        {
            Debug.LogWarningFormat("The parent doesn't have path block script attached Parent: {0}", aParent.gameObject.name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if(!mParentBlock.mIsMoving)
        {
            return;
        }
        if(!WaypointManager.CanMakeConnection(mParentBlock.GetInstanceID()))
        {
            return;
        }
        SnapPoint aSP = other.GetComponent<SnapPoint>();
        if (aSP != null)
        {
            if (aSP.mParentBlock.GetInstanceID() == mParentBlock.GetInstanceID())
            {
                return;
            }
            if(!WaypointManager.CanMakeConnection(aSP.mParentBlock.GetInstanceID()))
            {
                return;
            }
            if(!mParentBlock.mActiveClosestPoints.ContainsKey(this.GetInstanceID()))
            {
                mParentBlock.mActiveClosestPoints.Add(this.GetInstanceID(), new Connection(this, aSP));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(!mParentBlock.mIsMoving)
        {
            return;
        }

        SnapPoint aSP = other.GetComponent<SnapPoint>();
        if(aSP != null)
        {
            if (aSP.mParentBlock.GetInstanceID() == mParentBlock.GetInstanceID())
            {
                return;
            }
            if(mParentBlock.mCurrentConnection.mSelfBlockSnapPoint == this)
            {
                WaypointManager.RemovePreviousConnection(mParentBlock.mCurrentConnection);
            }
            else if(mParentBlock.mActiveClosestPoints.ContainsKey(this.GetInstanceID()))
            {
                mParentBlock.mActiveClosestPoints.Remove(this.GetInstanceID());
            }
        }
    }

}
