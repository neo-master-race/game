//SCHNEBERGER Maxime
//gère le système de checkpoint sur un circuit (modulable/adaptatif) selon le tracé et le nb de checkpoints voulu
//empêche le joueur de tricher sur la complétion d'un tour et l'oblige à suivre le tracé

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints_Check : MonoBehaviour
{

    [Header("Script Source requirement (0 for all on checkpoints object)")]
    public GameObject checkpointsParentScript;
    public GameObject[] checkpoints_collider;
    public bool[] wentThrough;
    public int lap_count = 1;
    public bool hasHitSFLineOnce;
    public int nextCheckpointNumber = 1;
    public int supposedNextCheckpointNumber = 1;

    [Header("Checkpoints variables (0 for all on script source)")]
    public bool isStartFinishLine;
    public int checkpointNumber;

    void OnTriggerEnter()
    {
        if (this.gameObject.GetComponent<Checkpoints_Check>().isStartFinishLine)
        {
            if (!checkpointsParentScript.GetComponent<Checkpoints_Check>().hasHitSFLineOnce)
                checkpointsParentScript.GetComponent<Checkpoints_Check>().hasHitSFLineOnce = true;
            else
            {
                int cp_count = 0;
                if (checkpointsParentScript.GetComponent<Checkpoints_Check>().nextCheckpointNumber == checkpointsParentScript.GetComponent<Checkpoints_Check>().supposedNextCheckpointNumber
                    && checkpointsParentScript.GetComponent<Checkpoints_Check>().nextCheckpointNumber == checkpointsParentScript.GetComponent<Checkpoints_Check>().wentThrough.Length)
                    checkpointsParentScript.GetComponent<Checkpoints_Check>().wentThrough[checkpointsParentScript.GetComponent<Checkpoints_Check>().wentThrough.Length - 1] = true;
                for (int i = 0; i <= checkpointsParentScript.GetComponent<Checkpoints_Check>().wentThrough.Length - 1; i++)
                {
                    if (checkpointsParentScript.GetComponent<Checkpoints_Check>().wentThrough[i] == true)
                        cp_count++;
                }
                if (cp_count == checkpointsParentScript.GetComponent<Checkpoints_Check>().wentThrough.Length)
                {
                    checkpointsParentScript.GetComponent<Checkpoints_Check>().lap_count++;
                    for (int j = 0; j <= checkpointsParentScript.GetComponent<Checkpoints_Check>().wentThrough.Length - 1; j++)
                        checkpointsParentScript.GetComponent<Checkpoints_Check>().wentThrough[j] = false;
                    checkpointsParentScript.GetComponent<Checkpoints_Check>().nextCheckpointNumber = 1;
                }
                checkpointsParentScript.GetComponent<Checkpoints_Check>().supposedNextCheckpointNumber = 1;
            }
        }
        else
        {
            if (checkpointsParentScript.GetComponent<Checkpoints_Check>().nextCheckpointNumber == this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber
                && checkpointsParentScript.GetComponent<Checkpoints_Check>().nextCheckpointNumber == checkpointsParentScript.GetComponent<Checkpoints_Check>().supposedNextCheckpointNumber)
            {
                checkpointsParentScript.GetComponent<Checkpoints_Check>().wentThrough[this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber - 1] = true;
                checkpointsParentScript.GetComponent<Checkpoints_Check>().nextCheckpointNumber++;
            }
            checkpointsParentScript.GetComponent<Checkpoints_Check>().supposedNextCheckpointNumber = this.gameObject.GetComponent<Checkpoints_Check>().checkpointNumber + 1;
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
