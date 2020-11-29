using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public string input = "";
    private bool paused = false;

    void Start()
    {
        if(Time.timeScale <= 0.0f) Time.timeScale = 1.0f;
    }

    void Update()
    {
        if(Input.GetButtonDown(input))
        {
            if(!paused)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                Time.timeScale = 0.0f;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }

            paused = !paused;
        }
    }
}
