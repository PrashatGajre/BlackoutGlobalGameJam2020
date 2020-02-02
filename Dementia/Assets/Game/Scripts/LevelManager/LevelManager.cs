using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class LevelManager : MonoBehaviour
{
    public float mRadius = 3.0f;
    public float mDistance = 5.0f;

    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static LevelManager m_Instance;

    public List<LightObject> lightObjects;
    public PlayerWin playerWin;
    public List<EnemyRangeDetector> enemyRangeDetectors;
    public List<PathBlock> pathBlocks;
    public SkyboxShader skybox;

    //public CinemachineTargetGroup mTargetGroup;

    public bool mPlay = true;
    public bool mTutDone = false;
    public GameObject mSpaceTut;
    /// <summary>
    /// Access LevelManager instance through this propriety.
    /// </summary>
    public static LevelManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance == null)
        {// Search for existing instance.
            m_Instance = (LevelManager)FindObjectOfType(typeof(LevelManager));

            // Create new instance if one doesn't already exist.
            if (m_Instance == null)
            {
                // Need to create a new GameObject to attach the singleton to.
                var singletonObject = new GameObject();
                m_Instance = singletonObject.AddComponent<LevelManager>();
                singletonObject.name = typeof(LevelManager).ToString() + " (Singleton)";

                // Make instance persistent.
                //DontDestroyOnLoad(singletonObject);
            }
        }
    }

    public void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }

    private void Start()
    {
        lightObjects = new List<LightObject>(GameObject.FindObjectsOfType<LightObject>());
        enemyRangeDetectors = new List<EnemyRangeDetector>(GameObject.FindObjectsOfType<EnemyRangeDetector>());
        pathBlocks = new List<PathBlock>(GameObject.FindObjectsOfType<PathBlock>());
        playerWin = GameObject.FindObjectOfType<PlayerWin>();
        skybox = GameObject.FindObjectOfType<SkyboxShader>();
    }

    private void Update()
    {
        foreach (Connection aConnection in PathBlock.mActiveConnections.Values)
        {
            aConnection.mSelfBlockSnapPoint.gameObject.GetComponent<MeshRenderer>().enabled = true;
            aConnection.mOtherBlockSnapPoint.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }


    //void FixedUpdate()
    //{
    //    RaycastHit[] aHit = Physics.SphereCastAll(WaypointManager.GetPlayer().transform.position, mRadius, WaypointManager.GetPlayer().transform.forward, mDistance);
    //    if (aHit.Length > 0)
    //    {
    //        List<CinemachineTargetGroup.Target> aTargets = new List<CinemachineTargetGroup.Target>(mTargetGroup.m_Targets);
    //        List<Transform> aTransforms = new List<Transform>();
    //        List<Transform> aColTransforms = new List<Transform>();
    //        List<CinemachineTargetGroup.Target> aRemovals = new List<CinemachineTargetGroup.Target>();
    //        foreach(var aTarget in aTargets)
    //        {
    //            aTransforms.Add(aTarget.target);
    //        }
    //        foreach(RaycastHit aCol in aHit)
    //        {
    //            aColTransforms.Add(aCol.collider.transform);
    //            if(!aTransforms.Contains(aCol.collider.transform))
    //            {
    //                aTransforms.Add(aCol.collider.transform);
    //                CinemachineTargetGroup.Target aNewTarget = new CinemachineTargetGroup.Target();
    //                aNewTarget.target = aCol.collider.transform;
    //                aNewTarget.radius = 2.0f;
    //                aNewTarget.weight = 1.5f;
    //                aTargets.Add(aNewTarget);
    //            }
    //        }
    //        foreach(var aTarget in aTargets)
    //        {
    //            if(aTarget.target == null)
    //            {
    //                aRemovals.Add(aTarget);
    //                continue;
    //            }
    //            if(!aColTransforms.Contains(aTarget.target))
    //            {
    //                if(aTarget.target.gameObject.GetComponent<PlayerMovement>() == null)
    //                {
    //                    if(aTarget.target.gameObject.GetComponent<EnemyRangeDetector>() == null)
    //                    {
    //                        aRemovals.Add(aTarget);
    //                    }
    //                }
    //            }
    //        }
    //        foreach(var aTarget in aRemovals)
    //        {
    //            aTargets.Remove(aTarget);
    //        }
    //        mTargetGroup.m_Targets = aTargets.ToArray();
    //    }
    //}

    public void OnEnemyAlarmOn(Transform pTarget)
    {
        skybox.Alarm();
        //CinemachineTargetGroup.Target aTarget = new CinemachineTargetGroup.Target();
        //aTarget.target = pTarget;
        //aTarget.weight = 2;
        //aTarget.radius = 2;
        //CinemachineTargetGroup.Target[] aTargets = new CinemachineTargetGroup.Target[mTargetGroup.m_Targets.Length + 1];
        //int aI = 0;
        //foreach(CinemachineTargetGroup.Target aTargetG in mTargetGroup.m_Targets)
        //{
        //    aTargets[aI] = aTargetG;
        //    aI++;
        //}
        //aTargets[aI] = aTarget;
        //mTargetGroup.m_Targets = aTargets;
    }
    public void OnEnemyAlarmOff(Transform pTarget)
    {
        skybox.Ambient();
        //CinemachineTargetGroup.Target[] aTargets = new CinemachineTargetGroup.Target[mTargetGroup.m_Targets.Length - 1];
        //int aI = 0;
        //foreach (CinemachineTargetGroup.Target aTargetG in mTargetGroup.m_Targets)
        //{
        //    if(aTargetG.target != pTarget)
        //    {
        //        aTargets[aI] = aTargetG;
        //        aI++;
        //    }
        //}
        //mTargetGroup.m_Targets = aTargets;
    }

    private void OnApplicationQuit()
    {
        m_ShuttingDown = true;
    }


    private void OnDestroy()
    {
        m_ShuttingDown = true;
    }


    public void StartTutorial()
    {
        mPlay = false;
        mSpaceTut.SetActive(true);
    }

    public void StopTutorial()
    {
        mPlay = true;
        mTutDone = true;
        mSpaceTut.SetActive(false);
    }


}
