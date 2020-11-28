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
    public AudioSource audSrc;

    void Update()
    {
        if(fadeIn)
        {
            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.clear, fadeLerp);
        }
        else
        {
            GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.black, fadeLerp);
            audSrc.volume = Mathf.Lerp(audSrc.volume, 0.0f, fadeLerp*2.0f);

            if(GetComponent<Image>().color == Color.black)
                SceneManager.LoadScene(fadeOutScene);
        }
    }
}
