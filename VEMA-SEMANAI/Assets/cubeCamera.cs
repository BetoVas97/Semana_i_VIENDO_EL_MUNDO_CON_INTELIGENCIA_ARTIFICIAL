using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class cubeCamera : MonoBehaviour {

	WebCamTexture _webcamtex;
    WebCamTexture _webcamtexBack;
    WebCamTexture WebCamActive;
	public int imagesCounter;


	WebCamDevice frontCamera;
    WebCamDevice backCamera;

    public Renderer scdCube;
    WebCamDevice[] devices;

    public Renderer meRenderer;
	// Use this for initialization
	void Start () {
        meRenderer = GetComponent<Renderer>();
		devices = WebCamTexture.devices;
        _webcamtex = new WebCamTexture(devices[0].name,1920,1080);
        _webcamtexBack = new WebCamTexture(devices[1].name,1920,1080);
		/*foreach (WebCamDevice cam in devices)
		{
			if (!cam.isFrontFacing)
			{
				frontCamera = cam;
				break;
			}
		}*/
		//imagesCounter = PlayerPrefs.GetInt("imgCounter");
		//frontCamera = devices[0];
        try{
            //_webcamtex = new WebCamTexture(devices[0].name);
            WebCamActive = _webcamtex;
            meRenderer.material.mainTexture = WebCamActive;
            WebCamActive.Play();
        }catch(Exception e){
            print("ouch");
        }

       

        /*_webcamtexBack = new WebCamTexture(devices[0].name);
        scdCube.material.mainTexture = _webcamtexBack;
        _webcamtexBack.Play();*/

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A))
		{
			StartCoroutine(CaptureTextureAsPNG());
		}
	}
    public void Shoot(){
        StartCoroutine(CaptureTextureAsPNG());
        Invoke("selfie", 0.1f);
    }
	IEnumerator CaptureTextureAsPNG()
	{
		yield return new WaitForEndOfFrame();
		Texture2D _TextureFromCamera = new Texture2D(GetComponent<Renderer>().material.mainTexture.width,
		GetComponent<Renderer>().material.mainTexture.height);
		_TextureFromCamera.SetPixels((GetComponent<Renderer>().material.mainTexture as WebCamTexture).GetPixels());
		_TextureFromCamera.Apply();
		byte[] bytes = _TextureFromCamera.EncodeToPNG();
        string filePath = Application.persistentDataPath + "/photo.png";
        print(Application.persistentDataPath); 
		//+ "screenshots/" +
		File.WriteAllBytes(filePath, bytes);
	}
	IEnumerator CaptureTextureAsPNGTwo()
	{
		
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForEndOfFrame();
        Texture2D _TextureFromCamera = new Texture2D(meRenderer.material.mainTexture.width,meRenderer.material.mainTexture.height);
        _TextureFromCamera.SetPixels((meRenderer.material.mainTexture as WebCamTexture).GetPixels());
		_TextureFromCamera.Apply();
		byte[] bytes = _TextureFromCamera.EncodeToPNG();
		string filePath = Application.persistentDataPath + "/phota.png";
        print(Application.persistentDataPath);
		//+ "screenshots/" +
		File.WriteAllBytes(filePath, bytes);
	}
    public void selfie(){
        WebCamActive.Stop();
        this.gameObject.SetActive(false);
        this.gameObject.SetActive(true);
        //_webcamtex = new WebCamTexture(devices[0].name);
        WebCamActive = _webcamtexBack;
        meRenderer.material.mainTexture = WebCamActive;
		
        WebCamActive.Play();
        Invoke("enlace", 0.5f);
    }
    public void enlace(){
        
        StartCoroutine(CaptureTextureAsPNGTwo());

        Invoke("returnCamera", 1.0f);
    }
    public void returnCamera(){
        WebCamActive.Stop();
        WebCamActive = _webcamtex;
		
        meRenderer.material.mainTexture = WebCamActive;
        WebCamActive.Play();
    }
	
}
