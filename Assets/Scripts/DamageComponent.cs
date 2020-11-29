using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private float maxHitPoints = 1.0f;
    [SerializeField] private GameObject damageSpriteDisplay;
    [SerializeField] private GameObject gameover;
    private float damageSpriteFullScale = 0.0f;
    private float hitPoints = 0.0f;

    public float GetHP()
    {
        return hitPoints;
    }

    public void SetHP(float hp)
    {
        hitPoints = hp;
        if (hitPoints > maxHitPoints) hitPoints = maxHitPoints;

        if (damageSpriteDisplay != null)
        {
            Vector2 sc = damageSpriteDisplay.transform.localScale;
            sc.x = damageSpriteFullScale * (hitPoints / maxHitPoints);
            damageSpriteDisplay.transform.localScale = sc;

            Vector2 pos = damageSpriteDisplay.transform.localPosition;
            pos.x = (-damageSpriteFullScale/2.0f) + (sc.x / damageSpriteFullScale);
            damageSpriteDisplay.transform.localPosition = pos;
        }
    }

    public void AddToHP(float addition)
    {
        SetHP(GetHP() + addition);
    }
    
    void Start()
    {
        if (damageSpriteDisplay != null)
        {
            damageSpriteFullScale = damageSpriteDisplay.transform.localScale.x;
        }

        hitPoints = maxHitPoints;
    }
    
    void Update()
    {
        if (hitPoints <= 0.0f)
        {
            if (gameObject.tag == "Player")
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                gameover.SetActive(true);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

//special wolf player damage thingy!
    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Enemy")
        {
            if(tag == "Player")
            {
                if(!GetComponent<PlayerController>().gameObject.transform.GetChild(0).GetChild(9).gameObject.activeSelf)
                {
                    hitPoints -= maxHitPoints;
                    Destroy(GetComponent<Collider2D>());
                }

                GetComponent<PlayerController>().gameObject.transform.GetChild(0).GetChild(9).gameObject.SetActive(false);
                GetComponent<PlayerController>().WolfParticles(true);
            }
        }   
    }
}
