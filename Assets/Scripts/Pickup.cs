using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum Type
    {
        GUN = 0,
        POTION,
        COIN
    }

    [SerializeField] private float minDistance = 2.0f;
    [SerializeField] private Pickup.Type type;
    private Transform playerTr;

    void Start()
    {
        playerTr = GameObject.FindWithTag("Player").transform;
    }
    
    void Update()
    {
        if (Vector2.Distance(transform.position, playerTr.position) < minDistance)
        {
            if(playerTr.gameObject.GetComponent<PickupReceiver>().AddPickup(type))
                Destroy(gameObject);
        }
    }
}
