using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{
    public GameObject waterFall;
    public GameObject explosionEffect;
    public Transform waterFallPosition;
    public Manager man;
    public AudioClip slapSoundEffect,crackSoundEffect,hallaM3akSoundEffect;

    public float periodToChangeStats;

    AudioSource myAudioSource;

    
    GameObject tempWaterFall;
    Animator myAnim;
    short animationStep = 0;

    IEnumerator couroutine;

    bool isHoleCovered = false;
    bool clapSoundPlayed = false;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        couroutine = changeMyStatus();
        StartCoroutine(couroutine);
        myAudioSource = GetComponent<AudioSource>();
    }

    IEnumerator changeMyStatus()
    {
        yield return new WaitForSeconds(periodToChangeStats);
        animationStep++;
        if (Manager.isSfxOn)
        myAudioSource.PlayOneShot(crackSoundEffect);
        if (!isHoleCovered) myAnim.SetInteger("crackStatus",animationStep);
        yield return new WaitForSeconds(periodToChangeStats);
        animationStep++;
       if (!isHoleCovered) myAnim.SetInteger("crackStatus", animationStep);
        yield return new WaitForSeconds(periodToChangeStats * 3);
        CrackLevel3();
        yield return new WaitForSeconds(periodToChangeStats * 3);
        GameOver();
    }

    void CrackLevel3()
    {
        if (Manager.isSfxOn) myAudioSource.PlayOneShot(hallaM3akSoundEffect);
     Destroy(Instantiate(explosionEffect, transform.position, Quaternion.identity),3);
         tempWaterFall.transform.localScale *= 2;
         transform.localScale *= 2;
         tempWaterFall.GetComponentInChildren<Animator>().Play("LoopWater", 0, 0);    
     this.gameObject.tag = "bigCrack";
    }

    void GameOver()
    {
        GameObject tempExplostion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        tempWaterFall.transform.localScale *= 5;
        Destroy(tempExplostion, 3);
        man.GameOver();
    }

    public void spawnWaterFall() 
    {
      tempWaterFall =  Instantiate(waterFall, waterFallPosition.position, Quaternion.identity);
      man.waterFalls.Add(tempWaterFall);
      Manager.orderInLayer++;
      tempWaterFall.GetComponentInChildren<SpriteRenderer>().sortingOrder = Manager.orderInLayer;

    }

    public void fadeAnimation()
    {
        man.incrementScore();
        StopCoroutine(couroutine);
        StartCoroutine(fade());
        if (animationStep > 1 && !clapSoundPlayed)
        {
            clapSoundPlayed = true;
            if (Manager.isSfxOn)
            myAudioSource.PlayOneShot(slapSoundEffect);
        }
    }

    IEnumerator fade()
    {
        GetComponent<Collider2D>().enabled = false;
        Color realCol = this.GetComponent<SpriteRenderer>().color;
        while (realCol.a > 0)
        {
            realCol.a -= Time.deltaTime ;
            this.GetComponent<SpriteRenderer>().color = realCol;
            yield return new WaitForEndOfFrame();
        }
        man.holesCreated.Remove(this.gameObject);
        Destroy(this.gameObject);
        if (tempWaterFall != null)
            Destroy(tempWaterFall);
    }

    public void endLoop(bool isFlip)
    {
        isHoleCovered = true;
        if (tempWaterFall != null)
        {
            tempWaterFall.GetComponentInChildren<Animator>().Play("endLoop");
            if (isFlip) tempWaterFall.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }
    }

}
