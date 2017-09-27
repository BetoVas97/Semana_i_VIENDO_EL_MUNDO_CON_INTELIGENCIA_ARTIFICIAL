using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

public class EmotionToComputerApi : MonoBehaviour {

    string VISIONKEY = "5e72ba714a39441d962d94f43481112f"; 

    string emotionURL = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?";

    public string fileName { get; private set; }
    string responseData;

    // Use this for initialization
    void Start()
    {
        fileName = Path.Combine(Application.streamingAssetsPath, "triste.jpg"); // Replace with your file
    }

    // Update is called once per frame
    void Update()
    {

        // This will be called with your specific input mechanism
        if (Input.GetKeyDown(KeyCode.Space))
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
        byte[] bytes = System.IO.File.ReadAllBytes(fileName);
        var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", VISIONKEY },
            { "Content-Type", "application/octet-stream" }
        };

        WWW www = new WWW(emotionURL, bytes, headers);

        yield return www;
        responseData = www.text; // Save the response as JSON string
        GetComponent<ParseComputerVisionResponse>().ParseJSONData(responseData);
    }
}
