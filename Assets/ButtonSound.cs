using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip hover;
    public AudioClip select;

    private bool isNormal = true;
    private bool isHover = true;

    public AudioSource audSrc;

    public void OnNormalBtn()
    {
        isNormal = true;
    }

    public void OnHoverBtn()
    {
        isHover = true;

        if(isNormal)
            audSrc.PlayOneShot(hover);

        isNormal = false;
    }

    public void OnSelectBtn()
    {
        if(isHover || isNormal)
            audSrc.PlayOneShot(select);

        isNormal = false;
        isHover = false;
    }
}
