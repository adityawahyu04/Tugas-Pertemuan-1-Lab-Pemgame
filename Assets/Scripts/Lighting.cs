using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    float time;
    GameObject[] allLights;
    // Start is called before the first frame update
    void Start()
    {
        allLights = GameObject.FindGameObjectsWithTag("My Lighting");
    }

    // Update is called once per frame
    void Update()
    {
        time = EnviroSky.instance.GameTime.Hours;

        if(time < 18 && time > 6)
        {
            foreach(GameObject i in allLights)
            {
                i.SetActive(false);
            }
        }

        else
            {
               foreach(GameObject i in allLights)
                {
                    i.SetActive(true);
                } 
            }
    }
}
