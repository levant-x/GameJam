using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioplayer;
    [SerializeField] private AudioClip onHoverSound;
    [SerializeField] private AudioClip onClickSound;
    [SerializeField] private AudioClip onCrowAppear;
    [SerializeField] private AudioClip onCrowFly;
    [SerializeField] private AudioClip onCrowCollide;
    [SerializeField] private AudioClip onCloudExplode;
    [SerializeField] private AudioClip onAddScore;
    [SerializeField] private AudioClip[] onBlockFirstCollide;
    [SerializeField] private AudioClip[] onBlockNextCollide;

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

    public void PlayCrowAppear()
    {
        audioplayer.PlayOneShot(onCrowAppear);
    }

    public void PlayCrowCollide()
    {
        audioplayer.PlayOneShot(onCrowCollide);
    }
    public void PlayCrowFly()
    {
        audioplayer.PlayOneShot(onCrowFly);
    }

    public void PlayCloudExplode()
    {
        audioplayer.PlayOneShot(onCloudExplode);
    }

    public void PlayAddScore()
    {
        audioplayer.PlayOneShot(onCloudExplode);
    }

    public void PlayBlockFirstCollide()
    {
        int randInd = Random.Range(0, onBlockFirstCollide.Length);
        audioplayer.PlayOneShot(onBlockFirstCollide[randInd]);
    }
    public void PlayBlockNextCollide()
    {
        int randInd = Random.Range(0, onBlockNextCollide.Length);
        audioplayer.PlayOneShot(onBlockNextCollide[randInd]);
    }
    SoundsManager soundsManager;
    private void Start()
    {        
        DontDestroyOnLoad(gameObject);
    }
}


