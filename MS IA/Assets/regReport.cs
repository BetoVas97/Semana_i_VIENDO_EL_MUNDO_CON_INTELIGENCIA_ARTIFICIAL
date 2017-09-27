using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regReport : MonoBehaviour
{
    public int imagesCounter;
    // Use this for initialization
    void Start()
    {
        imagesCounter = PlayerPrefs.GetInt("imgCounter");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddNumReport(){
        imagesCounter++;
        PlayerPrefs.SetInt("imgCounter",imagesCounter);
    }
}
