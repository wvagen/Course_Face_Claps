using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
public class MainMenuSceneManager : MonoBehaviour
{
    public Animator myAnim;
    public Text bestScoreTxt;
    bool isGameEntered = false;
    void Start()
    {
        bestScoreTxt.text = "Best Score:" + PlayerPrefs.GetInt("bestScore",0);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameEntered)
        {
            isGameEntered = true; 
            myAnim.Play("GameStartAnimation");
        }
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
