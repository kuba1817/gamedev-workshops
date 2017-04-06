using UnityEngine;
using System.Collections;
using System;

public class PopUpManager : Singleton<PopUpManager> {

    public Transform canvas;
    public GameObject popUp;
    
    public void ShowPopUp(string title, string text, Action action)
    {
        ShowGivenPopUp(title, text, popUp, action);
    }

    public void ShowPopUp(string title, string text)
    {
        ShowGivenPopUp(title, text, popUp);
    }

    private void ShowGivenPopUp(string title, string text, GameObject popUp, Action action = null)
    {
        GameObject instantiatedPopUp = Instantiate(popUp, transform.position, transform.rotation) as GameObject;
        instantiatedPopUp.GetComponent<PopUp>().SetValues(title, text, action);
        instantiatedPopUp.transform.SetParent(canvas, false);
        instantiatedPopUp.transform.localPosition = new Vector3(0, 0, 0);
        instantiatedPopUp.SetActive(true);
    }
}
