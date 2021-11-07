using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController : MonoBehaviour
{
    public List<Crow> Crows = new List<Crow>();
    public SoundsManager SoundManager;
    float timer = 0;
    public float LaunchDelay = 5;

    private void Start()
    {
        SoundManager = FindObjectOfType<SoundsManager>();
    }
    private void Update()
    {
        if(timer > LaunchDelay)
        {
            int randInd = Random.Range(0, Crows.Count);
            Crows[randInd].Launch();
            //if(!Crows[randInd].IsLaunch || !Crows[randInd].IsReturn)            
                //SoundManager.PlayCrowAppear();            
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
