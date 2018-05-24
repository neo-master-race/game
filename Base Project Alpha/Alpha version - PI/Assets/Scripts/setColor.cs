using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setColor : MonoBehaviour {

    public RawImage color1;
    public RawImage color2;
    public RawImage color3;
    public RawImage color4;

    public Material color1Material;
    public Material color2Material;
    public Material color3Material;
    public Material color4Material;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        color1.color = color1Material.color;
        color2.color = color2Material.color;
        color3.color = color3Material.color;
        color4.color = color4Material.color;
    }
}
