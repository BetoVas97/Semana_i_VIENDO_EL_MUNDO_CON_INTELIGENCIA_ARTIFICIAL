using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class cubeCameraTwo : MonoBehaviour
{

	WebCamTexture _webcamtex;
	public int imagesCounter;

	
	WebCamDevice frontCamera;
	// Use this for initialization
	void Start()
	{
		WebCamDevice[] devices = WebCamTexture.devices;
		/*foreach (WebCamDevice cam in devices)
		{
			if (cam.isFrontFacing)
			{
                frontCamera = cam;
				break;
			}
		}*/
		frontCamera = devices[1];
		imagesCounter = PlayerPrefs.GetInt("imgCounter");
        //_webcamtex = new WebCamTexture();
        _webcamtex = new WebCamTexture(frontCamera.name);
		Renderer _renderer = GetComponent<Renderer>();
		_renderer.material.mainTexture = _webcamtex;
		_webcamtex.Play();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			StartCoroutine(CaptureTextureAsPNG());
		}
	}
	public void Shoot()
	{
		StartCoroutine(CaptureTextureAsPNG());
	}
	IEnumerator CaptureTextureAsPNG()
	{
		yield return new WaitForEndOfFrame();
		Texture2D _TextureFromCamera = new Texture2D(GetComponent<Renderer>().material.mainTexture.width,
		GetComponent<Renderer>().material.mainTexture.height);
		_TextureFromCamera.SetPixels((GetComponent<Renderer>().material.mainTexture as WebCamTexture).GetPixels());
		_TextureFromCamera.Apply();
		byte[] bytes = _TextureFromCamera.EncodeToPNG();
        string filePath = Application.persistentDataPath + "/photoTwo.png";
		print(Application.dataPath);
		//+ "screenshots/" +
		File.WriteAllBytes(filePath, bytes);

	}

}
