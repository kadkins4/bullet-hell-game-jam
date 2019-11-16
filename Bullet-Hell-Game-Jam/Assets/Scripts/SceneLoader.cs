﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int waitAndLoad = 2;
    int splashScreenTime = 5;

    public void LoadSplashScreen () {
        StartCoroutine(ExitSplash());
    }

    IEnumerator ExitSplash() {
        yield return new WaitForSeconds(splashScreenTime);
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex += 1);
    }

    public void LoadStartScene() {
        SceneManager.LoadScene("Main Menu");
    }

    public void PlayGame() {
        SceneManager.LoadScene("Game");
    }

    IEnumerator LoadGameOver() {
        yield return new WaitForSeconds(waitAndLoad);
        SceneManager.LoadScene("Game Over");
    }

    public void TriggerGameOver() {
        StartCoroutine(LoadGameOver());
    }

    public void QuitGame() {
        Application.Quit();
    }
}
