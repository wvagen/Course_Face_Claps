using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    public GameObject smallGrout;
    public Manager man;

    GameObject lastGroutCreated;
    Vector2 mousePos;
    bool isMouseDown = false;
    //bool isSmallGroutSpawned = true;

    void Update()
    {
        if (Manager.isPaused || Manager.isGameOver) return;
        if (Input.GetMouseButtonDown(0))
        {
            OnDown();

        }
        if (Input.GetMouseButtonUp(0)) OnUp();
        if (isMouseDown) followMyMouse();
    }

    void OnDown()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastGroutCreated = Instantiate(smallGrout, mousePos, Quaternion.identity);
            lastGroutCreated.GetComponent<TrailRenderer>().startWidth *= 1.5f;
            lastGroutCreated.GetComponent<TrailRenderer>().endWidth *= 1.5f;
        Manager.orderInLayer++;
        lastGroutCreated.GetComponent<TrailRenderer>().sortingOrder = Manager.orderInLayer;
        lastGroutCreated.GetComponent<SpriteRenderer>().sortingOrder = Manager.orderInLayer;
        isMouseDown = true;
    }

    void OnUp()
    {
        if (lastGroutCreated != null)
        {
            if (lastGroutCreated.GetComponent<GroutLogic>().iDidWell)
                man.boostCombot();
            else
                man.resetCombo();

            lastGroutCreated.GetComponent<SpriteRenderer>().enabled = false;
            lastGroutCreated = null;
        }
            isMouseDown = false;

    }

    void followMyMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (lastGroutCreated != null)
        lastGroutCreated.transform.position = mousePos;
    }


}
