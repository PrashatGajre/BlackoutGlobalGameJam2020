using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public string music = "MainMusic";

    private void Start()
    {
        AudioManager.MusicPlay(music, true);
    }
}
