using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private float maxHitPoints = 1.0f;
    private float hitPoints = 0.0f;

    public float GetHP()
    {
        return hitPoints;
    }

    public void SetHP(float hp)
    {
        hitPoints = hp;
        if (hitPoints > maxHitPoints) hitPoints = maxHitPoints;
    }

    public void AddToHP(float addition)
    {
        SetHP(GetHP() + addition);
    }
    
    void Start()
    {
        hitPoints = maxHitPoints;
    }
    
    void Update()
    {
        if (hitPoints <= 0.0f)
        {
            if (gameObject.tag == "Player")
            {
                SceneManager.LoadScene("SideScroller");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
