﻿//Schneberger Maxime
//10-03-18

//gère la customisation des véhicule
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class customisation_color_selection : MonoBehaviour, IPointerDownHandler,IDragHandler, IEndDragHandler
{
    public Slider colorPicked;
    public Material gradient3D;

    public Material vehicleMainColor;

    private Color vehicle_color;

    private float cursorPositionX=-1f;
    private float cursorPositionY=-1f;

    private float cursorLocalPositionX = -1f;
    private float cursorLocalPositionY = -1f;

    public bool beginDrag = false;

    [Header("arrow variables")]
    public int carIndex = 1;
    public GameObject leftArrow;
    public GameObject rightArrow;

    [Header("Cars")]
    public GameObject Stratos;
    public GameObject Porsche;
    public GameObject Lamborghini;


    // Use this for initialization
    void Start () {
        
    }

    void disableAllCars()
    {
        Stratos.SetActive(false);
        Porsche.SetActive(false);
        Lamborghini.SetActive(false);
        //Stratos.SetActive(false);
    }

    public void onArrowClick(GameObject arrow)
    {
        if(arrow.gameObject.name=="LeftArrow")
            GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex--;
        else
            GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex++;

        disableAllCars();
        if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex==1)
        {
            Stratos.SetActive(true);
            leftArrow.SetActive(false);
        }
        else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 2)
        {
            Porsche.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
        }
        else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 3)
        {
            Lamborghini.SetActive(true);
            rightArrow.SetActive(false);
        }
        /*else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 4)
        {
            Stratos.SetActive(true);
            leftArrow.SetActive(false);
        }*/
    }
	
    //Lors d'un clic sur un élément affecté par ce script, envoie la position de la souris dans la fonction de changement de couleur du véhicule pour en changer la couleur
    public void OnPointerDown (PointerEventData pointerData) {

        if(pointerData.pointerEnter.name== "3D_Zone_Selection")
        {
            cursorPositionX = pointerData.position.x;
            cursorPositionY = pointerData.position.y;
            GameObject.Find("cursor_texture").transform.position = new Vector3(cursorPositionX, cursorPositionY);
            cursorLocalPositionX = GameObject.Find("cursor_texture").transform.localPosition.x;
            cursorLocalPositionY = GameObject.Find("cursor_texture").transform.localPosition.y;
            vehicleMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
            GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().beginDrag = true;
        }

    }

    public void OnDrag(PointerEventData pointerData)
    {

        if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().beginDrag == true && pointerData.pointerEnter.name == "3D_Zone_Selection")
        {
            cursorPositionX = pointerData.position.x;
            cursorPositionY = pointerData.position.y;
            GameObject.Find("cursor_texture").transform.position = new Vector3(cursorPositionX, cursorPositionY);
            cursorLocalPositionX = GameObject.Find("cursor_texture").transform.localPosition.x;
            cursorLocalPositionY = GameObject.Find("cursor_texture").transform.localPosition.y;
            vehicleMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
        }
        else if(GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().beginDrag == true)
        {
            Vector3[] v = new Vector3[4];
            GameObject.Find("3D_Zone_Selection").GetComponent<RectTransform>().GetWorldCorners(v);
            for (var i = 0; i < 4; i++)
            {
                Debug.Log("World Corner " + i + " : " + v[i]);
            }
            if (pointerData.position.x < v[0].x)
                cursorPositionX = v[0].x;
            else if (pointerData.position.x > v[2].x)
                cursorPositionX = v[2].x;
            else
                cursorPositionX = pointerData.position.x;

            if (pointerData.position.y < v[0].y)
                cursorPositionY = v[0].y;
            else if (pointerData.position.y > v[2].y)
                cursorPositionY = v[2].y;
            else
                cursorPositionY = pointerData.position.y;
            GameObject.Find("cursor_texture").transform.position = new Vector3(cursorPositionX, cursorPositionY);
            cursorLocalPositionX = GameObject.Find("cursor_texture").transform.localPosition.x;
            cursorLocalPositionY = GameObject.Find("cursor_texture").transform.localPosition.y;
            vehicleMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
        }

    }

    public void OnEndDrag(PointerEventData pointerData)
    {
        GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().beginDrag = false;
    }

    //suivant la position du slider sur le gradient 2D choisi par l'utilisateur change les paramètres du gradient (carré) 3D pour changer la tonalité de couleur en temps réel
    Color calculate_color_range(float value)
    {
        if (value <= 0.1666)
            return new Color(1, (value%0.1666f)*6, 0, 1);
        else if(value <= 0.3333)
            return new Color(1-((value % 0.1666f) * 6), 1, 0, 1);
        else if (value <= 0.5)
            return new Color(0, 1, (value % 0.1666f) * 6, 1);
        else if (value <= 0.6666)
            return new Color(0, 1-((value % 0.1666f) * 6), 1, 1);
        else if (value <= 0.8333)
            return new Color((value % 0.1666f) * 6, 0, 1, 1);
        else
            return new Color(1, 0, 1-((value % 0.1667f) * 6), 1);
    }

    //calcule la couleur sélectionnée par l'utilisateur suivant la position du curseur sur la zone du gradient 3D (unity ne permet pas de simplement repéré la couleur sur laquelle a cliqué le curseur)
    Color calculate_color_general(float x, float y)
    {

        float vehicle_color2_x = (x / 200) * (vehicle_color.r + (1 - (y/200)) * (1 - vehicle_color.r));
        float vehicle_color2_y = (x / 200) * (vehicle_color.g + (1 - (y / 200)) * (1 - vehicle_color.g));
        float vehicle_color2_z = (x / 200) * (vehicle_color.b + (1 - (y / 200)) * (1 - vehicle_color.b));

        Color vehicle_color2 = new Color(vehicle_color2_x, vehicle_color2_y, vehicle_color2_z);
        return vehicle_color2;
    }

    // Update is called once per frame
    void Update () {
        //appelle la fonction de changement de tonalité et de changement de couleur du véhicule à chaque frame pour avoir un changement adaptatif sans attente d'évènements par l'utilisateur
        vehicle_color=calculate_color_range(colorPicked.value);
        gradient3D.SetColor("_Color_TopR", vehicle_color);
        if(cursorLocalPositionX>=0 && cursorLocalPositionY>=0)
            vehicleMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
    }
}
