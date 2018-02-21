using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    float MaxRotation = 40.0f;
    float CatchupPrecision = 5.0f;
    float CatchupSpeed = 120.0f;

    float time; 

    /*
     * Les roues de la voiture et leur rotation
     */
    public GameObject[] wheels = new GameObject[4];
    float wheelsRotation;

    /*
     * La rotation du corps principal
     */
    float bodyRotation;

    void Start ()
    {
        time = 0.0f;

        /*
         * Réglage de l'écran pour qu'il ne tourne pas
         */
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
	
	void Update ()
    {
        /*
         * Input.acceleration.x donne la rotation de la tablette 
         */
        float padRotation = Input.acceleration.x * 100;
        if (MaxRotation < Mathf.Abs(padRotation))
        {
            padRotation = (0.0f < padRotation) ? MaxRotation : -MaxRotation; 
        }
        wheelsRotation = padRotation;

        /*
        * Je fais tourner les roues
        */
        for (int r = 0; r < 4; r++)
        {
            wheels[r].transform.localEulerAngles = new Vector3(0, wheelsRotation - bodyRotation, time);
        }

        /*
         * Le corps suit les roues avec un delai 
         */
        float difference = wheelsRotation - bodyRotation;

        if (CatchupPrecision < Mathf.Abs(difference))  //Le corps ne tourne que s'il a plus de X° de différence avec les roues
        {
            float way = (0.0f <= difference) ? 1.0f : -1.0f; //Est-ce la différence est positive ou négative? 
            float catchup = way * Time.deltaTime * CatchupSpeed; //Il faut adapter la vitesse au temps de rendu
            if (Mathf.Abs(difference) < Mathf.Abs(catchup)) //Il ne faut pas sur-compenser
                catchup = difference; 
            bodyRotation += catchup; 
        }
        
        transform.eulerAngles = new Vector3(0, 90 + bodyRotation, 0); // (la caméra est à 90° donc j'ajuste) 

        time += Time.deltaTime * 320.0f; 
    }
}
