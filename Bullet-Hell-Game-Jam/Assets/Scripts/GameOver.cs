using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    SceneLoader sl;
    // Start is called before the first frame update
    void Start()
    {
        sl = FindObjectOfType<SceneLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey) {
            sl.LoadStartScene();
        }
    }
}
