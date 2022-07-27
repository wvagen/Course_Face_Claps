using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    public Sprite[] faceLevels;
    public SpriteRenderer currentSpriteRend;

    bool isAlive = true;

    Game_Manager gameMan;
    float timer,phaseDuration;
    int phaseCount = 0;

    private void Start()
    {
        currentSpriteRend.sprite = faceLevels[phaseCount];
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= phaseDuration)
        {
            Scale_Up();
        }
    }

    void Scale_Up()
    {
        if (phaseCount < faceLevels.Length - 2 && isAlive)
        {
            timer = 0;
            phaseCount++;
            transform.localScale += Vector3.one * 0.3f;
            currentSpriteRend.sprite = faceLevels[phaseCount];
        }
        else if (phaseCount == faceLevels.Length - 2)
        {
            Time.timeScale = 0;
            gameMan.Game_Over();
        }
    }

    public void Klit_Sorfak(bool isLookingLeft)
    {
        isAlive = false;
        currentSpriteRend.sprite = faceLevels[faceLevels.Length - 1];
        Vector2 currentScale = transform.localScale;
        currentScale.x *= isLookingLeft ? 1 : -1;
        transform.localScale = currentScale;
        Destroy(this.gameObject, .3f);
    }

    public void Set_Me(float phaseDuration, Game_Manager gameMan)
    {
        this.phaseDuration = phaseDuration;
        this.gameMan = gameMan;
    }

}
