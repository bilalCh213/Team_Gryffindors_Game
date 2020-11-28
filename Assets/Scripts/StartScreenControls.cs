using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenControls : MonoBehaviour
{
    [SerializeField] private GameObject[] list;
    
    public void PlayButton()
    {
        list[3].SetActive(true);
    }

    public void CreditsButton()
    {
        list[0].SetActive(false);
        list[1].SetActive(false);
        list[2].SetActive(true);
    }

    public void QuitButton()
    {
        if (!Application.isEditor)
        {
            Application.Quit();
        }
    }

    public void BackToMenuButton()
    {
        list[0].SetActive(true);
        list[1].SetActive(true);
        list[2].SetActive(false);
    }
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
