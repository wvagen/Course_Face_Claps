using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Transform shine;
    public TextMeshProUGUI bestScoreTxtMesh;

    public Image soundsImg;
    public Sprite soundOnSprite, soundOffSprite;

    // Update is called once per frame

    private void Start()
    {
        Time.timeScale = 1;
        bestScoreTxtMesh.text = PlayerPrefs.GetInt("best_score", 0).ToString();
        Audio_Manager.isSoundOn= PlayerPrefs.GetInt("sfx", 1) == 1;
        Update_Sound_Img();
    }

    void Update_Sound_Img()
    {
        soundsImg.sprite = Audio_Manager.isSoundOn ? soundOnSprite : soundOffSprite;
    }

    public void Sound_Btn()
    {
        Audio_Manager.isSoundOn = !Audio_Manager.isSoundOn;
        PlayerPrefs.GetInt("sfx", Audio_Manager.isSoundOn ? 1 : 0);
        Update_Sound_Img();
    }

    public void Load_Scene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    void Update()
    {
        shine.transform.Rotate(Vector3.forward * Time.deltaTime * 5);
    }
}
