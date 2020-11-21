using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityGroundEffect : MonoBehaviour
{
    [SerializeField] private Transform playerTr;
    [SerializeField] private float minDistance = 4.0f;
    [SerializeField] private float maxDistance = 6.0f;
    private Vector2[,] preservedScale;
    
    
    void Start()
    {
        preservedScale = new Vector2[transform.childCount, 50];
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int n = 0; n < transform.GetChild(i).childCount; n++)
            {
                preservedScale[i, n] = transform.GetChild(i).GetChild(n).localScale;
            }
        }
    }
    
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int n = 0; n < transform.GetChild(i).childCount; n++)
            {
                float dist = Vector2.Distance(transform.GetChild(i).GetChild(n).position, playerTr.position);
                if (dist < maxDistance)
                {
                    dist -= minDistance;
                    if (dist > 0.0f)
                    {
                        Vector2 sc = transform.GetChild(i).GetChild(n).localScale;
                        sc.x = sc.y = 1.0f - (dist / (maxDistance - minDistance));
                        sc.x *= preservedScale[i, n].x;
                        sc.y *= preservedScale[i, n].y;
                        transform.GetChild(i).GetChild(n).localScale = sc;
                    }
                    else
                    {
                        transform.GetChild(i).GetChild(n).localScale = preservedScale[i, n];
                    }
                }
                else
                {
                    transform.GetChild(i).GetChild(n).localScale = Vector2.zero;
                }
            }
        }
    }
}
