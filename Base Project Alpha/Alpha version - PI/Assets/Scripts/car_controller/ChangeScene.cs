using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class ChangeScene : MonoBehaviour
{
    
	void Start ()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick); 
	}

    void OnClick()
    {
        SceneManager.LoadScene("track1", LoadSceneMode.Single);
    }
}
