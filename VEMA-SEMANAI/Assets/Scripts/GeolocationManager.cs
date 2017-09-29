/***************************************************************************************************************
This gist documents reverse geolocation by using Google Maps Geocoding API, assuming that the player has his/her
location enabled on the phone.

This is done by finding the player's longitude and latitude using:
1) Unity's LocationService (https://docs.unity3d.com/ScriptReference/LocationService.html)
2) Google Map's Geocoding API (https://developers.google.com/maps/documentation/geocoding/intro#reverse-example)

Reverse geolocation will use the player's longitude and latitude to extract relevant information, 	
one of which is the player's country, which will be demonstrated here.

Note that you will need a key/authentication to successfully request information.
Get your key through https://developers.google.com/maps/documentation/geocoding/get-api-key

****************************************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;		//Need this for JSON Deserialization
using UnityEngine.UI;

public class GeolocationManager : MonoBehaviour {
	//Get a Google API Key from https://developers.google.com/maps/documentation/geocoding/get-api-key
	public string GoogleAPIKey; 
	
	public string latitude;
	public string longitude;
	public string countryLocation;

    public Text lat;
    public Text lon;
    public Text country;



    public void Location(){
        
        StartCoroutine(Start());
    }

	IEnumerator Start()
  	{
        print("entro a gps");
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;           

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 5;
		
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
		  yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
            print("tiempo de espera excedido");
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
            print("Unable to determine device location");
		    	yield break;
		}
		else
		{
		  // Access granted and location value could be retrieve
			longitude = Input.location.lastData.longitude.ToString();
			latitude = Input.location.lastData.latitude.ToString();
            lat.text = latitude + "," + longitude;
            PlayerPrefs.SetString("coordinates","" + latitude + "," + longitude);

		}
		
		//Stop retrieving location
		Input.location.Stop();
		print("se envio info a google");
		//Sends the coordinates to Google Maps API to request information
		using (WWW www = new WWW("https://maps.googleapis.com/maps/api/geocode/json?latlng=" +latitude +","+ longitude   + "&key=" + GoogleAPIKey)){
            
            yield return www;

			//if request was successfully
			if(www.error == null)
			{
                
				//Deserialize the JSON file
				var location =  Json.Deserialize(www.text) as Dictionary<string, object>;
				var locationList = location["results"] as List<object>;
				var locationList2 = locationList[0] as Dictionary<string, object>;
					
				//Extract the substring information at the end of the locationList2 string
				//countryLocation = locationList2["formatted_address"].ToString().Substring(locationList2["formatted_address"].ToString().LastIndexOf(",")+2);
                country.text = locationList2["formatted_address"].ToString();
                PlayerPrefs.SetString("direccion", "" + locationList2["formatted_address"].ToString());
				//This will return the country information
				Debug.LogAssertion(countryLocation);
			}
		};
    }
}
