using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ScreenSwitcher : Singleton<ScreenSwitcher> {

    public Screens initialScreen;

    private List<AnimatedScreen> screens;
    private AnimatedScreen currentScreen;

    void Start()
    {
        screens = new List<AnimatedScreen>((AnimatedScreen[]) Resources.FindObjectsOfTypeAll(typeof(AnimatedScreen)));
        currentScreen = FindScreenOfType(initialScreen);
    }

    public void SwitchTo(Screens screen)
    {
        AnimatedScreen nextScreen = FindScreenOfType(screen);
        StartCoroutine(SwitchScenes(currentScreen, nextScreen));
        currentScreen = nextScreen;
    }
    
    private AnimatedScreen FindScreenOfType(Screens type)
    {
        return screens.Where(s => s.type == type).FirstOrDefault();
    }

    private IEnumerator SwitchScenes(AnimatedScreen currentScreen, AnimatedScreen nextScreen)
    {
        currentScreen.Hide();
        yield return new WaitForSeconds(Configuration.Animations.ScreenAnimationTime);
        currentScreen.gameObject.SetActive(false);
        nextScreen.gameObject.SetActive(true);
        nextScreen.Show();
    }
}
