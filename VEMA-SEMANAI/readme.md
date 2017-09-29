
# OPEN SOURCE.

Utilizando herramientras proporcionadas en el siguiente repositorio: https://github.com/misslivirose/unity-cognitive-services-demos/tree/master/unity-vision-demo

* Si desea probar el prototipo en la computadora, es necesario cambiar la linea 27 de la clase camara cubeCamara.cs ya que originalmente se encuentra como: new WebCamTexture(devices[1].name,1920,1080); 
es necesario cambiar el indice 1 por el indice 0 para que este funcione correctamente ya que se coloca el indice 1 cuando existen 2 camaras en el dispositiv o

## Setup
If you need instructions for setting up your machine for HoloLens development, check out the instructions on the [HoloLens Developer Site](https://www.microsoft.com/microsoft-hololens/en-us/developers).

1. Sign up for a [Cognitive Services](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api) Computer Vision API key. You will need this to access the API endpoints.

2. Clone the project to your local machine and open the project in Unity. 

3. Go to Scripts > ImageToComputerVisionAPI and replace the YOURVISIONKEYHERE string with your Computer Vision API key.

4. Run the application in the editor. Press 'P' to show the image on screen and the space bar to call the API request. The list of categories, as well as the category with the highest match, is printed in the debug console.

## Components
This demo contains the following components:

* FoundImageObject.cs: A class that wraps around the Computer Vision response returned by the API request
* ImageToComputerVisionAPI.cs: Script that handles the API call and returns the results as a JSON string
* ParseComputerVisionResponse.cs: Uses the JSON utility (included) to convert the JSON array to a list of FoundImageObjects
* ShowImageOnPanel.cs: Displays the photo onto a panel

 cityphoto.jpg with your own image. If you rename it, update line 19 in ImageToComputerVisionAPI.cs to reflect the new image name.

## Keyboard Shortcuts
Replace the following triggers with your own mechanism for selecting and displaying photos. 

Space: Calls the Cognitive Services API using your API key to validate and analyzing the sample photo 'cityphoto' in the Streaming Assets folder.

P: Display the image on the plane


