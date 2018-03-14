using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;

public class Voiture : MonoBehaviour {

  public bool isLocalPlayer;
  private int limiter = 0;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    limiter = limiter++ % 50;
    if (limiter == 0) {
      if (isLocalPlayer) {
        float verticalAxis = Input.GetAxis("Vertical");
    		float horizontalAxis = Input.GetAxis("Horizontal") * 5 * System.Math.Abs(verticalAxis);

        Vector3 vec = transform.localPosition;
        Vector3 vecRot = transform.localEulerAngles;

        vecRot.y += horizontalAxis;
        vec += verticalAxis * transform.forward;

        transform.localPosition = vec;
        transform.localEulerAngles = vecRot;

        // if the player moved, send his nex position
        if (verticalAxis != 0 || horizontalAxis != 0) {
          updatePlayerPosition();
        }
      }
    }
  }

  private void updatePlayerPosition() {

    // vectors that we need to send
    Protocol.Vector vecPosition = new Protocol.Vector{
      X = transform.position.x,
      Y = transform.position.y,
      Z = transform.position.z
    };
    Protocol.Vector vecRotation = new Protocol.Vector{
      X = transform.rotation.x,
      Y = transform.rotation.y,
      Z = transform.rotation.z
    };
    Protocol.Vector vecScale = new Protocol.Vector{
      X = transform.localScale.x,
      Y = transform.localScale.y,
      Z = transform.localScale.z
    };

    GameObject.Find("Network").GetComponent<Network>().updatePlayerPosition(vecPosition, vecRotation, vecScale);
  }

}
