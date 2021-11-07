using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowController : MonoBehaviour
{
    public List<Crow> Crows = new List<Crow>();

    float timer = 0;
    public float LaunchDelay = 5;
    private void Update()
    {
        if(timer > LaunchDelay)
        {
            int randInd = Random.Range(0, Crows.Count);
            Crows[randInd].Launch();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
