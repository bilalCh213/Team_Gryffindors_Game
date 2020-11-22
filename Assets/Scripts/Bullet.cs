using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private string[] targetTags;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float value = -0.1f;
    [SerializeField] private float destroyAfterDelay = 10.0f;
    [SerializeField] private GameObject spawnAtCollision;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 pos = transform.position;
        pos.x += speed * Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
        pos.y += speed * Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
        transform.position = pos;

        if(destroyAfterDelay <= 0.0f) Destroy(gameObject);
        else destroyAfterDelay -= Time.unscaledDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        for (int i = 0; i < targetTags.Length; i++)
        {
            if (other.gameObject.tag == targetTags[i])
            {
                other.gameObject.GetComponent<DamageComponent>().AddToHP(value);
            }
        }
        Instantiate(spawnAtCollision, transform.position, Quaternion.identity).SetActive(true);
        Destroy(gameObject);
    }
}
