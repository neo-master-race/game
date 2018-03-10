using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boost : MonoBehaviour {

    public float multiplicator = 0.2f;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player" )
        {
            CarController playerController=other.GetComponent<CarController>();
            if (playerController != null)
            {
                playerController.boost(multiplicator);
            }
        }
    }
}
