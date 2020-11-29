using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorToggle : MonoBehaviour
{
    [SerializeField] private bool state = false;
    public enum Type {
        BLUE,
        RED,
        GREEN,
        YELLOW
    }
    [SerializeField] private Type type = Type.BLUE;
    [Space]
    [SerializeField] private float minDistance = 2.0f;
    [Space]
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject blocks;
    [Space]
    [SerializeField] private string interactInput;
    [Space]
    [SerializeField] public SpriteRenderer colorInd;
    [Space]
    [SerializeField] private AudioClip onSound;
    [SerializeField] private AudioClip offSound;
    private AudioSource audSrc;
    private Transform playerTr;

    private float timer = 0.0f;
    private float delay = 1.0f;

    void Start()
    {
        audSrc = FindObjectOfType<AudioSource>();
        playerTr = GameObject.FindWithTag("Player").transform;
    }

    void ToggleColorBlocks()
    {
        /*for(int i = 0; i < 4; i++)
        {
           for(int n = 0; n < blocks.transform.GetChild(i).childCount; n++)
            {
                blocks.transform.GetChild(i).GetChild(n).GetChild(0).gameObject.SetActive(true);
                blocks.transform.GetChild(i).GetChild(n).GetChild(1).gameObject.SetActive(false);
            } 
        }*/

        for(int n = 0; n < blocks.transform.GetChild((int)type).childCount; n++)
        {
            blocks.transform.GetChild((int)type).GetChild(n).GetChild(0).gameObject.SetActive(!state);
            blocks.transform.GetChild((int)type).GetChild(n).GetChild(1).gameObject.SetActive(state);
        }

        if(!state) colorInd.color = Color.clear;
        else
        {
            switch (type)
            {
                case Type.BLUE:
                colorInd.color = new Color(0.0f, 0.0f, 1.0f, 0.006f);
                break;

                case Type.RED:
                colorInd.color = new Color(1.0f, 0.0f, 0.0f, 0.006f);
                break;

                case Type.GREEN:
                colorInd.color = new Color(0.0f, 1.0f, 0.0f, 0.006f);
                break;

                case Type.YELLOW:
                colorInd.color = new Color(1.0f, 1.0f, 0.0f, 0.006f);
                break;
            }
        }
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, playerTr.position) < minDistance)
        {
            indicator?.SetActive(true);

            if (Input.GetButtonDown(interactInput))
            {
                if (!state)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    state = true;
                    audSrc.PlayOneShot(onSound);
                    ToggleColorBlocks();
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(false);
                    state = false;
                    audSrc.PlayOneShot(offSound);
                    ToggleColorBlocks();
                }
            }
        }
        else
        {
            indicator?.SetActive(false);
        }

        if(timer <= 0.0f)
        {
            if(blocks.transform.GetChild((int)type).GetChild(0).GetChild(1).gameObject.activeSelf)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                state = true;
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                state = false;
            }

            timer = delay;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
