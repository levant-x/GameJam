using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioplayer;
    [SerializeField] private AudioClip onHoverSound;
    [SerializeField] private AudioClip onClickSound;

    private bool isBgPlaying = true;

    public void ToggleBackgroundSound()
    {
        if (isBgPlaying) audioplayer.Stop();
        else audioplayer.Play();
        isBgPlaying = !isBgPlaying;
    }

    public void PlayHover()
    {
        audioplayer.PlayOneShot(onHoverSound);
    }

    public void PlayClick()
    {
        audioplayer.PlayOneShot(onClickSound);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
