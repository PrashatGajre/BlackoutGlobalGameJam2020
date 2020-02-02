using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static LevelManager m_Instance;

    public List<LightObject> lightObjects;
    public PlayerWin playerWin;
    public List<EnemyRangeDetector> enemyRangeDetectors;
    public List<PathBlock> pathBlocks;
    public SkyboxShader skybox;

    public bool mPlay = true;
    public bool mTutDone = false;
    /// <summary>
    /// Access LevelManager instance through this propriety.
    /// </summary>
    public static LevelManager Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(LevelManager) +
                    "' already destroyed. Returning null.");
                return null;
            }

            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
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

                return m_Instance;
            }
        }
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
        //ui
    }

    public void StopTutorial()
    {
        mPlay = true;
        mTutDone = true;
    }


}
