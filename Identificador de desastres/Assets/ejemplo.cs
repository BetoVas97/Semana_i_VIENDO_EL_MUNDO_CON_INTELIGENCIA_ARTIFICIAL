using System.Collections;
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
    public string animal = " animal attack reptile mammal"; // animal salvaje
    public string traf = "traffic,light, outdoor, signal, red, yellow, green, " +
        "sign city street light"; //semáforo descompuesto
    public string flood = "outdoor, building, water, car, street, person, man, city, people, house, church, woman";


    //metricas para saber cual es el suceso de la imagen :v
    public float ifoN = 0;
    public float ireN = 0;
    public float tsuN = 0;
    public float pheN = 0;
    public float earthN = 0;
    public float autoN = 0;
    public float sinkN = 0;
    public float animalN = 0;
    public float trafN = 0;
    public float floodN = 0;


    // Use this for initialization
    void Start ()
    {
        string[] sub = { "hello", "dog", "sleeping", "nature", "street", "wave", "sand", "man", "rock", "woman","lake"
        ,"watercraft","dirt","smoke"};
        int x = 0;
        while (x < sub.Length)//hace un loop para todos los tag
        {
            Check(sub[x]);//determina a que siniestro corresponde determinado tag
            x++;
        }
        string[] sin= { "incendio forestal", "incendio de residencia", "tsunami", "persona herida", "terremot"
                , "accidente de transito", "barco hundido", "ataque animal", "semaforo", "Inundacion" }; 
        //arreglo de siniestros
        float[] siniestros = {ifoN, ireN, tsuN, pheN, earthN, autoN, sinkN, animalN, trafN, floodN};
        //arreglo
        
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
        else if (animal.Contains(sub))
        {
            animalN++;
            if (sub.Equals("animal") || sub.Equals("mammal"))
            {
                animalN++;
            }
        }
        else if (traf.Contains(sub))
        {
            trafN++;
            if (sub.Equals("traffic") || sub.Equals("light") || sub.Equals("sign"))
            {
                trafN++;
            }
        }
        else if (flood.Contains(sub))
        {
            sinkN++;
            if (sub.Equals("building") || sub.Equals("street"))
            {
                floodN++;
            }
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
