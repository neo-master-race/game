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

    [Header("Game Type Choice buttons")]
    public GameObject soloButton;
    public GameObject multiButton;
    public GameObject tutoButton;

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
                logInButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Sign_Up_Text":
                //change la couleur du bouton sign_up en vert
                signUpButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Play_As_Guest_Text":
                //change la couleur du bouton play_as_guest en vert
                playAsGuestButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
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
                playButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Customize_Text":
                //change la couleur du bouton customize en vert
                customizeButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Profile_Text":
                //change la couleur du bouton profile en vert
                profileButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;
            /************************************************************************************
             *                        BOUTONS DU MENU PRINCIPAL                                 *
             * **********************************************************************************/




            /************************************************************************************
             *                        BOUTONS DU TYPE DE PARTIE                                 *
             * **********************************************************************************/
            case "Solo_Text":
                //change la couleur du bouton solo en vert
                soloButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Multi_Text":
                //change la couleur du bouton multi en vert
                multiButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Tuto_Text":
                //change la couleur du bouton tuto en vert
                tutoButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;
            /************************************************************************************
             *                        BOUTONS DU TYPE DE PARTIE                                 *
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
                logInButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;

            case "Sign_Up_Text":
                //change la couleur du bouton sign_up en blanc
                signUpButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;

            case "Play_As_Guest_Text":
                //change la couleur du bouton play_as_guest en blanc
                playAsGuestButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
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
                //change la couleur du bouton play en blanc
                playButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;

            case "Customize_Text":
                //change la couleur du bouton customize en blanc
                customizeButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;

            case "Profile_Text":
                //change la couleur du bouton profile en blanc
                profileButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;
            /************************************************************************************
             *                        BOUTONS DU MENU PRINCIPAL                                 *
             * **********************************************************************************/




            /************************************************************************************
             *                        BOUTONS DU TYPE DE PARTIE                                 *
             * **********************************************************************************/
            case "Solo_Text":
                //change la couleur du bouton solo en blanc
                soloButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;

            case "Multi_Text":
                //change la couleur du bouton multi en blanc
                multiButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;

            case "Tuto_Text":
                //change la couleur du bouton tuto en blanc
                tutoButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
                break;
            /************************************************************************************
             *                        BOUTONS DU TYPE DE PARTIE                                 *
             * **********************************************************************************/
        }
    }


    //Schneberger Maxime
    //Quand l'utilisateur clique sur un object possédant un event trigger lié avec ce script
    public void OnPointer_Click()
    {
        switch (this.name)
        {
            /************************************************************************************
             *                        BOUTONS DU MENU PRINCIPAL                                 *
             * **********************************************************************************/
            case "Play_Text":
                soloButton.SetActive(true);
                multiButton.SetActive(true);
                tutoButton.SetActive(true);

                playButton.SetActive(false);
                customizeButton.SetActive(false);
                profileButton.SetActive(false);
                break;

            case "Customize_Text":
                
                break;

            case "Profile_Text":
                
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
