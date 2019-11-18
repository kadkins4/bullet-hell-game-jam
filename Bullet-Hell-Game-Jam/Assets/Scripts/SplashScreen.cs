using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SceneLoader>().LoadSplashScreen();
    }
}
