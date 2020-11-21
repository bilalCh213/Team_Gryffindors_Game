using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 0.9f;
    [SerializeField] private Camera cam;
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        transform.position = Vector3.Lerp(startPosition, cam.transform.position, parallaxFactor);
    }
}
