using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject hole;
    public Transform xLimit, yLimit;

    public GameObject pausePanel;
    public GameObject musicCutterImg, sfxCutterImg;

    public Text comboTxt, motvationalWordTxt,scoreTxt;
    public Text finalScoreTxt, finalBestScoreTxt;
    public string[] motivationWordsSet;
    public Color[] comboColors;
    public Animator canvasAnim;

    Color initComboAndMotivationColor;
    public float crackPeriodToChangeStatus = 2f;


    public List<GameObject> holesCreated = new List<GameObject>();
    public List<GameObject> waterFalls = new List<GameObject>();

    public static int orderInLayer = 0;
    public static bool isMusicOn = true, isSfxOn = true,isPaused = false;

    public static bool isGameOver = false;

    int score = 0;
    int scoreAmountToBeAdded = 5;
    bool isGameOverInvokedFromScoreIncrementing = false;
    short comboAmount = 1;
    short whenToStartCoutingCombo = 0;

    const float minimumDistanceBetweenHoles = 1f;
    void Start()
    {
        isPaused = false;
        isGameOver = false;
        InvokeRepeating("SpawnAHole", 0, crackPeriodToChangeStatus);
        initComboAndMotivationColor = scoreTxt.color;
    }

    



    void SpawnAHole()
    {
        if (isGameOver){
             foreach (GameObject g in holesCreated) Destroy(g);
             foreach (GameObject item in waterFalls)
             {
                 if (item != null) Destroy(item);
             }
            return;

        }
       
        Vector2 randomSpot = Vector2.zero ;
        do {
            randomSpot = new Vector2(Random.Range(-xLimit.position.x,xLimit.position.x),Random.Range(-yLimit.position.y,yLimit.position.y));
        }while (isPositionNearAnotherHole(randomSpot));

        Vector3 randomRotation = Vector3.zero;
        randomRotation.z = Random.Range(0, 360);
        GameObject tempHole = Instantiate(hole, randomSpot,Quaternion.Euler (randomRotation));
        tempHole.GetComponent<Crack>().man = this;
        tempHole.GetComponent<Crack>().periodToChangeStats = crackPeriodToChangeStatus;
        orderInLayer++;
        holesCreated.Add(tempHole);
    }
    bool isPositionNearAnotherHole(Vector2 randomSpot)
    {
        foreach( GameObject h in holesCreated){
            if (Vector2.Distance(randomSpot,h.transform.position) < minimumDistanceBetweenHoles) return true;
        }
        return false;
    }

    public void GameOver()
    {
        canvasAnim.SetBool("gameOver",true);
        isGameOver = true;
        if (score > PlayerPrefs.GetInt("bestScore", 0))
        {
            finalBestScoreTxt.text = "Best Score:" +score.ToString();
            PlayerPrefs.SetInt("bestScore", score);
        }else{
            finalBestScoreTxt.text = "Best Score:" + PlayerPrefs.GetInt("bestScore", 0).ToString();
        }
        finalScoreTxt.text = "Score: "+ score.ToString();
        GetComponent<AudioSource>().enabled = false;
    }


    public void incrementScore()
    {
        score += scoreAmountToBeAdded * comboAmount;
        scoreTxt.text = score.ToString();
        canvasAnim.Play("ScoreBumb", 0, 0);
        if (crackPeriodToChangeStatus > 0.1f)
        crackPeriodToChangeStatus -= 0.02f;
        whenToStartCoutingCombo++;
        if (isGameOver && !isGameOverInvokedFromScoreIncrementing)
        {
            GameOver();
            isGameOverInvokedFromScoreIncrementing = true;
        }
    }

    public void boostCombot()
    {
        if (whenToStartCoutingCombo < 100) return;
        if (comboAmount < (comboColors.Length - 1))
        comboAmount++;
        comboTxt.text = "Combo x" + comboAmount.ToString();
        comboTxt.color = comboColors[comboAmount - 2];
        scoreTxt.color = comboColors[comboAmount - 2];
        motvationalWordTxt.color = comboColors[comboAmount - 2];
        motvationalWordTxt.text = motivationWordsSet[Random.Range(0, motivationWordsSet.Length)];
        canvasAnim.Play("MotivationalWord", 1, 0);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        isPaused = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Home()
    {
        Time.timeScale = 1;
        isPaused = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        isPaused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        isPaused = false;
    }

    public void MusicBtn()
    {
        if (isMusicOn)
        {
            isMusicOn = false;
            musicCutterImg.SetActive(true);
            GetComponent<AudioSource>().enabled = false;
        }
        else{
            isMusicOn = true;
            musicCutterImg.SetActive(false);
            GetComponent<AudioSource>().enabled = true;
        }
    }
    public void SfxBtn()
    {
        if (isSfxOn)
        {
            isSfxOn = false;
            sfxCutterImg.SetActive(true);
        }
        else
        {
            isSfxOn = true;
            sfxCutterImg.SetActive(false);
        }
    }

    public void resetCombo()
    {
        whenToStartCoutingCombo = 0;
        comboAmount = 1;
        comboTxt.color = initComboAndMotivationColor;
        motvationalWordTxt.color = initComboAndMotivationColor;
        scoreTxt.color = initComboAndMotivationColor;
    }
    

}
