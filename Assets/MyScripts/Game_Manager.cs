using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public Animator myAnim;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI finalScoreTxt, bestScoreTxt;

    public GameObject hand;
    public GameObject target;
    public Transform targetParent;

    public Audio_Manager audioMan;

    GameObject spawnedHand;

    float timer, nextSpawmTime;

    int score = 0;

    float spawnRate = 3;
    float phaseDuration = 1;

    private void Start()
    {
        Time.timeScale = 1;
        nextSpawmTime += spawnRate;
        audioMan = FindObjectOfType<Audio_Manager>();
    }

    private void Update()
    {
        Get_Input();
        Spawn_Target();
    }


    void Spawn_Target()
    {
        timer += Time.deltaTime;
        if (timer >= nextSpawmTime)
        {
            nextSpawmTime += spawnRate;
            GameObject tempTarget = Instantiate(target, new Vector2(Random.Range(-7f, 7f), Random.Range(-3f, 3f)), Quaternion.identity, targetParent);
            tempTarget.GetComponent<Face>().Set_Me(phaseDuration, this);
            Difficulty_Manager();
        }
    }

    void Get_Input()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Spawn_Hand();
        }
    }

    void Difficulty_Manager()
    {
        if (spawnRate > 0.3f)
        {
            spawnRate -= 0.1f;
        }

        if (phaseDuration > 0.2f)
        {
            phaseDuration -= 0.05f;
        }
    }

    public void Incr_Score()
    {
        score += 10;
        scoreTxt.text = score.ToString();
    }

    public void Game_Over()
    {
        finalScoreTxt.text = scoreTxt.text;
        myAnim.Play("Game_Over");
        int previousBestScore = PlayerPrefs.GetInt("best_score", 0);
        int bestScore = score > previousBestScore ? score : previousBestScore;
        bestScoreTxt.text = bestScore.ToString();
        PlayerPrefs.SetInt("best_score", bestScore);
    }

    public void Load_Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Spawn_Hand()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        spawnedHand = Instantiate(hand, mousePosition, Quaternion.identity,targetParent);
        spawnedHand.GetComponent<Hand>().Set_GameMan(this);
        audioMan.Play_Tahwida_SFX();
    }

}
