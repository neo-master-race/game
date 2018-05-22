using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

    public bool setGravity = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody playerController = other.GetComponent<Rigidbody>();
            if (playerController != null)
            {
                if(setGravity)
                {
                    playerController.constraints = RigidbodyConstraints.FreezePositionY;
                }
                else
                {
                    playerController.constraints = RigidbodyConstraints.None;
                }
            }
        }
    }
}
