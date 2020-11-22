using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    [SerializeField] private float delay = 1.0f;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (delay > 0.0f) delay -= Time.deltaTime;
        else Destroy(gameObject);
    }
}
