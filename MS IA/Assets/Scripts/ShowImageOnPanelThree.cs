using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowImageOnPanelThree : MonoBehaviour {

    public GameObject ImageFrameObject; // The object to place the image on


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            DisplayFace();
        }

    }

    void DisplayFace()
    {
        Texture2D imageTxtr = new Texture2D(2, 2);
        string fileName = gameObject.GetComponent<FaceToComputerVisionAPI>().fileName;
        byte[] fileData = System.IO.File.ReadAllBytes(fileName);
        imageTxtr.LoadImage(fileData);
        ImageFrameObject.GetComponent<Renderer>().material.mainTexture = imageTxtr;

    }
}
