using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class carSelection : MonoBehaviour {

    public GameObject vehiculeNameText;
    public GameObject selectionGoLeft;
    public GameObject selectionGoRight;
    public GameObject carPodium;
    public GameObject stratos;
    public GameObject porsche;
    public GameObject lambo;
    public GameObject ford;

    public int selectedVehicule = 0;
    private int totalVehiculeNumber = 4;

    public void uptText(int selected)
    {
        switch (selected)
        {
            case 0:
                vehiculeNameText.GetComponent<Text>().text = "Stratos";
                stratos.SetActive(true);
                porsche.SetActive(false);
                lambo.SetActive(false);
                ford.SetActive(false);
                break;
            case 1:
                vehiculeNameText.GetComponent<Text>().text = "Porsche";
                stratos.SetActive(false);
                porsche.SetActive(true);
                lambo.SetActive(false);
                ford.SetActive(false);
                break;
            case 2:
                vehiculeNameText.GetComponent<Text>().text = "Lamborghini";
                stratos.SetActive(false);
                porsche.SetActive(false);
                lambo.SetActive(true);
                ford.SetActive(false);
                break;
            case 3:
                vehiculeNameText.GetComponent<Text>().text = "Ford";
                stratos.SetActive(false);
                porsche.SetActive(false);
                lambo.SetActive(false);
                ford.SetActive(true);
                break;

            default:
                vehiculeNameText.GetComponent<Text>().text = "Vehicule 1";
                break;
        }
    }



    public void OnPointer_Click(GameObject button)
    {
        switch (button.gameObject.name)
        {

            case "LeftArrow":
                if (selectedVehicule == 0)
                {
                    selectedVehicule = totalVehiculeNumber - 1;
                }
                else
                {
                    selectedVehicule--;
                }
                uptText(selectedVehicule);
                break;

            case "RightArrow":
                if (selectedVehicule == totalVehiculeNumber - 1)
                {
                    selectedVehicule = 0;
                }
                else
                {
                    selectedVehicule++;
                }

                uptText(selectedVehicule);
                break;
            default:
                break;
        }
    }

    void Start()
    {
        uptText(selectedVehicule);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
