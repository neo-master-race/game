using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour {

    public GameObject player;
    private Rigidbody playerBody;

    void Start()
    {
        playerBody = player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" && playerBody!=null)
        {
            playerBody.constraints = RigidbodyConstraints.FreezePositionY;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall" && playerBody != null)
        {
            playerBody.constraints = RigidbodyConstraints.None;
        }
    }
}
