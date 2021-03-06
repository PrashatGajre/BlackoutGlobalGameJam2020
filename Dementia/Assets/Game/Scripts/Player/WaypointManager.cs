﻿using System.Collections;
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
    PlayerMovement mPlayer = null;

    public static PlayerMovement GetPlayer()
    {
        return mInstance.mPlayer;
    }

    public static bool CanMakeConnection(int pPathBlockInstance)
    {
        if(mInstance.mActiveConnections.ContainsKey(pPathBlockInstance))
        {
            return !(mInstance.mActiveConnections[pPathBlockInstance].Count >= 2);
        }
        return true;
    }

    public static bool IsActivePathBlock(int pBlock)
    {
        if(mInstance.mActivePathBlock == null)
        {
            return false;
        }
        return mInstance.mActivePathBlock.GetInstanceID() == pBlock;
    }

    public static void AddNewConnection(Connection pConnection)
    {
        int aSelfConId = pConnection.mSelfBlockSnapPoint.mParentBlock.GetInstanceID();
        int aOthConId = pConnection.mOtherBlockSnapPoint.mParentBlock.GetInstanceID();
        if (!mInstance.mActiveConnections.ContainsKey(aSelfConId))
        {
            mInstance.mActiveConnections.Add(aSelfConId, new List<PathBlock>());
        }
        if (!mInstance.mActiveConnections.ContainsKey(aOthConId))
        {
            mInstance.mActiveConnections.Add(aOthConId, new List<PathBlock>());
        }

        mInstance.mActiveConnections[aSelfConId].Add(pConnection.mOtherBlockSnapPoint.mParentBlock);
        mInstance.mActiveConnections[aOthConId].Add(pConnection.mSelfBlockSnapPoint.mParentBlock);
        pConnection.mOtherBlockSnapPoint.mParentBlock.mNextConnection = new Connection(pConnection.mOtherBlockSnapPoint, pConnection.mSelfBlockSnapPoint);
        if (mInstance.mActivePathBlock == pConnection.mOtherBlockSnapPoint.mParentBlock
            || mInstance.mActivePathBlock == pConnection.mSelfBlockSnapPoint.mParentBlock)
        {
            mInstance.mPlayer.ResetWaypoint();
        }

    }

    public static bool IsActivePathConnectedToEnemy()
    {
        if(mInstance.mActivePathBlock == null)
        {
            return false;
        }
        if(!mInstance.mActiveConnections.ContainsKey(mInstance.mActivePathBlock.GetInstanceID()))
        {
            return false;
        }
        if(mInstance.mActiveConnections[mInstance.mActivePathBlock.GetInstanceID()].Count <= 0)
        {
            return false;
        }
        
        return mInstance.GetEnemyConnection() != null;
    }

    PathBlock GetEnemyConnection()
    {
        foreach (PathBlock aBlock in mInstance.mActiveConnections[mInstance.mActivePathBlock.GetInstanceID()])
        {
            if (aBlock.mIsEnemy)
            {
                return aBlock;
            }
        }
        return null;
    }

    public static Vector3 GetPlacingPoint()
    {
        PathBlock aPBlock = mInstance.GetEnemyConnection();

        if(aPBlock == null)
        {
            return mInstance.mPlayer.transform.position;
        }

        return (aPBlock.mPlayerWaypoints[1].transform.position + 
            (mInstance.mActivePathBlock.mNextConnection.mOtherBlockSnapPoint.transform.position - 
            aPBlock.mPlayerWaypoints[1].transform.position) / 2);

    }

    public static void RemoveEnemyConnection()
    {
        RemovePreviousConnection(mInstance.mActivePathBlock.mNextConnection);
    }

    public static void RemovePreviousConnection(Connection pConnection)
    {
        int aSelfConId = pConnection.mSelfBlockSnapPoint.mParentBlock.GetInstanceID();
        int aOthConId = pConnection.mOtherBlockSnapPoint.mParentBlock.GetInstanceID();
        pConnection.mSelfBlockSnapPoint.mParentBlock.mCurrentConnection = new Connection(null, null);
        pConnection.mOtherBlockSnapPoint.mParentBlock.mNextConnection = new Connection(null, null);
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
        if(mInstance.mActivePathBlock == pBlock)
        {
            return;
        }
        if(mInstance.mActivePathBlock != null)
        {
            mInstance.mActivePathBlock.mTraversed = true;
        }
        mInstance.mActivePathBlock = pBlock;
    }

    public static void SetActivePlayer(PlayerMovement pPlayer)
    {
        mInstance.mPlayer = pPlayer;
    }

    int GetClosestWaypoint(Vector3 pRefPos)
    {
        int aClosestIx = 0;
        float aShortestDistance = float.MaxValue;
        for (int aI = 0; aI < mInstance.mActivePathBlock.mPlayerWaypoints.Length; aI++)
        {
            float aDist = (mInstance.mActivePathBlock.mPlayerWaypoints[aI].transform.position - pRefPos).sqrMagnitude;
            if (aDist < aShortestDistance)
            {
                aClosestIx = aI;
                aShortestDistance = aDist;
            }
        }
        return aClosestIx;
    }

    public static PlayerWaypoint GetNextWaypoint(PlayerWaypoint pCurrentWaypoint, PlayerWaypoint pPreviousWaypoint, Vector3 pRefPos)
    {
        if(pCurrentWaypoint == null)
        {
            return mInstance.mActivePathBlock.mPlayerWaypoints[mInstance.GetClosestWaypoint(pRefPos)];
        }
        else
        {
            if(mInstance.mActiveConnections.ContainsKey(mInstance.mActivePathBlock.GetInstanceID()))
            {
                foreach(PathBlock aBlock in mInstance.mActiveConnections[mInstance.mActivePathBlock.GetInstanceID()])
                {
                    if(!aBlock.mTraversed)
                    {
                        int aClosestPoint = mInstance.GetClosestWaypoint(mInstance.mActivePathBlock.mNextConnection.mOtherBlockSnapPoint.transform.position);
                        if(aClosestPoint > pCurrentWaypoint.mWaypointIx && pCurrentWaypoint.mWaypointIx + 1 < mInstance.mActivePathBlock.mPlayerWaypoints.Length)
                        {
                            return mInstance.mActivePathBlock.mPlayerWaypoints[pCurrentWaypoint.mWaypointIx + 1];
                        }
                        else if(aClosestPoint < pCurrentWaypoint.mWaypointIx && pCurrentWaypoint.mWaypointIx-1 > -1)
                        {
                            return mInstance.mActivePathBlock.mPlayerWaypoints[pCurrentWaypoint.mWaypointIx - 1];
                        }
                        else 
                        {
                            return null;
                        }
                    }
                }
            }
            if(pPreviousWaypoint == null)
            {
                int aOneDirection = pCurrentWaypoint.mWaypointIx-1;
                int aSecond = pCurrentWaypoint.mWaypointIx+1;
                if(aOneDirection == -1 && aSecond < mInstance.mActivePathBlock.mPlayerWaypoints.Length)
                {
                    return mInstance.mActivePathBlock.mPlayerWaypoints[aSecond];
                }
                else if(aOneDirection >= 0 && aSecond >= mInstance.mActivePathBlock.mPlayerWaypoints.Length)
                {
                    return mInstance.mActivePathBlock.mPlayerWaypoints[aOneDirection];
                }
                else
                {
                    if(Random.value > 0.5f)
                    {
                        return mInstance.mActivePathBlock.mPlayerWaypoints[aSecond];
                    }
                    else
                    {
                        return mInstance.mActivePathBlock.mPlayerWaypoints[aOneDirection];
                    }
                }
            }
            else
            {
                if(pPreviousWaypoint.mWaypointIx < pCurrentWaypoint.mWaypointIx)
                {
                    if(mInstance.mActivePathBlock.mPlayerWaypoints.Length > pCurrentWaypoint.mWaypointIx+1)
                    {
                        return mInstance.mActivePathBlock.mPlayerWaypoints[pCurrentWaypoint.mWaypointIx + 1];
                    }
                }
                else
                {
                    if (-1 < pCurrentWaypoint.mWaypointIx - 1)
                    {
                        return mInstance.mActivePathBlock.mPlayerWaypoints[pCurrentWaypoint.mWaypointIx - 1];
                    }
                }
            }
        }
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
                    pFinalPosition = aBlock.mCurrentConnection.mSelfBlockSnapPoint.transform.position + 
                        (aBlock.mCurrentConnection.mSelfBlockSnapPoint.transform.position - mInstance.mPlayer.transform.position).normalized * 2.0f;
                    return;
                }
            }
        }
    }


}
