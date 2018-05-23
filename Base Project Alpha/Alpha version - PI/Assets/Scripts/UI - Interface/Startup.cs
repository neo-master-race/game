using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startup : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //Pour que l'orientation de la tablette ne change pas
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
