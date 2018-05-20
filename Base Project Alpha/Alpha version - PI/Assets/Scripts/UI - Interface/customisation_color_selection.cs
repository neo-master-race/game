//Schneberger Maxime
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

    public Material StratosMainColor;
    public Material PorscheMainColor;
    public Material LamborghiniMainColor;
    //public Material PorscheMainColor;

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

    [Header("picker")]
    public float stratosValueSlider;
    public float porscheValueSlider;
    public float lamborghiniValueSlider;
    public Color stratosTopR;
    public Color porscheTopR;
    public Color lamborghiniTopR;
    public float stratosCursorX;
    public float stratosCursorY;
    public float porscheCursorX;
    public float porscheCursorY;
    public float lamborghiniCursorX;
    public float lamborghiniCursorY;

    [Header("back buttons")]
    public GameObject play_button;
    public GameObject customize_button;
    public GameObject profile_button;
    public GameObject customUI;
    public GameObject noncustomBackground;


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
        if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 1)
        {
            stratosValueSlider = colorPicked.value;
            stratosTopR = gradient3D.GetColor("_Color_TopR");
        }  
        else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 2)
        {
            porscheValueSlider = colorPicked.value;
            porscheTopR = gradient3D.GetColor("_Color_TopR");
        }
        else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 3)
        {
            lamborghiniValueSlider = colorPicked.value;
            lamborghiniTopR = gradient3D.GetColor("_Color_TopR");
        }

        if (arrow.gameObject.name=="LeftArrow")
            GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex--;
        else
            GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex++;

        

        disableAllCars();
        if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex==1)
        {
            Stratos.SetActive(true);
            leftArrow.SetActive(false);
            colorPicked.value = stratosValueSlider;
            gradient3D.SetColor("_Color_TopR", stratosTopR);
            cursorLocalPositionX = stratosCursorX;
            cursorLocalPositionY = stratosCursorY;
            /*Color32 stratosColor = StratosMainColor.color;
            float valueNewCar=calculate_slider_value(stratosColor);
            Debug.Log(valueNewCar);
            colorPicked.value = valueNewCar;*/

            //vehicle_color = calculate_color_range(valueNewCar);
            //gradient3D.SetColor("_Color_TopR", vehicle_color);
        }
        else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 2)
        {
            Porsche.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);

            colorPicked.value = porscheValueSlider;
            gradient3D.SetColor("_Color_TopR", porscheTopR);
            cursorLocalPositionX = porscheCursorX;
            cursorLocalPositionY = porscheCursorY;
        }
        else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 3)
        {
            Lamborghini.SetActive(true);
            rightArrow.SetActive(false);

            colorPicked.value = lamborghiniValueSlider;
            gradient3D.SetColor("_Color_TopR", lamborghiniTopR);
            cursorLocalPositionX = lamborghiniCursorX;
            cursorLocalPositionY = lamborghiniCursorY;
        }
        GameObject.Find("cursor_texture").transform.localPosition = new Vector2(cursorLocalPositionX, cursorLocalPositionY);
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
            if(GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 1)
                StratosMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
            else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 2)
                PorscheMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
            else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 3)
                LamborghiniMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
            GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().beginDrag = true;
            if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 1)
            {
                stratosValueSlider = colorPicked.value;
                stratosTopR = gradient3D.GetColor("_Color_TopR");
                stratosCursorX=cursorLocalPositionX;
                stratosCursorY=cursorLocalPositionY;
            }
            else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 2)
            {
                porscheValueSlider = colorPicked.value;
                porscheTopR = gradient3D.GetColor("_Color_TopR");
                porscheCursorX=cursorLocalPositionX;
                porscheCursorY = cursorLocalPositionY;
            }
            else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 3)
            {
                lamborghiniValueSlider = colorPicked.value;
                lamborghiniTopR = gradient3D.GetColor("_Color_TopR");
                lamborghiniCursorX = cursorLocalPositionX;
                lamborghiniCursorY = cursorLocalPositionY;
            }
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
            if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 1)
                StratosMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
            else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 2)
                PorscheMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
            else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 3)
                LamborghiniMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
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
            if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 1)
                StratosMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
            else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 2)
                PorscheMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
            else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 3)
                LamborghiniMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
        }

    }

    public void OnEndDrag(PointerEventData pointerData)
    {
        GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().beginDrag = false;
        if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 1)
        {
            stratosCursorX = cursorLocalPositionX;
            stratosCursorY = cursorLocalPositionY;
        }
        else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 2)
        {
            porscheCursorX = cursorLocalPositionX;
            porscheCursorY = cursorLocalPositionY;
        }
        else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 3)
        {
            lamborghiniCursorX = cursorLocalPositionX;
            lamborghiniCursorY = cursorLocalPositionY;
        }
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

    float calculate_slider_value(Color32 carColor)
    {
        if(carColor.b==0)
        {
            if (carColor.r >= 254)
            {
                return (float)(carColor.g / 255f) * 0.1666f;
            }   
            else
                return 0.1666f+(float)(carColor.r / 255f) * 0.1666f;
        }
        if (carColor.r == 0)
        {
            if (carColor.g >= 254)
                return 0.333f+(float)(carColor.b / 255f) * 0.1666f;
            else
                return 0.5f + (float)(carColor.g / 255f) * 0.1666f;
        }
        if (carColor.g == 0)
        {
            if (carColor.b >= 254)
                return 0.6666f+(float)(carColor.r/ 255f) * 0.1666f;
            else
                return 0.8333f + (float)(carColor.b / 255f) * 0.1666f;
        }
        return 0f;
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

    public void wannaSave ()
    {
        GameObject.Find("UserStats").GetComponent<UserStats>().Car1R = (int)((StratosMainColor.color.r)*255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().Car1G = (int)((StratosMainColor.color.g) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().Car1B = (int)((StratosMainColor.color.b) * 255f);

        GameObject.Find("UserStats").GetComponent<UserStats>().Car2R = (int)((PorscheMainColor.color.r) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().Car2G = (int)((PorscheMainColor.color.g) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().Car2B = (int)((PorscheMainColor.color.b) * 255f);

        GameObject.Find("UserStats").GetComponent<UserStats>().Car3R = (int)((LamborghiniMainColor.color.r) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().Car3G = (int)((LamborghiniMainColor.color.g) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().Car3B = (int)((LamborghiniMainColor.color.b) * 255f);

        //GameObject.Find("UserStats").GetComponent<UserStats>().Car4R = (int)((.color.r) * 255f);
        //GameObject.Find("UserStats").GetComponent<UserStats>().Car4G = (int)((StratosMainColor.color.g) * 255f);
        //GameObject.Find("UserStats").GetComponent<UserStats>().Car4B = (int)((StratosMainColor.color.b) * 255f);

        GameObject.Find("UserStats").GetComponent<UserStats>().stratosValueSlider = stratosValueSlider;
        GameObject.Find("UserStats").GetComponent<UserStats>().porscheValueSlider = porscheValueSlider;
        GameObject.Find("UserStats").GetComponent<UserStats>().lamborghiniValueSlider = lamborghiniValueSlider;
        //GameObject.Find("UserStats").GetComponent<UserStats>().fordValueSlider = fordValueSlider;

        GameObject.Find("UserStats").GetComponent<UserStats>().stratosTopRR= (int)((stratosTopR.r) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().stratosTopRG = (int)((stratosTopR.g) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().stratosTopRB = (int)((stratosTopR.b) * 255f);

        GameObject.Find("UserStats").GetComponent<UserStats>().porscheTopRR = (int)((porscheTopR.r) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().porscheTopRG = (int)((porscheTopR.g) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().porscheTopRB = (int)((porscheTopR.b) * 255f);

        GameObject.Find("UserStats").GetComponent<UserStats>().lamborghiniTopRR = (int)((lamborghiniTopR.r) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().lamborghiniTopRG = (int)((lamborghiniTopR.g) * 255f);
        GameObject.Find("UserStats").GetComponent<UserStats>().lamborghiniTopRB = (int)((lamborghiniTopR.b) * 255f);

        //GameObject.Find("UserStats").GetComponent<UserStats>().stratosTopRR = (int)((stratosTopR.r) * 255f);
        //GameObject.Find("UserStats").GetComponent<UserStats>().stratosTopRG = (int)((stratosTopR.g) * 255f);
        //GameObject.Find("UserStats").GetComponent<UserStats>().stratosTopRB = (int)((stratosTopR.b) * 255f);

        GameObject.Find("UserStats").GetComponent<UserStats>().stratosCursorX = stratosCursorX;
        GameObject.Find("UserStats").GetComponent<UserStats>().stratosCursorY = stratosCursorY;
        GameObject.Find("UserStats").GetComponent<UserStats>().porscheCursorX = porscheCursorX;
        GameObject.Find("UserStats").GetComponent<UserStats>().porscheCursorY = porscheCursorY;
        GameObject.Find("UserStats").GetComponent<UserStats>().lamborghiniCursorX = lamborghiniCursorX;
        GameObject.Find("UserStats").GetComponent<UserStats>().lamborghiniCursorY = lamborghiniCursorY;
        //GameObject.Find("UserStats").GetComponent<UserStats>().stratosCursorX = stratosCursorX;
        //GameObject.Find("UserStats").GetComponent<UserStats>().stratosCursorY = stratosCursorY;

        play_button.SetActive(true);
        customize_button.SetActive(true);
        profile_button.SetActive(true);

        customUI.SetActive(false);
        noncustomBackground.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        //appelle la fonction de changement de tonalité et de changement de couleur du véhicule à chaque frame pour avoir un changement adaptatif sans attente d'évènements par l'utilisateur
        vehicle_color=calculate_color_range(colorPicked.value);
        gradient3D.SetColor("_Color_TopR", vehicle_color);
        if(cursorLocalPositionX>=0 && cursorLocalPositionY>=0)
        {
            if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 1)
                StratosMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
            else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 2)
                PorscheMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
            else if (GameObject.Find("3D_Zone_Selection").GetComponent<customisation_color_selection>().carIndex == 3)
                LamborghiniMainColor.color = calculate_color_general(cursorLocalPositionX, cursorLocalPositionY);
        }
    }
}
