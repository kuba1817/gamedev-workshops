using UnityEngine;
using System.Collections;
using Facebook.Unity;

public class MainMenuManager : MonoBehaviour {

    private ScreenSwitcher screenSwitcher;
    private FacebookManager facebookManager;

    void Start()
    {
        screenSwitcher = ScreenSwitcher.Instance;
        facebookManager = FacebookManager.Instance;
    }

	public void Play()
    {

    }

    public void Options()
    {
        screenSwitcher.SwitchTo(Screens.Options);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowFacebookUserInfo()
    {
        if (FB.IsLoggedIn)
        {
            PopUpManager.Instance.ShowPopUp("Info", facebookManager.User.FirstName + " " + facebookManager.User.LastName);
        }
        else
        {
            PopUpManager.Instance.ShowPopUp("Niestety!", "Nie jestes zalogowany!");
        }
    }

    public void FacebookLogin()
    {
        facebookManager.Login();
    }

    public void FacebookSharePost()
    {
        facebookManager.SharePost();
    }
}
