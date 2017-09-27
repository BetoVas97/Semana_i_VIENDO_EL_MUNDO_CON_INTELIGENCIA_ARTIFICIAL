using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

public class FaceToComputerVisionAPI : MonoBehaviour {
    string VISIONKEY = "8868a36ec9914630a82801d93bdb214b";

    string emotionURL = "https://westcentralus.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

    public string fileName { get; private set; }
    string responseData;

    // Use this for initialization
    void Start () {
        fileName = Path.Combine(Application.streamingAssetsPath, "face.jpg"); // Replace with your file
    }

    // Update is called once per frame
    void Update(){
        // This will be called with your specific input mechanism
        if (Input.GetKeyDown(KeyCode.Backspace))
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
            byte[] bytes = System.IO.File.ReadAllBytes(fileName);
            var headers = new Dictionary<string, string>() {
            { "Ocp-Apim-Subscription-Key", VISIONKEY },
            { "Content-Type", "application/octet-stream" }
        };

        WWW www = new WWW(emotionURL, bytes, headers);

        yield return www;
        responseData = www.text; // Save the response as JSON string
        //print("Salida faceApi" + responseData);
        JSONObject dataArray = new JSONObject(responseData);
        //print("Imprimiendo objeto JSON: " + dataArray.ToString()); //json ejemplo: "categories":[{"name":"dark_fire","score":0.90625}],"description":{"tags":["smoke","train","coming","track","grass","steam","air","country","clouds","old","forest","fire","dark","stop","mountain","throwing","sun","riding","intersection","water"],"captions":[{"text":"a train on a track with smoke coming out of it","confidence":0.2727745}]},"requestId":"79ebe576-ffe1-4518-96de-07f03b83138a","metadata":{"width":1280,"height":720,"format":"Jpeg"},"color":{"dominantColorForeground":"Brown","dominantColorBackground":"Brown","dominantColors":["Brown","Orange"],"accentColor":"C79304","isBWImg":false}}
        //GUARDAR LOS DATOS QUE QUEREMOS
        print("Respuesta EN FACETOCOMPUTER-genero: " + dataArray.list[0].list[2].list[2]+"\nFACECOMPUTER-edad: "+dataArray.list[0].list[2].list[3]);
        
        
        

    }
}
