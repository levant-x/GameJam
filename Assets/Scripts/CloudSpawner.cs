using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] Prefab;
    float time;
    public Test Test;
    public BuildingBlock template;
    
    void Update()
    {
        if(time > Test.SliderSpeed.value)
        {
            int ind = Random.Range(0, Prefab.Length);
            var temp = Instantiate(Prefab[ind], transform);
            template = temp.GetComponent<BuildingBlock>();
            template.Sleep();
            time = 0;
        }
        if(!template)
            time += Time.deltaTime;
    }

    private void OnDestroy()
    {
        
    }
}

