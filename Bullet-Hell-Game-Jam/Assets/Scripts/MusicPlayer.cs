using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioClip currentSong;

    [Header("Tracks")]
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameOverMusic;

    [Header("General")]
    [SerializeField] [Range(0f,1f)] float musicVolume = .5f;

    void Awake() {
        SetupSingleton();
    }

    void Start() {
        SetAndPlayMusicTrack(MusicTracks.Background);
    }

    void SetupSingleton() {
        int musicPlayerCount = FindObjectsOfType<MusicPlayer>().Length;

        if (musicPlayerCount > 1) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetAndPlayMusicTrack(MusicTracks type, float vol = 1f) {
        if (vol > 1f || vol < 0f) {
            musicVolume = 1f;
        }

        musicVolume = vol;

        switch (type) {
            case MusicTracks.Background:
                currentSong = backgroundMusic;
                break;
            case MusicTracks.Menu:
                currentSong = menuMusic;
                break;
            case MusicTracks.GameOver:
                currentSong = gameOverMusic;
                break;
        }

        PlayMusic();
    }

    private void PlayMusic() {
        AudioSource.PlayClipAtPoint(
            currentSong,
            Camera.main.transform.position,
            musicVolume
        );
    }
}
