using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class ChangeScene : MonoBehaviour
{
    public void ByName(string track)
    {
        SceneManager.LoadScene("trackCommon", LoadSceneMode.Single);
        SceneManager.LoadScene(track, LoadSceneMode.Additive);

        GameObject network = GameObject.Find("Network");
        GameObject trackInterface = GameObject.Find("PrefabInterface");

        network.GetComponent<Network>().carsContainer = GameObject.Find("Cars"); 
    }
}
