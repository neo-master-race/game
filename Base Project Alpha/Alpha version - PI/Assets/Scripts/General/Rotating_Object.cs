using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating_Object : MonoBehaviour {

    private bool is_chosed = false;
    private float timer = 1.5f;

    [Header("Vitesses de rotation")]
    public float initialRotateSpeed = 1.0f;
    public float secondaryRotateSpeed = 30.0f;

    // Use this for initialization
    void Start () {
		
	}
	
    IEnumerator Vehicle_selected () {
        
        while (timer>0)
        {
            
            
        }
        yield return "test";
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("space"))
            is_chosed = true;
        if(!is_chosed)
            transform.Rotate(0, initialRotateSpeed, 0);
        else
        {
            timer -= Time.deltaTime;
            if (timer > 0)
                transform.Rotate(0, secondaryRotateSpeed, 0);
            else
                transform.Rotate(0, initialRotateSpeed, 0);
        }

    }
}
