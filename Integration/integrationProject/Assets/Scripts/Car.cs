using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    float MaxRotation = 40.0f;
    float MinRotation = 10.0f; 
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
            padRotation = (0.0f < padRotation) ? MaxRotation : -(MaxRotation); 
        }
        /*
         * Zone morte. Si la rotation est inférieure à MinRotation, elle est nulle. 
         */
        if (Mathf.Abs(padRotation) < MinRotation)
        {
            padRotation = (0.0f < padRotation) ? MinRotation : -MinRotation;
        }
        padRotation += (0.0f < padRotation) ? -MinRotation : MinRotation; 
        wheelsRotation = padRotation;

        /*
        * Je fais tourner les roues
        */
        for (int r = 0; r < 4; r++)
        {
            wheels[r].transform.localEulerAngles = new Vector3(0, padRotation, time);
        }

        /*
         * Le corps suit les roues avec un delai 
         */
        float difference = wheelsRotation;

        if (CatchupPrecision < Mathf.Abs(difference))  //Le corps ne tourne que s'il a plus de X° de différence avec les roues
        {
            float way = (0.0f <= difference) ? 1.0f : -1.0f; //Est-ce la différence est positive ou négative? 
            float catchup = way * Time.deltaTime * CatchupSpeed; //Il faut adapter la vitesse au temps de rendu
            if (Mathf.Abs(difference) < Mathf.Abs(catchup)) //Il ne faut pas sur-compenser
                catchup = difference; 
            bodyRotation += catchup; 
        }
        
        transform.eulerAngles = new Vector3(0, 90 + bodyRotation, 0); // (la caméra est à 90° donc j'ajuste) 

        /*
         * Mouvement vers l'avant 
         */
        float speed = 10.0f * Time.deltaTime * Mathf.Cos(Mathf.Deg2Rad * wheelsRotation); 
        transform.position += Quaternion.Euler(0, bodyRotation, 0) * new Vector3(0.0f, 0.0f, speed);

        time += Time.deltaTime * 320.0f; 
    }
}
