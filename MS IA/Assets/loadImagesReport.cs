using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class loadImagesReport : MonoBehaviour {

    public Image imagentrasera;
    public Image imagenfrontal;

     
    private Sprite mySprite;
    private Sprite mySpriteFrontal;

    public int imagesCounter;


	// Use this for initialization
	void Start () {
        imagesCounter = PlayerPrefs.GetInt("imgCounter");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public static Texture2D LoadPNG(string filePath)
	{

		Texture2D tex = null;
		byte[] fileData;

		if (File.Exists(filePath))
		{
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}

    public void loadImagenTrasera(){
        string datafile = Application.persistentDataPath + "/photo.png";
        Texture2D textureback = LoadPNG(datafile);
        mySprite = Sprite.Create(textureback, new Rect(0.0f, 0.0f, textureback.width, textureback.height), new Vector2(0.5f, 0.5f), 100.0f);
        imagentrasera.sprite = mySprite;
    }
    public void loadImagenFrontal(){
        
		//string datafile = Application.dataPath + "SavedScreen," + imagesCounter + ".png";
        string datafile = Application.persistentDataPath + "/phota.png";
		Texture2D textureback = LoadPNG(datafile);
        mySpriteFrontal = Sprite.Create(textureback, new Rect(0.0f, 0.0f, textureback.width, textureback.height), new Vector2(0.5f, 0.5f), 100.0f);
        imagenfrontal.sprite = mySpriteFrontal;
    }
    public void loadImages(){
		Invoke("loadImagenTrasera", 1.0f);
		Invoke("loadImagenFrontal", 3.0f);
    }
}
