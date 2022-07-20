using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public GameObject hand;

    GameObject spawnedHand;
    private void Update()
    {
        Get_Input();
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

    void Spawn_Hand()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        spawnedHand = Instantiate(hand, mousePosition, Quaternion.identity);
    }

    void Destroy_Spawned_Hand()
    {
        if (spawnedHand != null)
        {
            Destroy(spawnedHand);
        }
    }

}
