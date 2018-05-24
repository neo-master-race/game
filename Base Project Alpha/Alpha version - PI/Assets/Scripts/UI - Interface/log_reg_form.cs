using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class log_reg_form : MonoBehaviour {

    public GameObject log_reg_canvas;

    public GameObject display_error;

    public GameObject user_input;
    public GameObject pwd_input;
    public GameObject user_text;
    public GameObject pwd_text;

    public GameObject user_input_reg;
    public GameObject pwd_input_reg;
    public GameObject pwd_input_conf_reg;
    public GameObject user_text_reg;
    public GameObject pwd_text_reg;
    public GameObject pwd_text_conf_reg;



    public GameObject after_canvas;
    public GameObject register_button;
    public GameObject login_button;
    public GameObject play_as_guest_button;
    public GameObject play_button;
    public GameObject customize_button;
    public GameObject profile_button;
    public GameObject solo_button;
    public GameObject multi_button;
	public GameObject tuto_button;
	public GameObject confirm;
	public GameObject backButton;
	public GameObject backMenuButton;
    public GameObject carSelection;

    public Material stratosMainMat;
    public Material porscheMainMat;
    public Material lamboMainMat;
    public Material fordMainMat;

    // Use this for initialization
    void Start () {
        if (GameObject.Find("UserStats").GetComponent<UserStats>().isBack)
        {
            GameObject.Find("Script_Source").GetComponent<menu_selection>().selection_step = 3;
            login_button.SetActive(false);
            register_button.SetActive(false);
            play_as_guest_button.SetActive(false);

            backButton.SetActive(true);
            backMenuButton.SetActive(true);
            solo_button.SetActive(true);
            multi_button.SetActive(true);

			GameObject.Find ("TitleCanvas").SetActive (false);
			GameObject.Find("UserStats").GetComponent<UserStats>().isBack = false;
			GameObject.Find("UserStats").GetComponent<UserStats>().playingSolo = false;
			GameObject.Find("UserStats").GetComponent<UserStats>().playingMulti = false;
			GameObject.Find("UserStats").GetComponent<UserStats>().isOnRoomList = false;
			GameObject.Find("UserStats").GetComponent<UserStats>().isOnLobby = false;
			GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb = 0;
			GameObject.Find("UserStats").GetComponent<UserStats>().trackLapNumber = 0;
        }

    }

	public void ErrorOff () {
		display_error.SetActive(false);
	}

    public void setvehicle ()
    {
        GameObject.Find("UserStats").GetComponent<UserStats>().carIndex = carSelection.GetComponent<carSelection>().selectedVehicule+1;
    }

    public void setTrack(GameObject track)
    {
        GameObject.Find("UserStats").GetComponent<UserStats>().onTrackNb = track.GetComponent<track_selection_form>().nbTrack;
        GameObject.Find("UserStats").GetComponent<UserStats>().trackLapNumber = track.GetComponent<track_selection_form>().nbLaps;
        GameObject.Find("UserStats").GetComponent<UserStats>().playingSolo = true;
    }

    public void playMulti()
    {
        GameObject.Find("UserStats").GetComponent<UserStats>().playingMulti = true;
        GameObject.Find("UserStats").GetComponent<UserStats>().trackLapNumber = 3;
    }

    public void setcolor ()
    {
        stratosMainMat.color = new Color(
            GameObject.Find("UserStats").GetComponent<UserStats>().Car1R / 255f,
            GameObject.Find("UserStats").GetComponent<UserStats>().Car1G / 255f,
            GameObject.Find("UserStats").GetComponent<UserStats>().Car1B / 255f
            );
        porscheMainMat.color = new Color(
            GameObject.Find("UserStats").GetComponent<UserStats>().Car2R / 255f,
            GameObject.Find("UserStats").GetComponent<UserStats>().Car2G / 255f,
            GameObject.Find("UserStats").GetComponent<UserStats>().Car2B / 255f
            );
        lamboMainMat.color = new Color(
            GameObject.Find("UserStats").GetComponent<UserStats>().Car3R / 255f,
            GameObject.Find("UserStats").GetComponent<UserStats>().Car3G / 255f,
            GameObject.Find("UserStats").GetComponent<UserStats>().Car3B / 255f
            );
        fordMainMat.color = new Color(
            GameObject.Find("UserStats").GetComponent<UserStats>().Car4R / 255f,
            GameObject.Find("UserStats").GetComponent<UserStats>().Car4G / 255f,
            GameObject.Find("UserStats").GetComponent<UserStats>().Car4B / 255f
            );
    }

    public IEnumerator go_to_menu(String message, bool is_guest)
    {

        after_canvas.SetActive(true);
        GameObject.Find("Success_Register").GetComponent<Text>().enabled = false;
        GameObject.Find("Success_Login").GetComponent<Text>().enabled = true;
        GameObject.Find("Success_Login").GetComponent<Text>().text = message;
        log_reg_canvas.SetActive(false);
        backButton.SetActive(false);
		backMenuButton.SetActive(false);
        yield return new WaitForSeconds(3.0f);

		GameObject.Find("Success_Login").GetComponent<Text>().enabled = false;
        after_canvas.SetActive(false);
        backButton.SetActive(true);
		backMenuButton.SetActive(true);

        if (!is_guest)
        {
            play_button.SetActive(true);
            customize_button.SetActive(true);
            profile_button.SetActive(true);
        }
        else
        {
            solo_button.SetActive(true);
            multi_button.SetActive(true);
            tuto_button.SetActive(true);
        }


        
    }

    public void towwwlog()
    {
        after_canvas.SetActive(true);
        GameObject.Find("Success_Register").GetComponent<Text>().enabled = false;
        GameObject.Find("Success_Login").GetComponent<Text>().enabled = true;
        GameObject.Find("Success_Login").GetComponent<Text>().text = "Connection...";
        log_reg_canvas.SetActive(false);

        StartCoroutine(go_to_menu("Vous vous êtes correctement identifié\nVous allez être redirigé vers le menu", false));
    }

    public IEnumerator towwwsign()
    {
        after_canvas.SetActive(true);
        GameObject.Find("Success_Register").GetComponent<Text>().enabled = true;
        GameObject.Find("Success_Login").GetComponent<Text>().enabled = false;
        GameObject.Find("Success_Register").GetComponent<Text>().text = "Création de votre compte...";
        log_reg_canvas.SetActive(false);
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(go_to_menu("Vous vous êtes correctement inscrit\nVous allez être redirigé vers le menu", false));
    }

    public IEnumerator displayError(String msg)
    {
        log_reg_canvas.SetActive(true);
        user_input.SetActive(true);
        pwd_input.SetActive(true);
		confirm.GetComponent<RawImage>().color = new Color(255, 255, 255);
        user_text.GetComponent<Text>().enabled = true;
        pwd_text.GetComponent<Text>().enabled = true;
        display_error.SetActive(true);
        GameObject.Find("ErrorDisplayText").GetComponent<Text>().enabled = true;
        GameObject.Find("ErrorDisplayText").GetComponent<Text>().text = msg;
        yield return new WaitForSeconds(3.0f);
		if (display_error.activeSelf)
        	GameObject.Find("ErrorDisplayText").GetComponent<Text>().enabled = false;
        display_error.SetActive(false);


    }


    public IEnumerator displayErrorReg(String msg)
    {
        log_reg_canvas.SetActive(true);
        user_input_reg.SetActive(true);
        pwd_input_reg.SetActive(true);
        pwd_input_conf_reg.SetActive(true);
        confirm.GetComponent<RawImage>().color = new Color(255, 255, 255);
        user_text_reg.GetComponent<Text>().enabled = true;
        pwd_text_reg.GetComponent<Text>().enabled = true;
        pwd_text_conf_reg.GetComponent<Text>().enabled = true;
        display_error.SetActive(true);
        GameObject.Find("ErrorDisplayText").GetComponent<Text>().enabled = true;
        GameObject.Find("ErrorDisplayText").GetComponent<Text>().text = msg;
        yield return new WaitForSeconds(3.0f);
        if (display_error.activeSelf)
            GameObject.Find("ErrorDisplayText").GetComponent<Text>().enabled = false;
        display_error.SetActive(false);


    }

    public void LogInSuccess()
    {
        Debug.Log("New user connected");
        GameObject.Find("UserStats").GetComponent<UserStats>().username = GameObject.Find("Network").GetComponent<Network>().username;
        towwwlog();
    }

    public void LogInError(String msg)
    {
        Debug.Log(msg);
        StartCoroutine(displayError(msg));
    }

    public void RegisterSuccess()
    {
        Debug.Log("New user registered");
        GameObject.Find("UserStats").GetComponent<UserStats>().username = GameObject.Find("Network").GetComponent<Network>().getClientName();
        StartCoroutine(towwwsign());
    }

    public void RegisterError(String msg)
    {
        Debug.Log(msg);
        StartCoroutine(displayErrorReg(msg));

    }

    // Update is called once per frame
    void Update () {
		
	}
}
