using System.Collections;
using UnityEngine;


//Desarrollado por Daniel ALillo Marinez y ligeras adaptaciones por Irvin Emmanuel Trujillo Díaz.

public class AlgoritmoDeDeteccionDeDesastres : MonoBehaviour
{
    //banco de palabras - TAGS.
    public string ifo = "fire forest tree plant dark red yellow fireplace mountain "; //incendio forestal
    public string ire = " fire building outdoor street smoke city"; //incendio en un edificio, residencia
    public string tsu = " water, nature, wave, wet, beach, surfing, sand, ocean "; //tsunami
    public string phe = " Laying, lying, woman, man, floor, sleeping, outdoor, street, ground, red "; //persona herida.
    public ArrayList earth =  new ArrayList {"old", "pile","rock","ground","street","house","building", "dirt"}; //terremoto
    public string auto = " car, street, road,old, city, truck, motorcycle, dirt ";  //accidente de tránsito
    public string sink = " boat, ocean, large, man,water, floating, watercraft, lake "; //hundimiento de barco
    public string animal = " animal attack reptile mammal"; // animal salvaje
    public string traf = "traffic,light, outdoor, signal, red, yellow, green, " +
        "sign city street light"; //semáforo descompuesto
    public string flood = "outdoor, building, water, car, street, person, man, city, people, house, church, woman";

    //ArrayList de tags recaudadas de la imagen analizada con ImageToComputerVisionAPI.

    ArrayList tagsDetectadosDeImagenes = new ArrayList {"hello", "dog", "sleeping", "nature", "street", "sand", "man", "rock", "woman", "lake"
        , "dirt", "smoke", "animal", "mamal", "attack", "reptile"};  //esto son los de default para las pruebas pero seran substitudios al invocar la clase en ImageToComputerVisionAPI.cs

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


    //CONSTRUCTOR
    public AlgoritmoDeDeteccionDeDesastres(JSONObject tags)
    {
        this.tagsDetectadosDeImagenes = new ArrayList(tags.Count); //se crea un nuevo arraylist de la clase.
        
        //Los tags son un JSONObject, es una especie de lista pero esta no se puede convertir de manera explicita en una lista comund e strings, por lo hay que hacerlo a manita como si fuera en C:
        for (int i = 0; i < tags.Count; i++)
        {
            this.tagsDetectadosDeImagenes.Add(tags.list[i].ToString()); //se visita cada elemento y se agrega al arraylist de la clase         
        }

        this.realizarDeteccionDeSiniestro();
    }

    // inicializaciond de variables
    void Start()
    {
   

    }

    public string realizarDeteccionDeSiniestro() {

        foreach (string elementoTag in this.tagsDetectadosDeImagenes) //hace un "loop mejrado" para visitar los tag
        {
            //print(elementoTag); //imprime cada tag
            Check(elementoTag);//modifica variables globales para determinar a que siniestro corresponde determinado conjunto de tags recolectado previamente con la recoleccion de información
        }
        //lista de siniestros
        string[] sin = { "incendio forestal", "incendio de residencia", "tsunami", "persona herida", "terremoto"
                , "accidente de transito", "barco hundido", "ataque animal", "semaforo", "Inundacion" };
        //lista de los puntajes para determinar de que desastre se trata.
        float[] listaPuntajeDesastre = { this.ifoN, this.ireN, this.tsuN, this.pheN, this.earthN, this.autoN, this.sinkN, this.animalN, this.trafN, this.floodN };
        print (acomodar(listaPuntajeDesastre, sin));
        return (acomodar(listaPuntajeDesastre, sin));
    }


    public string acomodar(float[] lista, string[] sin)
    {
        float temp = 0;
        string tt = "";

        for (int i = 0; i < (lista.Length - 1); i++)
        {
            for (int j = i + 1; j < lista.Length; j++)
            {
                if (lista[j] > lista[i])
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
        //return ("Es un: \n" + sin[0] +" "+  lista[0] + " \n o tambien \n un " + sin[1] +" "+ lista[1]);
        return (sin[0] + "*"+ sin[1]);
    }

    void Check(string tagLeidoConFormateoInadecuado)
    {

        string[] tagLeido=  tagLeidoConFormateoInadecuado.Split('\"'); //generando una lista para quitar las comillas integradas en el string de json y asi poder hace rla evaluacion correspondiente
        //el primer elemento de la lista generada es el tag que queremos!!! :)
        if (ifo.Contains(tagLeido[1]))
        {
            ifoN++;
            if (tagLeido[1].Equals("nature") || tagLeido[1].Equals("tree"))
            {
                ifoN++;
            }
        }
         if (ire.Contains(tagLeido[1].ToString()))
        {
            ireN++;
            if (tagLeido[1].Equals("building"))
            {
                ireN++;
            }
        }
         if (tsu.Contains(tagLeido[1]))
        {
            if (tagLeido[1].Equals("surfing"))
            {
                tsuN++;
            }
            tsuN++;
        }
         if (earth.Contains(tagLeido[1]))
        {
            earthN++;
        }
         if (auto.Contains(tagLeido[1]))
        {
            autoN++;
            if (tagLeido[1].Equals("car") || tagLeido[1].Equals("truck"))
            {
                autoN++;
            }
        }
         if (sink.Contains(tagLeido[1]))
        {
            sinkN++;
            if (tagLeido[1].Equals("ship") || tagLeido[1].Equals("watercraft"))
            {
                sinkN++;
            }
        }
         if (animal.Contains(tagLeido[1]))
        {
            animalN++;
            if (tagLeido[1].Equals("animal") || tagLeido[1].Equals("mammal"))
            {
                animalN++;
            }
        }
         if (traf.Contains(tagLeido[1]))
        {
            trafN++;
            if (tagLeido[1].Equals("traffic") || tagLeido[1].Equals("light") || tagLeido[1].Equals("sign"))
            {
                trafN++;
            }
        }
         if (flood.Contains(tagLeido[1]))
        {
            floodN++;
            if (tagLeido[1].Equals("building") || tagLeido[1].Equals("street"))
            {
                floodN++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}