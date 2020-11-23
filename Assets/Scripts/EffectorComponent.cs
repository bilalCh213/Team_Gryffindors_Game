using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectorComponent : MonoBehaviour
{
    public enum Type
    {
        JUMPER = 0,
        DAMAGE,
    }

    [SerializeField] private EffectorComponent.Type type = Type.JUMPER;
    [SerializeField] private float value = 1.0f;
    [SerializeField] private string[] targetTags;
    [SerializeField] private GameObject effectObject;
    [SerializeField] private float disableDelay = 0.2f;

    private float disableTimer = 0.0f;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if (disableTimer > 0.0f) disableTimer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (disableTimer <= 0.0f)
        {
            for (int i = 0; i < targetTags.Length; i++)
            {
                if (other.gameObject.tag == targetTags[i])
                {
                    switch (type)
                    {
                        case Type.JUMPER:
                            other.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0.0f, value);
                            break;
                        case Type.DAMAGE:
                            other.gameObject.GetComponent<DamageComponent>().AddToHP(value);
                            break;
                    }

                    if (effectObject) Instantiate(effectObject, transform.position, Quaternion.identity);
                    disableTimer = disableDelay;
                }
            }
        }
    }
}
