using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Camera_Scene_Manager : MonoBehaviour
{
    public Renderer previewRend;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Load_Selfie(int selfieIndex)
    {
        string selfiePath = Path.Combine(Constants.SELFIE_PATH ,Constants.SELFIE_PRE_NAME + selfieIndex + Constants.SELFIE_EXTENSION);
        Debug.Log(selfiePath);
        if (File.Exists(selfiePath))
        {
            byte[] data = File.ReadAllBytes(selfiePath);
            Texture2D tex = new Texture2D(331, 331);
            tex.LoadImage(data);
            previewRend.material.mainTexture = tex;
        }
    }
}
