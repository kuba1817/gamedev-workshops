using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class AnimatedScreen : MonoBehaviour {

    public Screens type;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

	public void Show()
    {
        animator.SetTrigger(Triggers.Gui.ShowScreen);
    }

    public void Hide()
    {
        animator.SetTrigger(Triggers.Gui.HideScreen);
    }
}
