using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Transform plTr;
    [SerializeField] private Transform ropePoint;
    [Space]
    [SerializeField] private float minAttachDist = 2.0f;
    [Space]
    [SerializeField] private Animator animator;
    [Space]
    [SerializeField] private string inputKey;
    [SerializeField] private float disableDelay = 0.5f;

    private float timer = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        if(Vector3.Distance(plTr.position, ropePoint.position) < minAttachDist && timer <= 0.0f)
        {
            plTr.position = ropePoint.position;
            plTr.gameObject.GetComponent<PlayerController>().enabled = false;
            plTr.localScale = new Vector2(1.0f, 1.0f);
            animator.SetBool("isPlayerHanging", true);

            if(Input.GetButtonDown(inputKey))
            {
                timer = disableDelay;
                plTr.gameObject.GetComponent<PlayerController>().enabled = true;
                animator.SetBool("isPlayerHanging", false);
            }
        }

        timer -= Time.deltaTime;
    }
}
