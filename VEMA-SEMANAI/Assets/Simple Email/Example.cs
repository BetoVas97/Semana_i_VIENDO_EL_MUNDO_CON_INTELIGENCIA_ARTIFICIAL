using UnityEngine;
using System.ComponentModel;
using UnityEngine.UI;
using System;
using UnityEngine.UI;

public class Example : MonoBehaviour {

    public int imagesCounter;

    bool triggerResultEmail= false;
    bool resultEmailSucess;

    public Text resultText;

    public InputField SMTPClient;
    public InputField SMTPPort;
    public InputField UserName;
    public InputField UserPass;
    public InputField To;
    public InputField Subject;
    public InputField Body;
    public InputField AttachFile;

    public string siniestro;
    public string correosin;
    public string depto;

    public Text txtSiniestro;
    public Text txtPersona;
    public Text txtDescSiniestro;

    public string name;

    void Start () {
        imagesCounter = PlayerPrefs.GetInt("imgCounter");
        if (PlayerPrefs.GetInt("nombreActivado") == 1)
        {

            name = PlayerPrefs.GetString("nombre");
        }else{
            name = "anónimo";
        }
        //SMTPClient.text = PlayerPrefs.GetString("SMTPClient");
        //SMTPPort.text = PlayerPrefs.GetString("SMTPPort");
        //UserName.text = PlayerPrefs.GetString("UserName");
        //UserPass.text = PlayerPrefs.GetString("UserPass");
        //To.text = PlayerPrefs.GetString("To");
        //Subject.text = PlayerPrefs.GetString("Subject");
        //Body.text = PlayerPrefs.GetString("Body");
        //AttachFile.text = PlayerPrefs.GetString("AttachFile");
    }

    void OnApplicationQuit()
    {
        //PlayerPrefs.SetString("SMTPClient", SMTPClient.text);
        //PlayerPrefs.SetString("SMTPPort", SMTPPort.text);
        //PlayerPrefs.SetString("UserName", UserName.text);
        //PlayerPrefs.SetString("UserPass", UserPass.text);
        //PlayerPrefs.SetString("To", To.text);
        //PlayerPrefs.SetString("Subject", Subject.text);
        //PlayerPrefs.SetString("Body", Body.text);
        //PlayerPrefs.SetString("AttachFile", AttachFile.text);

        PlayerPrefs.Save();
    }

    public void sendEmail()
    {
        SimpleEmailSender.emailSettings.STMPClient = SMTPClient.text.Trim();
        SimpleEmailSender.emailSettings.SMTPPort = Int32.Parse(SMTPPort.text.Trim());
        SimpleEmailSender.emailSettings.UserName = UserName.text.Trim();
        SimpleEmailSender.emailSettings.UserPass = UserPass.text.Trim();

        //SimpleEmailSender.Send(To.text, Subject.text, Body.text, AttachFile.text, SendCompletedCallback);
        //debug
        string datafile = Application.dataPath + "SavedScreen," + imagesCounter + ".png";
        SimpleEmailSender.Send(To.text, Subject.text, Body.text, datafile, SendCompletedCallback);
        //endDebug
    }
    public void sendEmailTwo(){
		//datos
		SimpleEmailSender.emailSettings.STMPClient = "smtp.gmail.com";
		SimpleEmailSender.emailSettings.SMTPPort = Int32.Parse("587");
		SimpleEmailSender.emailSettings.UserName = "gabendez27@gmail.com";
		SimpleEmailSender.emailSettings.UserPass = "itesmfake";
        //enviar
		string datafile = Application.persistentDataPath + "/photo.png";
        string datafileTwo = Application.persistentDataPath + "/phota.png";
        string bodyMessage = "";
        bodyMessage = bodyMessage + siniestro + " reportado en: ";
        bodyMessage = bodyMessage + PlayerPrefs.GetString("direccion");
        bodyMessage = bodyMessage + "\n";
        bodyMessage = bodyMessage + "Lo reportó: " + name;
        bodyMessage = bodyMessage + "\n https://maps.google.com/maps?q=" + PlayerPrefs.GetString("coordinates");
        bodyMessage = bodyMessage + "\n";
        bodyMessage = bodyMessage + "segun nuestro sistema la persona es de " + PlayerPrefs.GetString("edad") + " años.";
        bodyMessage = bodyMessage + "y es: " + PlayerPrefs.GetString("genero");
        bodyMessage = bodyMessage + "\n";
        bodyMessage = bodyMessage + "tambien muestra una emocion dominante de: " + PlayerPrefs.GetString("emocion");
        bodyMessage = bodyMessage + "\n";
        bodyMessage = bodyMessage + "Imagenes: ";
        bodyMessage = bodyMessage + "\n";
        SimpleEmailSender.Send(correosin, siniestro, bodyMessage, datafile,datafileTwo, SendCompletedCallback);
    }

    private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        if (e.Cancelled || e.Error != null)
        {
            print("Email not sent: " + e.Error.ToString());

            resultEmailSucess = false;
            triggerResultEmail = true;
        }
        else
        {
            print("Email successfully sent.");

            resultEmailSucess = true;
            triggerResultEmail = true;
        }
    }
    public void sendEmailInvoke(){
        

        //armar descripcion



		Invoke("sendEmailTwo", 2.0f);
	}
    public void enlace(){
        if(PlayerPrefs.GetInt("enlaceActivator")==1){
            Invoke("filldata", 1.0f);
        }else{
            Invoke("enlace", 0.5f);
        }

    }
    public void filldata(){
		siniestro = PlayerPrefs.GetString("siniestro");
        txtSiniestro.text = siniestro;

		if (siniestro == "incendio forestal")
		{
			correosin = "deptoprotecciontest@gmail.com";
            depto = "protección civil";
		}
		else if (siniestro == "incendio de residencia")
		{
			correosin = "deptobomberostest@gmail.com";
            depto = "bomberos";
		}
		else if (siniestro == "tsunami")
		{
			correosin = "deptoprotecciontest@gmail.com";
            depto = "protección civil";
		}
		else if (siniestro == "persona herida")
		{
			correosin = "deptourgenciastest@gmail.com";
            depto = "urgencias";
		}
		else if (siniestro == "terremoto")
		{
			correosin = "deptoprotecciontest@gmail.com";
            depto = "protección civil";
		}
		else if (siniestro == "accidente de transito")
		{
			correosin = "deptotransitotest@gmail.com";
            depto = "transito";
		}
		else if (siniestro == "barco hundido")
		{
			correosin = "deptoprotecciontest@gmail.com";
            depto = "protección civil";
		}
		else if (siniestro == "ataque animal")
		{
			correosin = "deptopoliciatest@gmail.com";
            depto = "policia";
		}
		else if (siniestro == "semaforo")
		{
			correosin = "deptotransitotest@gmail.com";
            depto = "transito";
		}
		else if (siniestro == "Inundacion")
		{
			correosin = "deptoprotecciontest@gmail.com";
            depto = "protección civil";
		}
		else
		{
			correosin = "gerardo.ezequiel@outlook.com";
            depto = "depto jerry";
		}
        txtPersona.text = depto;
    }
    public void examplee(){
        PlayerPrefs.SetInt("enlaceActivator", 0);
        txtSiniestro.text = "cargando...";
        txtPersona.text = "cargando...";
        txtDescSiniestro.text = "cargando...";
    }
}
