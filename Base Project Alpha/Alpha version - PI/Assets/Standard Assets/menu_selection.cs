//Schneberger Maxime
//21-02-18

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    [Header("Multiplayer Section")]
    public GameObject multiplayerUI;

    [Header("Circuits buttons")]
    public GameObject circuit1Button;
    public GameObject circuit2Button;
    public GameObject circuit3Button;

    [Header("Profile section")]
    public GameObject userInfo;
    public GameObject userStats;

    [Header("Others GameObject")]
    public GameObject confirmButton;
    public GameObject backButtonForm;
    public GameObject backButton;

    [Header("Variables")]
    public int selection_step = 0;
    public bool is_guest = false;
    public int confirm_start=0;
    public String start_action="";

	[Header("Menu index")]
	public int currentIndex = 1;
	public int maxIndex = 0;
	public InputField connexionUser;
	public InputField connexionPasswd;
	public InputField inscriptionUser;
	public InputField inscriptionPasswd;
	public InputField inscriptionPasswdConf;



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
             *                          BOUTONS DE CONNEXION                                   *
             * **********************************************************************************/
            case "Confirm_Text":
                //change la couleur du bouton confirm en vert
                confirmButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;

            case "Back_Text":
                //change la couleur du bouton back en vert
                backButtonForm.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;
            /************************************************************************************
             *                          BOUTONS DE CONNEXION                                   *
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
            /************************************************************************************
             *                        BOUTONS DU TYPE DE PARTIE                                 *
             * **********************************************************************************/

            case "Back_Text_Menu":
                //change la couleur du bouton retour en vert
                backButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
                break;
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
             *                          BOUTONS DE CONNEXION                                   *
             * **********************************************************************************/
            case "Confirm_Text":
                //change la couleur du bouton confirm en blanc
                confirmButton.GetComponent<RawImage>().color = new Color(255, 0, 0);
                break;

            case "Back_Text":
                //change la couleur du bouton play_as_guest en blanc
                backButtonForm.GetComponent<RawImage>().color = new Color(255, 0, 0);
                break;
            /************************************************************************************
             *                          BOUTONS DE CONNEXION                                   *
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
            /************************************************************************************
             *                        BOUTONS DU TYPE DE PARTIE                                 *
             * **********************************************************************************/

            case "Back_Text_Menu":
                //change la couleur du bouton retour en vert
                backButton.GetComponent<RawImage>().color = new Color(255, 0, 0);
                break;
        }
    }


    //Schneberger Maxime
    //Quand l'utilisateur clique sur un object possédant un event trigger lié avec ce script
    public void OnPointer_Click()
    {
        switch (this.name)
        {
            /************************************************************************************
             *                          BOUTONS D'ENTREE                                        *
             * **********************************************************************************/
            case "Log_In_Text":
				GameObject.Find("Script_Source").GetComponent<menu_selection>().currentIndex = 1;
				GameObject.Find("Script_Source").GetComponent<menu_selection>().maxIndex = 2;
                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 1;
                break;

            case "Sign_Up_Text":
				GameObject.Find("Script_Source").GetComponent<menu_selection>().currentIndex = 1;
				GameObject.Find("Script_Source").GetComponent<menu_selection>().maxIndex = 3;
                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 1;
                break;

            case "Play_As_Guest_Text":
                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 3;
                GameObject.Find("Script_Source").GetComponent<menu_selection>().is_guest = true;
                break;
            /************************************************************************************
             *                          BOUTONS D'ENTREE                                        *
             * **********************************************************************************/




            /************************************************************************************
             *                          BOUTONS DE CONNEXION                                   *
             * **********************************************************************************/
            case "Confirm_Text":
                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 2;
                break;

            case "Back_Text":
                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 0;
                break;
            /************************************************************************************
             *                          BOUTONS DE CONNEXION                                   *
             * **********************************************************************************/





            /************************************************************************************
             *                        BOUTONS DU MENU PRINCIPAL                                 *
             * **********************************************************************************/
            case "Play_Text":
                soloButton.SetActive(true);
                multiButton.SetActive(true);

                playButton.SetActive(false);
                customizeButton.SetActive(false);
                profileButton.SetActive(false);

                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 3;
                break;

            case "Customize_Text":


                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 3;
                break;

            case "Profile_Text":
                userInfo.SetActive(true);
                userStats.SetActive(true);

                playButton.SetActive(false);
                customizeButton.SetActive(false);
                profileButton.SetActive(false);

                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 3;
                break;
            /************************************************************************************
             *                        BOUTONS DU MENU PRINCIPAL                                 *
             * **********************************************************************************/




            /************************************************************************************
             *                        BOUTONS DU TYPE DE PARTIE                                 *
             * **********************************************************************************/
            case "Solo_Text":
                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 4;
                SceneManager.LoadScene("trackCommon", LoadSceneMode.Single);
                SceneManager.LoadScene("Track1", LoadSceneMode.Additive);
                break;

            case "Multi_Text":
                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 4;
                soloButton.SetActive(false);
                multiButton.SetActive(false);

                multiplayerUI.SetActive(true);


                break;
            /************************************************************************************
             *                        BOUTONS DU TYPE DE PARTIE                                 *
             * **********************************************************************************/

            case "Back_Text_Menu":
                want_to_back(GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step);
                break;



            /************************************************************************************
             *                        BOUTONS DE L'ECRAN MULTIJOUEUR                            *
             * **********************************************************************************/
            //case "RoomCreationSubmitText":



            /************************************************************************************
             *                        BOUTONS DE L'ECRAN MULTIJOUEUR                            *
             * **********************************************************************************/
        }
    }


    //Schneberger Maxime
    //désactive les objets du menu sauf le bouton retour qui active cette fonction afin de réactiver les bons par la suite
    void disable_all()
    {
        logInButton.SetActive(false);
        logInButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
        signUpButton.SetActive(false);
        signUpButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
        playAsGuestButton.SetActive(false);
        playAsGuestButton.GetComponent<RawImage>().color = new Color(255, 255, 255);

        playButton.SetActive(false);
        playButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
        customizeButton.SetActive(false);
        customizeButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
        profileButton.SetActive(false);
        profileButton.GetComponent<RawImage>().color = new Color(255, 255, 255);

        soloButton.SetActive(false);
        soloButton.GetComponent<RawImage>().color = new Color(255, 255, 255);
        multiButton.SetActive(false);
        multiButton.GetComponent<RawImage>().color = new Color(255, 255, 255);

        //circuit1Button.SetActive(false);
        //circuit2Button.SetActive(false);
        //circuit3Button.SetActive(false);

        confirmButton.GetComponent<RawImage>().color = new Color(255, 0, 0);
        backButtonForm.GetComponent<RawImage>().color = new Color(255, 0, 0);

        userInfo.SetActive(false);
        userStats.SetActive(false);
    }

    //Schneberger Maxime
    //si l'utilisateur appuie sur retour, on revient un cran en arrière dans le menu
    void want_to_back(int step)
    {
        disable_all();
        switch(step)
        {
            case 2:
                logInButton.SetActive(true);
                signUpButton.SetActive(true);
                playAsGuestButton.SetActive(true);

                GameObject.Find("Play_As_Guest").GetComponent<RawImage>().enabled = true;
                GameObject.Find("Play_As_Guest_Text").GetComponent<Text>().enabled = true;
                GameObject.Find("Script_Source").GetComponent<menu_selection>().is_guest = false;
                break;

            case 3:
                if(!GameObject.Find("Script_Source").GetComponent<menu_selection>().is_guest)
                {
                    playButton.SetActive(true);
                    customizeButton.SetActive(true);
                    profileButton.SetActive(true);
                }
                else
                {
                    want_to_back(2);
                    GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step--;
                }
                break;

            case 4:
                soloButton.SetActive(true);
                multiButton.SetActive(true);
                break;

            
        }
        GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step--;
    }

    // Update is called once per frame
    void Update () {
		if (this.name == "Script_Source" && Input.GetKeyDown (KeyCode.Tab)) {
			if (GameObject.Find("Script_Source").GetComponent<menu_selection>().currentIndex < GameObject.Find("Script_Source").GetComponent<menu_selection>().maxIndex) {
				GameObject.Find("Script_Source").GetComponent<menu_selection>().currentIndex++;

				if (GameObject.Find("Script_Source").GetComponent<menu_selection>().maxIndex == 3) {
					if (GameObject.Find("Script_Source").GetComponent<menu_selection>().currentIndex == 1)
						inscriptionUser.ActivateInputField ();
					else if (GameObject.Find("Script_Source").GetComponent<menu_selection>().currentIndex == 2)
						inscriptionPasswd.ActivateInputField ();
					else if (GameObject.Find("Script_Source").GetComponent<menu_selection>().currentIndex == 3)
						inscriptionPasswdConf.ActivateInputField ();
				} 

				else if (GameObject.Find("Script_Source").GetComponent<menu_selection>().maxIndex == 2) {
					if (GameObject.Find("Script_Source").GetComponent<menu_selection>().currentIndex == 1)
						connexionUser.ActivateInputField ();
					else if (GameObject.Find("Script_Source").GetComponent<menu_selection>().currentIndex == 2)
						connexionPasswd.ActivateInputField ();
				}
			}
		}

		if (this.name == "Script_Source") {
			if (inscriptionUser.isFocused || connexionUser.isFocused)
				GameObject.Find ("Script_Source").GetComponent<menu_selection> ().currentIndex = 1;

			if (inscriptionPasswd.isFocused || connexionPasswd.isFocused)
				GameObject.Find ("Script_Source").GetComponent<menu_selection> ().currentIndex = 2;		

			if (inscriptionPasswdConf.isFocused)
				GameObject.Find ("Script_Source").GetComponent<menu_selection> ().currentIndex = 3;		
			
		}
    }
}
