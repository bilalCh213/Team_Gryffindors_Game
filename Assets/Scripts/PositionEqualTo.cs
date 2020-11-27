using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionEqualTo : MonoBehaviour
{
    [SerializeField] private Transform tr;
    
    void Start()
    {
        transform.position = tr.position;
        transform.rotation = tr.rotation;
    }
    
    void Update()
    {
        transform.position = tr.position;
        transform.rotation = tr.rotation;
    }
}
