using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class ChangeScene : MonoBehaviour
{
    public GameObject inGame;

	void Start ()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        DontDestroyOnLoad(inGame); 
	}

    void OnClick()
    {
        SceneManager.LoadScene("track1", LoadSceneMode.Single);
    }
}
