using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

namespace Kakera
{
    public class PickerController : MonoBehaviour
    {
        [SerializeField]
        private Unimgpicker imagePicker;

        [SerializeField]
        private Camera_Scene_Manager camSceneMan;

        public int photoIndex = 0;

        void Awake()
        {
            imagePicker.Completed += (string path) =>
            {
                StartCoroutine(LoadImage(path));
            };
        }

        public void OnPressShowPicker()
        {
            imagePicker.Show("Select Image", "unimgpicker");
        }

        private IEnumerator LoadImage(string path)
        {
            var url = "file://" + path;
            var unityWebRequestTexture = UnityWebRequestTexture.GetTexture(url);
            yield return unityWebRequestTexture.SendWebRequest();

            var texture = ((DownloadHandlerTexture)unityWebRequestTexture.downloadHandler).texture;
            if (texture == null)
            {
                Debug.LogError("Failed to load texture url:" + url);
            }

            Save_File_To_Device_Locally(texture.EncodeToPNG(), Constants.SELFIE_PRE_NAME + photoIndex + Constants.SELFIE_EXTENSION);
        }

        void Save_File_To_Device_Locally(byte[] bytes, string path)
        {
            string persistentDataPath = Constants.SELFIE_PATH;
            //persistentDataPath = @"/Users/mouadhmkadmi/Documents/Unity\ Projects/Course_Face_Claps/Assets";
            persistentDataPath = Path.Combine(persistentDataPath, path); //For Pc
            Debug.Log(persistentDataPath);

            if (File.Exists(persistentDataPath))
            {
                File.Delete(persistentDataPath);
            }

            File.WriteAllBytes(persistentDataPath, bytes);

            camSceneMan.Update_Inputs();
        }
    }
}