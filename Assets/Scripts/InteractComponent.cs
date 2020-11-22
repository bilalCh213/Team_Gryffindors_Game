using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractComponent : MonoBehaviour
{
    public enum Type
    {
        SWITCH = 0,
    }

    [SerializeField] private bool state = false;
    [Space]
    [SerializeField] private float minDistance = 2.0f;
    [Space]
    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject[] activate;
    [SerializeField] private GameObject[] deactivate;
    [Space]
    [SerializeField] private InteractComponent.Type type;
    [SerializeField] private string interactInput;
    private Transform playerTr;

    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
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
                    foreach (GameObject obj in activate) obj.SetActive(true);
                    foreach (GameObject obj in deactivate) obj.SetActive(false);
                    state = true;
                }
                else
                {
                    foreach (GameObject obj in activate) obj.SetActive(false);
                    foreach (GameObject obj in deactivate) obj.SetActive(true);
                    state = false;
                }
            }
        }
        else
        {
            indicator?.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Bullet>())
        {
            if (!state)
            {
                foreach (GameObject obj in activate) obj.SetActive(true);
                foreach (GameObject obj in deactivate) obj.SetActive(false);
                state = true;
            }
            else
            {
                foreach (GameObject obj in activate) obj.SetActive(false);
                foreach (GameObject obj in deactivate) obj.SetActive(true);
                state = false;
            }
        }
    }
}
