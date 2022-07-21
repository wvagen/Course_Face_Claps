using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroutLogic : MonoBehaviour
{
    public bool iDidWell = false;

    Transform holePos = null;

    float distance;
    TrailRenderer myTrail;
    SpriteRenderer mySprite;
    Vector2 firstPos;
    bool weCanTalkNow = false;
    bool isCalculating = false;

    const float minDistanceToConsider = 0.1f;

    bool isFlip = true;
    void Start()
    {
        myTrail = GetComponent<TrailRenderer>();
        mySprite = GetComponent<SpriteRenderer>();
        StartCoroutine(fadeMe());
        myTrail.enabled = false;
        firstPos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "crack" || col.gameObject.tag == "bigCrack") && !Manager.isGameOver)
        {
            holePos = col.transform;
        }
    }

    void Update()
    {
        if (!weCanTalkNow && Vector2.Distance(firstPos,transform.position) > minDistanceToConsider)
        {
            if (transform.position.x > firstPos.x)
            {
                isFlip = true;
                mySprite.flipX = true;
            }
            else
            {
                isFlip = false;
            }
            weCanTalkNow = true;
            myTrail.enabled = true;
        }
        if (weCanTalkNow && holePos != null && !isCalculating)
        {
            QuickMaths();
        }
    }
    IEnumerator fadeMe()
    {
        Color colWhite = Color.white;
        while (colWhite.a > 0)
        {
            colWhite.a -= Time.deltaTime;
            mySprite.color = colWhite;
            yield return new WaitForEndOfFrame();
        }
        Destroy(this.gameObject);

    }

    void QuickMaths()
    {
       /* distance = Vector2.Distance(transform.position, holePos.position);
        if (distance < (holePos.transform.localScale.x / 2) * Mathf.Sqrt(2))
        {*/
        isCalculating = true;
        holePos.GetComponent<Crack>().fadeAnimation();
        iDidWell = true;
        holePos.GetComponent<Crack>().endLoop(isFlip);
        isCalculating = false;
       // }
    }

}
