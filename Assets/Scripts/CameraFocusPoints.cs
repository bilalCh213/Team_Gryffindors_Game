using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusPoints : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform playerTr;
    [Space]
    [SerializeField] private float playerFocusFactor = 0.5f;
    [SerializeField] private Vector3 playerFocusOffset = Vector3.zero;
    [SerializeField] private float pointFocusFactor = 0.1f;
    [SerializeField] private float zoomLerpFactor = 0.025f;
    private float zoom = 0.0f;

    void Start()
    {
        
    }

    Vector2 GetPositionOfClosestPoint(Vector3 camPos)
    {
        Vector2 vec = Vector2.zero;
        float distance = 99999.0f;
        for (int i = 0; i < transform.childCount; i++)
        {
            float newDistance = Vector2.Distance(transform.GetChild(i).position, playerTr.position);
            if (newDistance < distance)
            {
                distance = newDistance;
                vec = transform.GetChild(i).position;
                zoom = transform.GetChild(i).position.z;
            }
        }
        return vec;
    }
    
    void Update()
    {
        Vector3 camPos = cam.transform.position;
        
        camPos = Vector2.Lerp(camPos, playerTr.position + playerFocusOffset, playerFocusFactor);
        camPos = Vector2.Lerp(camPos, GetPositionOfClosestPoint(camPos), pointFocusFactor);

        camPos.z = -10.0f;
        cam.transform.position = camPos;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, zoomLerpFactor);
    }
}
