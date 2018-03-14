using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voiture : MonoBehaviour {

  public bool isLocalPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if (isLocalPlayer) {
      float verticalAxis = Input.GetAxis("Vertical");
  		float horizontalAxis = Input.GetAxis("Horizontal");

      Vector3 vec = transform.localPosition;
      Vector3 vecRot = transform.localEulerAngles;

      vecRot.y += horizontalAxis;
      vec += verticalAxis * transform.forward;

      transform.localPosition = vec;
      transform.localEulerAngles = vecRot;

      if (verticalAxis != 0 || horizontalAxis != 0) {
        Debug.Log(transform.position);
        Debug.Log(transform.rotation);
        Debug.Log(transform.localScale);
      }
    }
	}
}
