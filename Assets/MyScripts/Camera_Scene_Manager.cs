using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Camera_Scene_Manager : MonoBehaviour
{
    public WebCam webCam;
    public Renderer previewRend;

    public GameObject Previous_Btn, Next_Btn,Retry_Shot,Cam_Shot,Done_Btn;

    // Start is called before the first frame update
    void Start()
    {
        Update_Inputs();
    }

    void Update_Inputs()
    {
        Load_Selfie();
        Previous_Btn.SetActive(Check_Photo_Index_Existance(webCam.photoIndex - 1));
        Next_Btn.SetActive(Check_Photo_Index_Existance(webCam.photoIndex + 1) && !Done_Btn.activeSelf);
        Retry_Shot.SetActive(Check_Photo_Index_Existance(webCam.photoIndex));
        Cam_Shot.SetActive(!Check_Photo_Index_Existance(webCam.photoIndex));
        if (!Check_Photo_Index_Existance(webCam.photoIndex))
        {
            webCam.wct.Play();
        }
    }

    bool Check_Photo_Index_Existance(int photoIndex)
    {
        string selfiePath = Path.Combine(Constants.SELFIE_PATH, Constants.SELFIE_PRE_NAME + photoIndex + Constants.SELFIE_EXTENSION);
        return File.Exists(selfiePath);
    }

    public void Load_Selfie()
    {
        string selfiePath = Path.Combine(Constants.SELFIE_PATH ,Constants.SELFIE_PRE_NAME + webCam.photoIndex + Constants.SELFIE_EXTENSION);
        Debug.Log(selfiePath);
        if (File.Exists(selfiePath))
        {
            byte[] data = File.ReadAllBytes(selfiePath);
            Texture2D tex = new Texture2D(331, 331);
            tex.LoadImage(data);
            previewRend.material.mainTexture = tex;
        }
    }

    public void Cam_Btn_Click()
    {
        Done_Btn.SetActive(true);
        Update_Inputs();
    }

    public void Next_Btn_Click()
    {
        webCam.photoIndex++;
        Update_Inputs();
    }

    public void Previous_Btn_Click()
    {
        webCam.photoIndex--;
        Update_Inputs();
    }

    public void Retry_Btn_Click()
    {
        string selfiePath = Path.Combine(Constants.SELFIE_PATH, Constants.SELFIE_PRE_NAME + webCam.photoIndex + Constants.SELFIE_EXTENSION);
        Debug.Log(selfiePath);
        if (File.Exists(selfiePath))
        {
            File.Delete(selfiePath);
        }
        Update_Inputs();
    }

    public void Done_Btn_Click()
    {
        webCam.doneBtn.SetActive(false);
        if (webCam.photoIndex < Constants.PHOTOS_LENGTH_AMOUNT - 1)
        {
            Next_Btn_Click();
        }
    }

}
