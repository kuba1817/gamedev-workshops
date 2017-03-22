using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MultiLanguageTextElement : MonoBehaviour {

    public MultiLanguageTexts text;
    public bool toUpper = false;

    private Text textComponent;
    private LanguageManager languageManager;

    void Awake()
    {
        languageManager = LanguageManager.Instance;
        textComponent = this.GetComponent<Text>();
    }

    void Start()
    {
        string textToShow = languageManager.GetWord(text);
        if (toUpper)
        {
            textToShow = textToShow.ToUpper();
        }
        textComponent.text = textToShow;
    }
}
