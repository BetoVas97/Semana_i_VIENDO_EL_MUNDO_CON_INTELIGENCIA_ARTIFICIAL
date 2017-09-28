using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;
using System.IO;

public class ImageToEmotionAPI : MonoBehaviour {

    string EMOTIONKEY = "355eea721c554929adbb87505529455d"; // replace with your Emotion API Key

    string emotionURL = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?";

    public string fileName;
    string responseData;

    public int imagesCounter;

    public GameObject advert;
    // Use this for initialization
    void Start () {
        imagesCounter = PlayerPrefs.GetInt("imgCounter");
        fileName = Application.persistentDataPath + "/phota.png"; // Replace with your file
    }
	
	// Update is called once per frame
	void Update () {
	
        // This will be called with your specific input mechanism
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetEmotionFromImages());
        }

	}
    /// <summary>
    /// Get emotion data from the Cognitive Services Emotion API
    /// Stores the response into the responseData string
    /// </summary>
    /// <returns> IEnumerator - needs to be called in a Coroutine </returns>
    IEnumerator GetEmotionFromImages()
    {
        //byte[] bytes = UnityEngine.Windows.File.ReadAllBytes(fileName);
        byte[] bytes = File.ReadAllBytes(fileName);
        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", EMOTIONKEY },
            { "Content-Type", "application/octet-stream" }
        };

        WWW www = new WWW(emotionURL, bytes, headers);

        yield return www;
        responseData = www.text; // Save the response as JSON string
        Debug.Log("emotion: " + responseData);
        GetComponent<ParseEmotionResponse>().ParseJSONData(responseData,advert);
    }
    public void callEmotion(){
        Invoke("enlace", 2.0f);
    }
    public void enlace(){
        StartCoroutine(GetEmotionFromImages());
    }
}
