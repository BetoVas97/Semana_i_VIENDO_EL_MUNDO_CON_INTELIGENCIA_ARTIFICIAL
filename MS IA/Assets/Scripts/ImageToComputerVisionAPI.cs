using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class ImageToComputerVisionAPI : MonoBehaviour {

    string VISIONKEY = "5e72ba714a39441d962d94f43481112f"; // replace with your Computer Vision API Key

    //string emotionURL = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/analyze";
    string emotionURL = "https://westcentralus.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures=Categories,Description,Color&language=en";
    public string fileName;
    string responseData;

    public int imagesCounter;

    public Text txtDescripcion;

    // Use this for initialization
    void Start () {
        imagesCounter = PlayerPrefs.GetInt("imgCounter");
	    //fileName = Path.Combine(Application.streamingAssetsPath, "cityphoto.jpg"); // Replace with your file
        fileName = Application.persistentDataPath + "/photo.png";
    }
	
	// Update is called once per frame
	void Update () {
	
        // This will be called with your specific input mechanism
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetVisionDataFromImages());
        }

	}
    /// <summary>
    /// Get emotion data from the Cognitive Services Emotion API
    /// Stores the response into the responseData string
    /// </summary>
    /// <returns> IEnumerator - needs to be called in a Coroutine </returns>
    IEnumerator GetVisionDataFromImages()
    {
        //byte[] bytes = UnityEngine.Windows.File.ReadAllBytes(fileName);
		byte[] bytes = File.ReadAllBytes(fileName);
        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", VISIONKEY },
            { "Content-Type", "application/octet-stream" }
        };

        WWW www = new WWW(emotionURL, bytes, headers);

        yield return www;
        responseData = www.text; // Save the response as JSON string

		JSONObject dataArray = new JSONObject(responseData);
		//print("Imprimiendo objeto JSON: " + dataArray.ToString()); 
        //json ejemplo: "categories":[{"name":"dark_fire","score":0.90625}],"description":{"tags":["smoke","train","coming","track","grass","steam","air","country","clouds","old","forest","fire","dark","stop","mountain","throwing","sun","riding","intersection","water"],"captions":[{"text":"a train on a track with smoke coming out of it","confidence":0.2727745}]},"requestId":"79ebe576-ffe1-4518-96de-07f03b83138a","metadata":{"width":1280,"height":720,"format":"Jpeg"},"color":{"dominantColorForeground":"Brown","dominantColorBackground":"Brown","dominantColors":["Brown","Orange"],"accentColor":"C79304","isBWImg":false}}
		print("Respuesta EN IMAGETOCOMPUTER-tags: :v " + dataArray.list[1].list[0]);//imprimiendo lista de tags, ya que la objtengo del json
        print("Respuesta image computer description" + dataArray.list[1].list[1].list[0].list[0]);
        txtDescripcion.text = " " + dataArray.list[1].list[1].list[0].list[0];
		AlgoritmoDeDeteccionDeDesastres lol = new AlgoritmoDeDeteccionDeDesastres(dataArray.list[1].list[0]); //creo la clase mandandole el jsonobject :v- INTERNAMEMTE EN ESTA CLASE AL HACER EL START SE MANDAN A LLAMAR LAS FUNCIONES 
        //GetComponent<ParseComputerVisionResponse>().ParseJSONData(responseData);  //se comenta ya que no lo usaremos en el sistema al momento .. :v

		//GetComponent<ParseComputerVisionResponse>().ParseJSONData(responseData);
    }







    public void callImageRecognition(){
        Invoke("enlace", 1.0f);

    }
    public void enlace(){
        StartCoroutine(GetVisionDataFromImages());
    }
}
