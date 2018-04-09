using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class waiting_opponents : MonoBehaviour {

    public bool hasStartedProcedure = false;

    public List<int> placement_int = new List<int>();

    // Use this for initialization
    void Start () {
        //Debug.Log(GameObject.Find("2nd_Place").transform.position);
        //GameObject.Find("Stratos_AI_Position").transform.position = new Vector3(GameObject.Find("2nd_Place").transform.position.x, 0.67f, GameObject.Find("2nd_Place").transform.position.z+2f);
    }

    void set_position ()
    {
        int random = Random.Range(1, (GameObject.Find("Race_Control").GetComponent<player_info_server>().players.Length + 1));
        if (placement_int.Contains(random))
            set_position();
        else
            placement_int.Add(random);
    }

    // Update is called once per frame
    void Update () {
        
		if(!hasStartedProcedure && GameObject.Find("Race_Control").GetComponent<player_info_server>().players.Length>=2)
        {
            for(int i=0;i< GameObject.Find("Race_Control").GetComponent<player_info_server>().players.Length; i++)
            {
                set_position();
                GameObject.Find("Race_Control").GetComponent<player_info_server>().players[i].transform.position= 
                    new Vector3(GameObject.Find("Place" + placement_int[i]).transform.position.x,
                    0.67f, 
                    GameObject.Find("Place"+placement_int[i]).transform.position.z + 2f);
            }
            hasStartedProcedure = true;
        }
	}
}
