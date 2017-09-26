using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ejemplo : MonoBehaviour {

    //banco de palabras.
    public string ifo = "fire forest tree plant dark red yellow fireplace mountain "; //incendio forestal
    public string ire = " fire building outdoor street smoke city"; //incendio en un edificio, residencia
    public string tsu = " water, nature, wave, wet, beach, surfing, sand, ocean "; //tsunami
    public string phe = " Laying, lying, woman, man, floor, sleeping, outdoor, street, ground, red "; //persona herida.
    public string earth = "old, pile, rock, ground, street, house, building, dirt "; //terremoto
    public string auto = " car, street, road,old, city, truck, motorcycle, dirt ";  //accidente de tránsito
    public string sink = " boat, ocean, large, man,water, floating, watercraft, lake "; //hundimiento de barco


    //metricas para saber cual es el suceso de la imagen :v
    public float ifoN = 0;
    public float ireN = 0;
    public float tsuN = 0;
    public float pheN = 0;
    public float earthN = 0;
    public float autoN = 0;
    public float sinkN = 0;
    

    // Use this for initialization
    void Start ()
    {
        string[] sub = { "hello", "dog", "sleeping", "nature", "street", "wave", "sand", "man", "rock", "woman","lake"
        ,"watercraft","dirt","smoke"};
        int x = 0;
        while (x < sub.Length)
        {
            Check(sub[x]);
            x++;
        }
        string[] sin= { "incendio forestal", "incendio de residencia", "tsunami", "persona herida", "terremot"
                , "accidente de transito", "barco hundido" };
        float[] siniestros = {ifoN, ireN, tsuN, pheN, earthN, autoN, sinkN };
        
        //print("forestal: "+ ifoN+ "\n incendio residencial: "+ ireN+ "\n tsunami: "+ tsuN+ "\n persona herida" +
        //  ": "+pheN+ "\n terremoto: "+ earthN);
        print(acomodar(siniestros, sin));

    }

    
    public string acomodar(float []lista, string[] sin)
    {
        float temp = 0;
        string tt = "";

        for(int i =0; i< (lista.Length-1); i++)
        {
            for(int j = i+1;j < lista.Length; j++)
            {
                if(lista[j]>lista[i])
                {
                    temp = lista[i];
                    lista[i] = lista[j];
                    lista[j] = temp;

                    tt = sin[i];
                    sin[i] = sin[j];
                    sin[j] = tt;
                }
            }
        }
        return ("Es un: \n"+sin[0]+"\n o \n un "+sin[1]);
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
        else if (auto.Contains(sub))
        {
            autoN++;
            if(sub.Equals("car") || sub.Equals("truck"))
            {
                autoN++;
            }
        }
        else if(sink.Contains(sub))
        {
            sinkN++;
            if (sub.Equals("ship") ||  sub.Equals("watercraft"))
            {
                sinkN++;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
