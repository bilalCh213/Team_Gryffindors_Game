using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupReceiver : MonoBehaviour
{
    private List<Pickup.Type> pickups;
    
    public bool AddPickup(Pickup.Type type)
    {
        pickups.Add(type);
        return true;
    }
    
    void Start()
    {
        pickups = new List<Pickup.Type>();
    }
    
    void Update()
    {
        for (int i = 0; i < pickups.Count; i++)
        {
            switch (pickups[i])
            {
                case Pickup.Type.WOLFSKIN:
                    GetComponent<PlayerController>().gameObject.transform.GetChild(0).GetChild(9).gameObject.SetActive(true);
                    GetComponent<PlayerController>().WolfParticles();
                    pickups.Remove(pickups[i]);
                    break;
                case Pickup.Type.POTION:
                    GetComponent<DamageComponent>().AddToHP(0.25f);
                    pickups.Remove(pickups[i]);
                    break;
                case Pickup.Type.COIN:
                    pickups.Remove(pickups[i]);
                    break;
            }
        }
    }
}
