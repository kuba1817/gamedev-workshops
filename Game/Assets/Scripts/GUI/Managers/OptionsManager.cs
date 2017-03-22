using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class OptionsManager : MonoBehaviour {

    public UnityEvent MusicTurnedOn;
    public UnityEvent MusicTurnedOff;

    private ScreenSwitcher screenSwitcher;

    void Start()
    {
        screenSwitcher = ScreenSwitcher.Instance;
    }

    public void MusicToggleClicked(bool isOn)
    {
        if (isOn)
        {
            MusicTurnedOn.Invoke();
        }
        else
        {
            MusicTurnedOff.Invoke();
        }
    }

    public void Back()
    {
        screenSwitcher.SwitchTo(Screens.MainMenu);
    }
}
