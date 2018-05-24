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

    public GameObject cursor1;
    public GameObject cursor2;
    public GameObject cursor3;
    public GameObject cursor4;

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
                cursor1.GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 0);
                cursor2.GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 0);
                cursor3.GetComponent<RectTransform>().anchoredPosition = new Vector2(60, 0);
                cursor4.GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 0);
                break;
            case 1:
                vehiculeNameText.GetComponent<Text>().text = "Porsche";
                stratos.SetActive(false);
                porsche.SetActive(true);
                lambo.SetActive(false);
                ford.SetActive(false);
                cursor1.GetComponent<RectTransform>().anchoredPosition = new Vector2(120, 0);
                cursor2.GetComponent<RectTransform>().anchoredPosition = new Vector2(40, 0);
                cursor3.GetComponent<RectTransform>().anchoredPosition = new Vector2(40, 0);
                cursor4.GetComponent<RectTransform>().anchoredPosition = new Vector2(60, 0);
                break;
            case 2:
                vehiculeNameText.GetComponent<Text>().text = "Lamborghini";
                stratos.SetActive(false);
                porsche.SetActive(false);
                lambo.SetActive(true);
                ford.SetActive(false);
                cursor1.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 0);
                cursor2.GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 0);
                cursor3.GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 0);
                cursor4.GetComponent<RectTransform>().anchoredPosition = new Vector2(60, 0);
                break;
            case 3:
                vehiculeNameText.GetComponent<Text>().text = "Ford";
                stratos.SetActive(false);
                porsche.SetActive(false);
                lambo.SetActive(false);
                ford.SetActive(true);
                cursor1.GetComponent<RectTransform>().anchoredPosition = new Vector2(60, 0);
                cursor2.GetComponent<RectTransform>().anchoredPosition = new Vector2(120, 0);
                cursor3.GetComponent<RectTransform>().anchoredPosition = new Vector2(80, 0);
                cursor4.GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 0);
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
