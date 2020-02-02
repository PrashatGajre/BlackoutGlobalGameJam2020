using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    public GameObject mGameOverPanel;

    private bool _dead = false;

    private void Start()
    {
        _dead = false;
    }

    void Update()
    {
        if(_dead)
        {
            return;
        }
        if(WaypointManager.GetPlayer().mDead)
        {
            mGameOverPanel.SetActive(true);
            AudioManager.PlaySFX("Player_Death");
            _dead = true;
        }
    }
}
