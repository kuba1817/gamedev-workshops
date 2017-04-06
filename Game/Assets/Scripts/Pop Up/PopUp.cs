using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PopUp : MonoBehaviour { 

    public Text titleText;
    public Text textText;

    private Action action;

    public void SetValues(string title, string text, Action action = null)
    {
        titleText.text = title;
        textText.text = text;
        this.action = action;
    }

    public void Close()
    {
        if (action != null)
        {
            action();
        }
        Destroy(gameObject);
    }
}
