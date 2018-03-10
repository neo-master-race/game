using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customisation_color_selection : MonoBehaviour {

    public Slider colorPicked;
    public Material gradient3D;

    private Color vehicle_color;

    // Use this for initialization
    void Start () {
		
	}
	
    public void OnPointerClick () {
        Debug.Log(this.name);
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

    // Update is called once per frame
    void Update () {
        vehicle_color=calculate_color_range(colorPicked.value);
        gradient3D.SetColor("_Color_TopR", vehicle_color);// colorpick_g.value * 255, colorpick_b.value * 255, 255));
    }
}
