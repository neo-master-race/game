//Schneberger Maxime
//21-02-18

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu_selection : MonoBehaviour {

    [Header("Enter_Game buttons")]
    public GameObject logInButton;
    public GameObject signUpButton;
    public GameObject playAsGuestButton;

    [Header("Menu buttons")]
    public GameObject playButton;
    public GameObject customizeButton;
    public GameObject profileButton;

    [Header("Circuits buttons")]
    public GameObject circuit1Button;
    public GameObject circuit2Button;
    public GameObject circuit3Button;


    //Schneberger Maxime
    //Quand le pointeur de la souris entre dans un object possédant un event trigger lié avec ce script
    public void OnPointer_Enter () {
        switch(this.name)
        {
            /************************************************************************************
             *                          BOUTONS D'ENTREE                                        *
             * **********************************************************************************/
            case "Log_In_Text":
                //change la couleur du bouton log_in en vert
                GameObject.Find("Log_In").GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Sign_Up_Text":
                //change la couleur du bouton sign_up en vert
                GameObject.Find("Sign_Up").GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Play_As_Guest_Text":
                //change la couleur du bouton play_as_guest en vert
                GameObject.Find("Play_As_Guest").GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;
            /************************************************************************************
             *                          BOUTONS D'ENTREE                                        *
             * **********************************************************************************/




            /************************************************************************************
             *                          BOUTONS DE CONNECTION                                   *
             * **********************************************************************************/
            case "Confirm_Text":
                //change la couleur du bouton confirm en vert
                GameObject.Find("Confirm_Button").GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Back_Text":
                //change la couleur du bouton play_as_guest en vert
                GameObject.Find("Back_Button").GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;
            /************************************************************************************
             *                          BOUTONS DE CONNECTION                                   *
             * **********************************************************************************/




            /************************************************************************************
             *                        BOUTONS DU MENU PRINCIPAL                                 *
             * **********************************************************************************/
            case "Play_Text":
                //change la couleur du bouton play en vert
                GameObject.Find("Play").GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Customize_Text":
                //change la couleur du bouton customize en vert
                GameObject.Find("Customize").GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Profile_Text":
                //change la couleur du bouton profile en vert
                GameObject.Find("Profile").GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;
            /************************************************************************************
             *                        BOUTONS DU MENU PRINCIPAL                                 *
             * **********************************************************************************/
        }
    }

    //Schneberger Maxime
    //Quand le pointeur de la souris sort d'un object possédant un event trigger lié avec ce script
    public void OnPointer_Exit () {
        switch (this.name)
        {

            /************************************************************************************
             *                          BOUTONS D'ENTREE                                        *
             * **********************************************************************************/
            case "Log_In_Text":
                //change la couleur du bouton log_in en blanc
                GameObject.Find("Log_In").GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;

            case "Sign_Up_Text":
                //change la couleur du bouton sign_up en blanc
                GameObject.Find("Sign_Up").GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;

            case "Play_As_Guest_Text":
                //change la couleur du bouton play_as_guest en blanc
                GameObject.Find("Play_As_Guest").GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;
            /************************************************************************************
             *                          BOUTONS D'ENTREE                                        *
             * **********************************************************************************/




            /************************************************************************************
             *                          BOUTONS DE CONNECTION                                   *
             * **********************************************************************************/
            case "Confirm_Text":
                //change la couleur du bouton confirm en blanc
                GameObject.Find("Confirm_Button").GetComponent<RawImage>().color = new Color(255, 0, 0);
                break;

            case "Back_Text":
                //change la couleur du bouton play_as_guest en blanc
                GameObject.Find("Back_Button").GetComponent<RawImage>().color = new Color(255, 0, 0);
                break;
            /************************************************************************************
             *                          BOUTONS DE CONNECTION                                   *
             * **********************************************************************************/




            /************************************************************************************
             *                        BOUTONS DU MENU PRINCIPAL                                 *
             * **********************************************************************************/
            case "Play_Text":
                //change la couleur du bouton play en vert
                GameObject.Find("Play").GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;

            case "Customize_Text":
                //change la couleur du bouton customize en vert
                GameObject.Find("Customize").GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;

            case "Profile_Text":
                //change la couleur du bouton profile en vert
                GameObject.Find("Profile").GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;
            /************************************************************************************
             *                        BOUTONS DU MENU PRINCIPAL                                 *
             * **********************************************************************************/
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
