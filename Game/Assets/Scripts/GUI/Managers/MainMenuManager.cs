using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

    private ScreenSwitcher screenSwitcher;

    void Start()
    {
        screenSwitcher = ScreenSwitcher.Instance;
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
}
