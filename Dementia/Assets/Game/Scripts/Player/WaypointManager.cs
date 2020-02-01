using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    static WaypointManager mInstance;
    void Awake()
    {
        if(mInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        mInstance = this;
    }

    Dictionary<int, List<PathBlock>> mActiveConnections = new Dictionary<int, List<PathBlock>>();
    PathBlock mActivePathBlock = null;


    public static bool CanMakeConnection(int pPathBlockInstance)
    {
        if(mInstance.mActiveConnections.ContainsKey(pPathBlockInstance))
        {
            return !(mInstance.mActiveConnections[pPathBlockInstance].Count >= 2);
        }
        return true;
    }

    public static void AddNewConnection(Connection pConnection)
    {
        int aSelfConId = pConnection.mSelfBlockSnapPoint.GetInstanceID();
        int aOthConId = pConnection.mOtherBlockSnapPoint.GetInstanceID();
        if (!mInstance.mActiveConnections.ContainsKey(aSelfConId))
        {
            mInstance.mActiveConnections.Add(aSelfConId, new List<PathBlock>());
        }
        if(!mInstance.mActiveConnections.ContainsKey(aOthConId))
        {
            mInstance.mActiveConnections.Add(aOthConId, new List<PathBlock>());
        }

        mInstance.mActiveConnections[aSelfConId].Add(pConnection.mOtherBlockSnapPoint.mParentBlock);
        mInstance.mActiveConnections[aOthConId].Add(pConnection.mSelfBlockSnapPoint.mParentBlock);

    }

    public static void RemovePreviousConnection(Connection pConnection)
    {
        int aSelfConId = pConnection.mSelfBlockSnapPoint.GetInstanceID();
        int aOthConId = pConnection.mOtherBlockSnapPoint.GetInstanceID();

        if (mInstance.mActiveConnections.ContainsKey(aSelfConId))
        {
            if(mInstance.mActiveConnections[aSelfConId].Contains(pConnection.mOtherBlockSnapPoint.mParentBlock))
            {
                mInstance.mActiveConnections[aSelfConId].Remove(pConnection.mOtherBlockSnapPoint.mParentBlock);
            }
        }
        if (mInstance.mActiveConnections.ContainsKey(aOthConId))
        {
            if(mInstance.mActiveConnections[aOthConId].Contains(pConnection.mSelfBlockSnapPoint.mParentBlock))
            {
                mInstance.mActiveConnections[aOthConId].Remove(pConnection.mSelfBlockSnapPoint.mParentBlock);
            }
        }

    }

    public static void SetActivePathblock(PathBlock pBlock)
    {
        if(mInstance.mActivePathBlock != null)
        {
            mInstance.mActivePathBlock.mTraversed = true;
        }
        mInstance.mActivePathBlock = pBlock;
    }

    public static PlayerWaypoint GetNextWaypoint(bool pNoCurrent, Vector3 pRefPos)
    {
        
        
        return null;
    }

    public static void GetPlayerFinalPosition(ref Vector3 pFinalPosition)
    {
        if(mInstance.mActiveConnections.ContainsKey(mInstance.mActivePathBlock.GetInstanceID()))
        {
            foreach(PathBlock aBlock in mInstance.mActiveConnections[mInstance.mActivePathBlock.GetInstanceID()])
            {
                if(!aBlock.mTraversed)
                {
                    pFinalPosition = aBlock.mCurrentConnection.mSelfBlockSnapPoint.transform.position;
                    return;
                }
            }
        }
    }


}
