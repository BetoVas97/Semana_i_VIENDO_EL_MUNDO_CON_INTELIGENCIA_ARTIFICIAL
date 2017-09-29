using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameManager : MonoBehaviour {

    public Text txtName;
	// Use this for initialization
	void Start () {
        if(PlayerPrefs.GetInt("nombreActivado")==1){

            txtName.text = PlayerPrefs.GetString("nombre");
        }
        else{
            txtName.text = "Anónimo";
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.M)){
            PlayerPrefs.SetInt("nombreActivado", 0);
        }
	}

}
