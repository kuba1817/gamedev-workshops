using UnityEngine;
using System.Collections;

public class ChooseLanguageManager : MonoBehaviour {

    private LanguageManager languageManager;
    private ScreenSwitcher screenSwitcher;

    void Start()
    {
        languageManager = LanguageManager.Instance;
        screenSwitcher = ScreenSwitcher.Instance;
    }

	public void ChoosePolish()
    {
        ChooseLanguageAndGoToMainMenu(Languages.Polish);
    }

    public void ChooseEnglish()
    {
        ChooseLanguageAndGoToMainMenu(Languages.English);
    }

    private void ChooseLanguageAndGoToMainMenu(Languages language)
    {
        languageManager.SetLanguage(language);
        screenSwitcher.SwitchTo(Screens.MainMenu);
    }
}
