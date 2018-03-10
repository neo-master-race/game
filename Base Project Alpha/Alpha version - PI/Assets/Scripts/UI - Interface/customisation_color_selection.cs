using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class customisation_color_selection : MonoBehaviour, IPointerDownHandler
{

    public Slider colorPicked;
    public Material gradient3D;

    public Material vehicleMainColor;

    private Color vehicle_color;

    // Use this for initialization
    void Start () {

	}
	
    public void OnPointerDown (PointerEventData pointerData) {
        if(pointerData.position.x>=512 && pointerData.position.x <= 712 && pointerData.position.y >= 484 && pointerData.position.y <= 684)
        {
            GameObject.Find("cursor_texture").transform.localPosition = new Vector3(pointerData.position.x - 512, pointerData.position.y - 484, 0);
            vehicleMainColor.color = calculate_color_general(pointerData.position.x-512, pointerData.position.y-484);
        }
    }

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
        vehicle_color=calculate_color_range(colorPicked.value);
        gradient3D.SetColor("_Color_TopR", vehicle_color);// colorpick_g.value * 255, colorpick_b.value * 255, 255));
    }
}
