using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    Game_Manager myGameMan;

    public void Set_GameMan(Game_Manager myGameMan)
    {
        this.myGameMan = myGameMan; 
    }

    void Update()
    {
        Follow_My_Mouse();
    }
    void Follow_My_Mouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        transform.position = mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            myGameMan.Incr_Score();
            Destroy(collision.gameObject);
        }
    }
}
