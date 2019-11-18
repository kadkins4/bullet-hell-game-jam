using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    MusicPlayer mp;
    SceneLoader sl;

    void Awake() {
        mp = FindObjectOfType<MusicPlayer>();
        sl = FindObjectOfType<SceneLoader>();
    }

    private void Start() {
        mp.SetAndPlayMusicTrack(MusicTracks.Menu, .5f);
    }

    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            sl.LoadNextScene();
        }
    }
}
