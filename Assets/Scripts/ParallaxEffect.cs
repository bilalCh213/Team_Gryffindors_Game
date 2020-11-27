using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxFactor = 0.9f;
    [SerializeField] private bool parallaxOnYAxis = false;
    [SerializeField] private Camera cam;
    private Vector3 startPosition;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        Vector3 pos = transform.position;
        
        if (parallaxOnYAxis)
            pos = Vector3.Lerp(startPosition, cam.transform.position, parallaxFactor);
        else
            pos = Vector3.Lerp(startPosition,
                new Vector3(cam.transform.position.x, pos.y, cam.transform.position.z), parallaxFactor);

        pos.z = 0.0f;

        transform.position = pos;
    }
}
