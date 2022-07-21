using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FirstScene : MonoBehaviour
{
    bool isSceneLoaded = false;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSceneLoaded)
            MoveToNextScene();
    }

    public void MoveToNextScene()
    {
        isSceneLoaded = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
