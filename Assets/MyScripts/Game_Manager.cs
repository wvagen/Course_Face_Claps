using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreTxt;

    public GameObject hand;
    public GameObject target;

    GameObject spawnedHand;

    float timer,nextSpawmTime;

    int score = 0;

    const int SPAWN_RATE = 3;

    private void Start()
    {
        nextSpawmTime += SPAWN_RATE;
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
            nextSpawmTime += SPAWN_RATE;
            GameObject tempTarget = Instantiate(target, new Vector2(Random.Range(-5f, 5f), Random.Range(-4f, 4f)), Quaternion.identity);
            Destroy(tempTarget, 3);
        }
    }

    void Get_Input()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Spawn_Hand();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Destroy_Spawned_Hand();
        }
    }

    public void Incr_Score()
    {
        score += 10;
        scoreTxt.text = score.ToString();
    }

    void Spawn_Hand()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        spawnedHand = Instantiate(hand, mousePosition, Quaternion.identity);
        spawnedHand.GetComponent<Hand>().Set_GameMan(this);
    }

    void Destroy_Spawned_Hand()
    {
        if (spawnedHand != null)
        {
            Destroy(spawnedHand);
        }
    }

}
