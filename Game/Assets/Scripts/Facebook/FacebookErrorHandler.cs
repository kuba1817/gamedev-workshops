using UnityEngine;
using System.Collections;

public class FacebookErrorHandler : MonoBehaviour {

	public void LoginFailed()
    {
        PopUpManager.Instance.ShowPopUp("Niestety!", "Nie udalo sie zalogowac!");
    }
}
