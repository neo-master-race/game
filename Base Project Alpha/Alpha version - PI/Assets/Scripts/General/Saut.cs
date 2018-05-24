using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saut : MonoBehaviour {
   
    public GameObject respawn;

    private void OnTriggerEnter(Collider other)
    {/*
        if (other.tag == "Player")
        {
            if(respawn!=null)
            {
                CarController playerController = other.GetComponent<CarController>();
                Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
                //playerRigidbody.velocity = Vector3.zero;
                other.transform.position = respawn.transform.position;
                //playerRigidbody.velocity = Vector3.zero;
                playerRigidbody.AddForceAtPosition(other.transform.up * playerController.gravityForce*4, other.transform.position);
                playerRigidbody.AddForce(other.transform.forward * 25000);
            }
        }*/
    }
}
