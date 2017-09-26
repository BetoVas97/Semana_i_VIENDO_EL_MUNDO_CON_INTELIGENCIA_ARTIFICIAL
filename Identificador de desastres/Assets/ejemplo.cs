using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ejemplo : MonoBehaviour {

    //banco de palabras.
    public string ifo =  "fire forest tree plant dark red yellow fireplace mountain " ; //incendio forestal
    public string ire = " fire building outdoor street smoke city" ; //incendio en un edificio, residencia
    public string tsu =  " water, nature, wave, wet, beach, surfing, sand, ocean " ; //tsunami
    public string phe = " Laying, lying, woman, man, floor, sleeping, outdoor, street, ground, red " ; //persona herida.
    public string earth =  "old, pile, rock, ground, street " ; //terremoto

    //metricas para saber cual es el suceso de la imagen :v
    public float ifoN = 0;
    public float ireN = 0;
    public float tsuN = 0;
    public float pheN = 0;
    public float earthN = 0;

    // Use this for initialization
    void Start ()
    {
        string[] sub = { "hello", "dog", "sleeping", "nature", "street", "wave", "sand", "man", "rock", "woman" };
        int x = 0;
        while (x < sub.Length)
        {
            Check(sub[x]);
            x++;
        }
        print("forestal: "+ ifoN+ "\n incendio residencial: "+ ireN+ "\n tsunami: "+ tsuN+ "\n persona herida" +
            ": "+pheN+ "\n terremoto: "+ earthN);

    }
    void Check(string sub)
    {
        if (ifo.Contains(sub))
        {
            ifoN++;
            if (sub.Equals("nature") || sub.Equals("tree"))
            {
                ifoN++;
            }
        }
        else if (ire.Contains(sub))
        {
            ireN++;
            if (sub.Equals("building"))
            {
                ifoN++;
            }
        }
        else if (tsu.Contains(sub))
        {
            if (sub.Equals("surfing"))
            {
                tsuN++;
            }
            tsuN++;
        }
        else if (earth.Contains(sub))
        {
            earthN++;
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
