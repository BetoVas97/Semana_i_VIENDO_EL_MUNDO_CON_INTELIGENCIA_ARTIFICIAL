using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FBScript : MonoBehaviour {

	public GameObject DialogLoggedIn;
	public GameObject DialogLoggedOut;
	public GameObject DialogUsername;
	public GameObject DialogProfilePic;
    public Text iniciotext;
    public GameObject btnlog;

	void Awake()
	{
		FB.Init(SetInit, OnHideUnity);
	}
    void Start(){
        if(PlayerPrefs.GetInt("nombreActivado") == 1){
            iniciotext.text = "Hola! " + PlayerPrefs.GetString("nombre") + ". Ya iniciaste sessión." +  "\nAhora puedes reportar con tu nombre.";
            btnlog.SetActive(false);
        }
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.M)){
            PlayerPrefs.SetInt("nombreActivado", 0);
        }

    }

    void SetInit()
	{

		if (FB.IsLoggedIn)
		{
			Debug.Log("FB is logged in");
		}
		else
		{
			Debug.Log("FB is not logged in");
		}

		DealWithFBMenus(FB.IsLoggedIn);

	}

	void OnHideUnity(bool isGameShown)
	{

		if (!isGameShown)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}

	}

	public void FBlogin()
	{

		List<string> permissions = new List<string>();
		permissions.Add("public_profile");

		FB.LogInWithReadPermissions(permissions, AuthCallBack);
	}

	void AuthCallBack(IResult result)
	{

		if (result.Error != null)
		{
			Debug.Log(result.Error);
		}
		else
		{
			if (FB.IsLoggedIn)
			{
				Debug.Log("FB is logged in");
			}
			else
			{
				Debug.Log("FB is not logged in");
			}

			DealWithFBMenus(FB.IsLoggedIn);
		}

	}

	void DealWithFBMenus(bool isLoggedIn)
	{

		if (isLoggedIn)
		{
			DialogLoggedIn.SetActive(true);
			DialogLoggedOut.SetActive(false);

			FB.API("/me?fields=name", HttpMethod.GET, DisplayUsername);
			FB.API("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);

		}
		else
		{
			DialogLoggedIn.SetActive(false);
			DialogLoggedOut.SetActive(true);
		}

	}

	void DisplayUsername(IResult result)
	{

		Text UserName = DialogUsername.GetComponent<Text>();

		if (result.Error == null)
		{

			UserName.text = "Hola! , " + result.ResultDictionary["name"];
            PlayerPrefs.SetString("nombre",result.ResultDictionary["name"].ToString());
            PlayerPrefs.SetInt("nombreActivado",1);
		}
		else
		{
			Debug.Log(result.Error);
		}

	}

	void DisplayProfilePic(IGraphResult result)
	{

		if (result.Texture != null)
		{

			Image ProfilePic = DialogProfilePic.GetComponent<Image>();

			ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());


		}

	}
}
