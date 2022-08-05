using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Face : MonoBehaviour
{

    public Image currentImg; 
    public Sprite[] faceLevels;

    bool isAlive = true;

    Game_Manager gameMan;
    float timer, phaseDuration;
    int phaseCount = 0;

    private void Start()
    {
        LoadFaces();
        currentImg.sprite = faceLevels[phaseCount];
    }

    private void LoadFaces()
    {
        for (int i = 0; i < Constants.PHOTOS_LENGTH_AMOUNT; i++)
        {
            string selfiePath = Path.Combine(Constants.SELFIE_PATH, Constants.SELFIE_PRE_NAME + i + Constants.SELFIE_EXTENSION);
            if (File.Exists(selfiePath))
            {
                byte[] data = File.ReadAllBytes(selfiePath);
                Texture2D tex = new Texture2D(20, 20);
                tex.LoadImage(data);
                faceLevels[i] = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 250);
            }
        }
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
            gameMan.audioMan.Play_Crack_Sounds(phaseCount);
            timer = 0;
            phaseCount++;
            transform.localScale += Vector3.one * 0.3f;
            currentImg.sprite = faceLevels[phaseCount];
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
        currentImg.sprite = faceLevels[faceLevels.Length - 1];
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
