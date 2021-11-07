using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AuthorsToggler2 : AuthorsToggler
{
    private Animator animator;
    private IEnumerable<AnimationClip> clips;

    public override void Popup(bool toShow)
    {
        if (toShow) gameObject.SetActive(true);
        else Hide();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        clips = animator.runtimeAnimatorController.animationClips;
    }

    private void Hide()
    {

        animator.SetBool("Popup", false);
    }    

    protected override void OnEnable()
    {
        animator.SetBool("Popup", true);
    }
}
