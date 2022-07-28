using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Animator myAnim; 
    Game_Manager myGameMan;

    List<Vector2> handPositions = new List<Vector2>();

    bool isHandAboutToDestory = false;

    Collider2D previousCollision;

    public void Set_GameMan(Game_Manager myGameMan)
    {
        this.myGameMan = myGameMan; 
    }

    void Update()
    {

        Follow_My_Mouse();
        if (Input.GetMouseButtonUp(0))
        {
            Release();
        }
    }

    void Follow_My_Mouse()
    {
        if (!isHandAboutToDestory)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
            handPositions.Add(mousePosition);
            if (handPositions.Count > 1)
            {
                if (handPositions[handPositions.Count - 1].x > handPositions[handPositions.Count - 2].x)
                {
                    transform.localScale = new Vector2(-1, 1);
                }
                else if (handPositions[handPositions.Count - 1].x < handPositions[handPositions.Count - 2].x)
                {
                    transform.localScale *= new Vector2(1, 1);
                }
                handPositions.RemoveAt(0);
            }
        }
    }

    public void Release()
    {
        isHandAboutToDestory = true;
        Destroy(gameObject, 0.3f);
        myAnim.SetBool("isClapping", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Target")
        {
            if (previousCollision == null || previousCollision != collision)
            myGameMan.Incr_Score();

            collision.GetComponent<Face>().Klit_Sorfak(transform.localScale.x == 1);
            previousCollision = collision;

            myGameMan.audioMan.Play_Slap_SFX();
        }
    }
}
