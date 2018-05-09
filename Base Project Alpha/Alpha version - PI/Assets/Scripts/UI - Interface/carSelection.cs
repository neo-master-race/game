using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class carSelection : MonoBehaviour {

    public GameObject vehiculeNameText;
    public GameObject selectionGoLeft;
    public GameObject selectionGoRight;

    private int selectedVehicule = 0;
    private int totalVehiculeNumber = 4;

    public void uptText(int selected)
    {
        switch (selected)
        {
            case 0:
                vehiculeNameText.GetComponent<Text>().text = "Stratos";
                break;
            case 1:
                vehiculeNameText.GetComponent<Text>().text = "Porsche";
                break;
            case 2:
                vehiculeNameText.GetComponent<Text>().text = "Lamborghini";
                break;
            case 3:
                vehiculeNameText.GetComponent<Text>().text = "Ford";
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
