using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public bool fadeIn = true;
    public float fadeLerp = 0.001f;
    public string fadeOutScene = "";

    void Update()
    {
        if(fadeIn)
        {
            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.clear, fadeLerp);
        }
        else
        {
            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.black, fadeLerp);

            if(GetComponent<Image>().color == Color.black)
                SceneManager.LoadScene(fadeOutScene);
        }
    }
}
