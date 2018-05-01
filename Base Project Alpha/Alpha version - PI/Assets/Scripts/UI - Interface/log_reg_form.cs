using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class log_reg_form : MonoBehaviour {

    public GameObject log_reg_canvas;
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
    public GameObject backButton;

    // Use this for initialization
    void Start () {
		
	}

    public IEnumerator go_to_menu(String message, bool is_guest)
    {

        after_canvas.SetActive(true);
        GameObject.Find("Success_Register").GetComponent<Text>().enabled = false;
        GameObject.Find("Success_Login").GetComponent<Text>().enabled = true;
        GameObject.Find("Success_Login").GetComponent<Text>().text = message;
        log_reg_canvas.SetActive(false);
        backButton.SetActive(false);
        yield return new WaitForSeconds(3.0f);


        after_canvas.SetActive(false);
        backButton.SetActive(true);

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

    public void LogInSuccess()
    {
        Debug.Log("New user connected");
        GameObject.Find("UserStats").GetComponent<UserStats>().username = GameObject.Find("Network").GetComponent<Network>().username;
        towwwlog();
    }

    public void LogInError(String msg)
    {
        Debug.Log(msg);
    }

    public void RegisterSuccess()
    {
        Debug.Log("New user registered");
        GameObject.Find("UserStats").GetComponent<UserStats>().username = GameObject.Find("Network").GetComponent<Network>().username;
        StartCoroutine(towwwsign());
    }

    public void RegisterError(String msg)
    {
        Debug.Log(msg);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
