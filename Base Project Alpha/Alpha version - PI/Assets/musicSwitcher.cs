using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicSwitcher : MonoBehaviour {

    public AudioClip euromir;
    public AudioClip sunset1;
    public AudioClip sunset2;
    public AudioClip adrenalise;

    // Use this for initialization
    void Start () {
        int rdm = Random.Range(1, 4);
        if (rdm == 1)
            this.GetComponent<AudioSource>().clip = euromir;
        else if (rdm == 2)
            this.GetComponent<AudioSource>().clip = sunset1;
        else if (rdm == 3)
            this.GetComponent<AudioSource>().clip = sunset2;
        else if (rdm == 4)
            this.GetComponent<AudioSource>().clip = adrenalise;

        this.GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
