using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;


public class EmailManager : MonoBehaviour {
	private bool hideCameraGUI;
	private string debugger;
	public Texture2D tex2d;

	private int i;

	public IEnumerator SendEmail()
	{
		debugger="Start";
		//CameraDevice.Instance.SetFlashTorchMode(true);
		yield return new WaitForSeconds(1);
		yield return new WaitForEndOfFrame();

		string smtp_Name;
		int port_no;
		string toAddr = "mgs2157@gmail.com";
		MailMessage mail = new MailMessage();
		//From address to send email
		mail.From = new MailAddress("mgs2157@gmail.com");
		//To address to send email
		mail.To.Add(toAddr);
		mail.Subject = "TEST for UNITY3D...!!!!!!";
		mail.Body = "This is a test mail from C# program";
		debugger="Befor Pic";
		int picNumber = PlayerPrefs.GetInt("PicNumber");
		picNumber++;
		ScreenCapture.CaptureScreenshot("currentImage_"+picNumber+".jpg",1);
		var pathToImage =  Application.persistentDataPath + "/currentImage_"+picNumber+".jpg";

		yield return 0;

		PlayerPrefs.SetInt("PicNumber", picNumber);
		debugger="Start Yield 4";
		yield return new WaitForSeconds(2);


		debugger="Done " + pathToImage;


		if(!System.IO.File.Exists(pathToImage))
		{
			Debug.LogError("There is no screenshot avaiable on " + pathToImage);
			debugger="aaaa " + pathToImage + "   \n" +  i;
			//return;
		}
		else
			debugger="Exist image " + pathToImage;

		yield return new WaitForSeconds(2);

		var data = System.IO.File.ReadAllBytes(pathToImage);
		debugger="1";
		yield return new WaitForSeconds(1);

		MemoryStream bytes = new MemoryStream(data);
		debugger="2";
		yield return new WaitForSeconds(1);

		//mail.Attachments.Add(new Attachment(bytes,"/currentImage_"+picNumber+".jpg", @"image/jpg"));

		string attachmentPath = pathToImage;
		System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentPath);
		mail.Attachments.Add(attachment);

		debugger="3";
		yield return new WaitForSeconds(1);
		//      screenShot = Application.persistentDataPath + "/Screenshot.jpg";
		//          mail.Attachments.Add(new Attachment(Application.persistentDataPath + "/Screenshot.jpg", @"image/jpg"));



		smtp_Name = "smtp.gmail.com";
		port_no = 587;

		debugger="4";
		yield return new WaitForSeconds(1);

		SmtpClient smtpC = new SmtpClient(smtp_Name);
		smtpC.Port = port_no;

		debugger="5";
		yield return new WaitForSeconds(1);

		//Credentials for From address
		smtpC.Credentials =(System.Net.ICredentialsByHost) new System.Net.NetworkCredential("myemail@gmail.com", "password") as ICredentialsByHost;

		debugger="6";
		yield return new WaitForSeconds(1);

		smtpC.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback =
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{ return true; };
		debugger="7";
		yield return new WaitForSeconds(1);
		smtpC.Send(mail);
		debugger="8";
		yield return new WaitForSeconds(1);

		hideCameraGUI=false;
		//CameraDevice.Instance.SetFlashTorchMode(false);
	}



	void OnGUI()
	{
		GUILayout.Label(debugger);
		if(!hideCameraGUI){
			if(GUI.Button(new Rect(0,0,200,200),"lightOn"))
			{

				//CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
				StartCoroutine(SendEmail());
				hideCameraGUI=true;
			}
		}
	}

}