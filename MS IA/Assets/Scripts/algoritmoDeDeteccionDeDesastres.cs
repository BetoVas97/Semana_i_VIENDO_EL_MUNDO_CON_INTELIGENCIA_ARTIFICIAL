using System.Collections;
using UnityEngine;


//Desarrollado por Daniel ALillo Marinez y ligeras adaptaciones por Irvin Emmanuel Trujillo Díaz.

public class AlgoritmoDeDeteccionDeDesastres : MonoBehaviour
{
	//banco de palabras - TAGS.
	public string ifo = "fire forest tree grass yellow plant dark red yellow fireplace mountain forest "; //incendio forestal
	public string ire = " fire building outdoor street yellow smoke city traffic fireplace"; //incendio en un edificio, residencia
	public string tsu = " water, nature, wave, wet, city beach, surfing, sand, ocean, mountain, large "; //tsunami
	public string phe = "outdoor sitting girl boy person laying, lying, woman, man, floor, sleeping, outdoor, street, ground, red "; //persona herida.
	public string earth = "rock rocky old, pile,gray rock, ground,street,house,building, dirt,sign, church tower"; //terremoto
	public string auto = " car, street, road,old, city, truck, motorcycle, dirt ";  //accidente de tránsito
	public string sink = " boat, ocean, large, man,water, floating, watercraft, lake "; //hundimiento de barco
	public string animal = " animal attack reptile mammal"; // animal salvaje
	public string traf = "traffic,light, outdoor, signal, red, yellow, green, " +
		"sign city street light"; //semáforo descompuesto
	public string flood = " building, water, car, street, person, man, city, people, house,  woman";

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

	public string realizarDeteccionDeSiniestro()
	{

		foreach (string elementoTag in this.tagsDetectadosDeImagenes) //hace un "loop mejrado" para visitar los tag
		{
			string[] tagLeido = elementoTag.Split('\"');
			if (tagLeido[1].Equals("smiling"))
			{
				PlayerPrefs.SetString("siniestro", "ERROR");
				return ("ERROR");
			}
			Check(tagLeido[1]);//modifica variables globales para determinar a que siniestro corresponde determinado conjunto de tags recolectado previamente con la recoleccion de información
		}
		//lista de siniestros
		string[] sin = { "incendio forestal", "incendio de residencia", "tsunami", "persona herida", "terremoto"
				, "accidente de transito", "barco hundido", "ataque animal", "semaforo", "Inundacion" };
		//lista de los puntajes para determinar de que desastre se trata.
		float[] listaPuntajeDesastre = { this.ifoN, this.ireN, this.tsuN, this.pheN, this.earthN, this.autoN, this.sinkN, this.animalN, this.trafN, this.floodN };
		print(acomodar(listaPuntajeDesastre, sin));
		PlayerPrefs.SetString("siniestro", acomodar(listaPuntajeDesastre, sin));
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
		if (lista[0] < 3)
		{
			return "ERROR";
		}
		//return ("Es un: \n" + sin[0] +" "+  lista[0] + " \n o tambien \n un " + sin[1] +" "+ lista[1]);
		return (sin[0]);
	}

	void Check(string tagLeido)
	{

		//string[] tagLeido=  tagLeidoConFormateoInadecuado.Split('\"'); //generando una lista para quitar las comillas integradas en el string de json y asi poder hace rla evaluacion correspondiente
		//el primer elemento de la lista generada es el tag que queremos!!! :)

		if (ifo.Contains(tagLeido))
		{
			ifoN++;
			if (tagLeido.Equals("grass") || tagLeido.Equals("dark") || tagLeido.Equals("light") || tagLeido.Equals("nature")
				|| tagLeido.Equals("forest") || tagLeido.Equals("tree") || tagLeido.Equals("fire") || tagLeido.Equals("smoke"))
			{
				ifoN++;
			}
		}
		if (ire.Contains(tagLeido.ToString()))
		{
			ireN++;
			if (tagLeido.Equals("building") || tagLeido.Equals("smoke") || tagLeido.Equals("fire")
				|| tagLeido.Equals("street") || tagLeido.Equals("fireplace"))
			{
				ireN++;
			}
		}
		if (tsu.Contains(tagLeido))
		{
			if (tagLeido.Equals("surfing") || tagLeido.Equals("ocean") || tagLeido.Equals("wave") || tagLeido.Equals("large"))
			{
				tsuN++;
			}
			tsuN++;
		}
		if (earth.Contains(tagLeido))
		{
			if (tagLeido.Equals("building") || tagLeido.Equals("street") || tagLeido.Equals("house")
				|| tagLeido.Equals("outdoor"))
			{
				earthN++;
			}
			earthN++;
		}
		if (auto.Contains(tagLeido))
		{
			autoN++;
			if (tagLeido.Equals("car") || tagLeido.Equals("truck") || tagLeido.Equals("street") || tagLeido.Equals("old"))
			{
				autoN++;
			}
		}
		if (sink.Contains(tagLeido))
		{
			sinkN++;
			if (tagLeido.Equals("ship") || tagLeido.Equals("watercraft") || tagLeido.Equals("ocean"))
			{
				sinkN++;
			}
		}
		if (animal.Contains(tagLeido))
		{
			animalN++;
			if (tagLeido.Equals("animal") || tagLeido.Equals("mammal"))
			{
				animalN++;
			}
		}
		if (phe.Contains(tagLeido))
		{
			pheN++;
			if (tagLeido.Equals("laying") || tagLeido.Equals("lying"))
			{
				pheN++;
			}
		}
		if (traf.Contains(tagLeido))
		{
			trafN++;
			if (tagLeido.Equals("traffic") || tagLeido.Equals("light") || tagLeido.Equals("sign"))
			{
				trafN++;
			}
		}
		if (flood.Contains(tagLeido))
		{
			floodN++;
			if (tagLeido.Equals("building") || tagLeido.Equals("street") || tagLeido.Equals("water"))
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
