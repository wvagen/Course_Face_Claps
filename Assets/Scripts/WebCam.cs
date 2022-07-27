using UnityEngine;
using System.Collections;
using System.IO;

public class WebCam : MonoBehaviour {

	public WebCamTexture wct;
	public Texture2D maskTexture;
	public Texture2D sampleTexture500x500;
	public Renderer FaceMappingPanel;

	public GameObject doneBtn;

	public int photoIndex = 0;

	void Start ()
	{
		if (Application.platform == RuntimePlatform.Android){
			WebCamDevice[] devices = WebCamTexture.devices;

			foreach(WebCamDevice cam in devices)
			{
				if(cam.isFrontFacing )
				{
					wct = new WebCamTexture(cam.name, Screen.height, Screen.width, 12);
					wct.deviceName = cam.name;
					if(wct != null){
						FaceMappingPanel.material.mainTexture = wct;
						wct.Play();
					}
				}
			}
		}
		else{
			WebCamDevice[] devices = WebCamTexture.devices;
			string deviceName = devices[1].name;
			wct = new WebCamTexture(deviceName, Screen.height, Screen.width, 12);
			if(wct != null){
				FaceMappingPanel.material.mainTexture = wct;
				wct.Play();
			}
		}

	}

	
	public void CutOutFace()
	{
		//pausing webcam
		wct.Pause();

		// creating a new texture2d, having same width and height of our webcam texture
		Texture2D destTexture = new Texture2D(wct.width, wct.height, TextureFormat.ARGB32, false);
		// storing pixel data of webcam texture using getpixels
		Color[] textureData = wct.GetPixels();

		// assigning textureData to destTexture
		destTexture.SetPixels(textureData);
		// applying the changes to the texture
		destTexture.Apply();
		// scaling destTexture size to match sampleTexture331x331 that 331*331,
		// so that you dont get unwanted result because of unmatched pixel data
		TextureScale.Bilinear (destTexture, 331, 331);

		// storing pixel data of destTexture texture using getpixels
		textureData = destTexture.GetPixels();

		// assigning textureData to sampleTexture331x331
		sampleTexture500x500.SetPixels(textureData);
		// applying the changes to the texture
		sampleTexture500x500.Apply();


		// storing pixel data of maskTexture texture using getpixels
		Color [] maskPixels = maskTexture.GetPixels();	
		// storing pixel data of sampleTexture331x331 texture using getpixels
		Color [] curPixels = sampleTexture500x500.GetPixels();
		// running a nested for loop, which checks every pixel data in
		// maskPixels and if any pixel in maskPixels matches with maskPixels[0]
		// that is the black portion, it clears that pixel in curPixels
		// where curPixels store our real image from the webcam
		// so thats how we get that oval shape in middle

		int index = 0;
		for(int y = 0 ; y < maskTexture.height ; y++)
		{
			for(int x = 0 ; x < maskTexture.width ; x++)
			{
				if ( maskPixels[index] == maskPixels[0] )
				{
					curPixels[index] = Color.clear;
				}
				index++;
			}
		}
		sampleTexture500x500.SetPixels (curPixels,0);
		sampleTexture500x500.Apply(false);
		FaceMappingPanel.material.mainTexture = sampleTexture500x500;
		Save_File_To_Device_Locally(sampleTexture500x500.EncodeToPNG(), Constants.SELFIE_PRE_NAME + photoIndex + Constants.SELFIE_EXTENSION);
	}

	void Save_File_To_Device_Locally(byte[] bytes, string path)
	{
		string persistentDataPath = Constants.SELFIE_PATH;
        //persistentDataPath = @"/Users/mouadhmkadmi/Documents/Unity\ Projects/Course_Face_Claps/Assets";
        persistentDataPath = Path.Combine(persistentDataPath, path); //For Pc
		Debug.Log(persistentDataPath);

		if (!File.Exists(persistentDataPath))
		{
			File.WriteAllBytes(persistentDataPath, bytes);
		}
	}
}
