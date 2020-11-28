using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    [SerializeField] private Transform plTr;
    [SerializeField] private float minDist = 2.5f;
    [SerializeField] private GameObject nextSceneObject;
    private bool done = false;

    void Start()
    {
        
    }

    void Update()
    {
        if(Vector3.Distance(plTr.position, transform.position) < minDist && !done)
        {
            nextSceneObject.SetActive(true);
            done = true;
        }
    }
}
