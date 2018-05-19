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

	[Header("Track Selection")]
	public GameObject trackSelection;
	public GameObject trackConfirm;

    [Header("Car Selection")]
    public GameObject carSelection;
	public GameObject carConfirm;

	[Header("Back")]
	public GameObject backMenu;
	public GameObject canvas;
	public GameObject home;
	public GameObject quit;
	public GameObject cancel;

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

    [Header("Customisation")]
    public GameObject customUI;
	public GameObject noncustomBackground;



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
                backButton.GetComponent<RawImage>().color = new Color(0, 255, 0);
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


			/************************************************************************************
            *                              BOUTONS DE RETOUR                                    *
            * **********************************************************************************/


			case "Back_Text_Menu":
				//change la couleur du bouton retour en vert
				backButtonForm.GetComponent<RawImage>().color = new Color(0, 255, 0);
				break;

			case "Back_menu_Text_Menu":
				//change la couleur du bouton retour en vert
				backMenu.GetComponent<RawImage>().color = new Color(0, 255, 0);
				break;

			case "Logout_Text_Menu":
				//change la couleur du bouton deconnexion en vert
				home.GetComponent<RawImage>().color = new Color(0, 255, 0);
				break;

			case "Quit_Text_Menu":
				//change la couleur du bouton quitter en vert
				quit.GetComponent<RawImage>().color = new Color(0, 255, 0);
				break;

			case "Cancel_Text_Menu":
				//change la couleur du bouton annuler en vert
				cancel.GetComponent<RawImage>().color = new Color(0, 255, 0);
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
                backButton.GetComponent<RawImage>().color = new Color(255, 0, 0);
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


			/************************************************************************************
            *                              BOUTONS DE RETOUR                                    *
            * **********************************************************************************/


			case "Back_Text_Menu":
				//change la couleur du bouton retour en blanc
				backButtonForm.GetComponent<RawImage>().color = new Color(255, 255, 255);
				break;

			case "Back_menu_Text_Menu":
			//change la couleur du bouton retour en blanc
				backMenu.GetComponent<RawImage>().color = new Color(255, 255, 255);
				break;

			case "Logout_Text_Menu":
				//change la couleur du bouton deconnexion en blanc
				home.GetComponent<RawImage>().color = new Color(255, 255, 255);
				break;

			case "Quit_Text_Menu":
				//change la couleur du bouton quitter en blanc
				quit.GetComponent<RawImage>().color = new Color(255, 255, 255);
				break;

			case "Cancel_Text_Menu":
				//change la couleur du bouton annuler en blanc
				cancel.GetComponent<RawImage>().color = new Color(255, 255, 255);
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
                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 1;
				backButtonForm.SetActive (false);
                break;

            case "Sign_Up_Text":
                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 1;
				backButtonForm.SetActive (false);
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
				backButtonForm.SetActive (true);
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
                disable_all();
                customUI.SetActive(true);
                noncustomBackground.SetActive(false);

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
				trackSelection.SetActive(true);
				soloButton.SetActive(false);
				multiButton.SetActive(false);
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


			/************************************************************************************
			*                        BOUTON DE CONFIRMATION DU CIRCUIT                          *
			* **********************************************************************************/

			case "Confirm_Text_Menu":
				GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 5;
				trackSelection.SetActive(false);
                carSelection.SetActive(true);

                
				
				break;

            /************************************************************************************
            *                        BOUTON DE CONFIRMATION DU VEHICULE                         *
            * **********************************************************************************/

            case "ConfirmCar":
                GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 6;
                carSelection.SetActive(false);

                int nbTrack = trackConfirm.GetComponent<track_selection_form>().nbTrack;
                SceneManager.LoadScene("trackCommon", LoadSceneMode.Single);
				if (nbTrack == 1)
                    SceneManager.LoadScene("Track1", LoadSceneMode.Additive); 
				if (nbTrack == 2)
                    SceneManager.LoadScene("Track2", LoadSceneMode.Additive);
                if (nbTrack == 3)
                    SceneManager.LoadScene("Track3", LoadSceneMode.Additive);


                break;

			/************************************************************************************
            *                              BOUTONS DE RETOUR                                    *
            * **********************************************************************************/


			case "Back_Text_Menu":
				want_to_back(GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step);
				break;

			case "Back_menu_Text_Menu":
					disable_all ();
					GameObject.Find ("Script_Source").GetComponent<menu_selection> ().selection_step++;

					canvas.SetActive (true);
					home.SetActive (true);
					quit.SetActive (true);
					cancel.SetActive (true);
					break;

			case "Logout_Text_Menu":
				GameObject.Find("Script_Source").GetComponent<menu_selection> ().selection_step = 2;
				want_to_back(GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step);
				break;

			case "Quit_Text_Menu":
				want_to_back(0);
				break;

			case "Cancel_Text_Menu":
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

		trackSelection.SetActive(false);
        carSelection.SetActive(false);

        //circuit1Button.SetActive(false);
        //circuit2Button.SetActive(false);
        //circuit3Button.SetActive(false);

        confirmButton.GetComponent<RawImage>().color = new Color(255, 0, 0);
        backButton.GetComponent<RawImage>().color = new Color(255, 0, 0);

        userInfo.SetActive(false);
        userStats.SetActive(false);

        multiplayerUI.SetActive(false);

		canvas.SetActive(false);
		home.SetActive(false);
		quit.SetActive(false);
		cancel.SetActive(false);

		backButtonForm.GetComponent<RawImage>().color = new Color(255, 255, 255);
		backMenu.GetComponent<RawImage>().color = new Color(255, 255, 255);
		home.GetComponent<RawImage>().color = new Color(255, 255, 255);
		quit.GetComponent<RawImage>().color = new Color(255, 255, 255);
		cancel.GetComponent<RawImage>().color = new Color(255, 255, 255);
    }

    //Schneberger Maxime
    //si l'utilisateur appuie sur retour, on revient un cran en arrière dans le menu
    void want_to_back(int step)
	{
		if (GameObject.Find ("Script_Source").GetComponent<menu_selection> ().selection_step > 0) {
			disable_all ();
			switch (step) {
			case 1:
				logInButton.SetActive (true);
				signUpButton.SetActive (true);
				playAsGuestButton.SetActive (true);

				GameObject.Find ("Play_As_Guest").GetComponent<RawImage> ().enabled = true;
				GameObject.Find ("Play_As_Guest_Text").GetComponent<Text> ().enabled = true;
				GameObject.Find ("Script_Source").GetComponent<menu_selection> ().is_guest = false;
				break;

			case 2:
				logInButton.SetActive (true);
				signUpButton.SetActive (true);
				playAsGuestButton.SetActive (true);

				GameObject.Find ("Play_As_Guest").GetComponent<RawImage> ().enabled = true;
				GameObject.Find ("Play_As_Guest_Text").GetComponent<Text> ().enabled = true;
				GameObject.Find ("Script_Source").GetComponent<menu_selection> ().is_guest = false;
				GameObject.Find ("Script_Source").GetComponent<menu_selection> ().selection_step--;
				break;

			case 3:
				if (!GameObject.Find ("Script_Source").GetComponent<menu_selection> ().is_guest) {
					playButton.SetActive (true);
					customizeButton.SetActive (true);
					profileButton.SetActive (true);
				} else {
					want_to_back (2);
					GameObject.Find ("Script_Source").GetComponent<menu_selection> ().selection_step--;
				}
				break;

			case 4:
				disable_all ();
				soloButton.SetActive (true);
				multiButton.SetActive (true);
				break;

			case 5:
				disable_all ();
				trackSelection.SetActive (true);
				break;

			case 6:
				disable_all ();
				carSelection.SetActive (true);
				break;
	            
			}
			GameObject.Find ("Script_Source").GetComponent<menu_selection> ().selection_step--;
		}
		else
			Application.Quit ();
		
		if (GameObject.Find ("Script_Source").GetComponent<menu_selection> ().selection_step < 2)
			backMenu.SetActive (false);
		if (GameObject.Find ("Script_Source").GetComponent<menu_selection> ().selection_step == 1)
			backButtonForm.SetActive (false);
	}
}
