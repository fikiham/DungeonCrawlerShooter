using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float damping;

    float defaultZoom;
    float targetZoom = 10;

    Vector3 velocity = Vector3.zero;

    Vector2 bossArea = new(12, 7);

    private void Start()
    {
        defaultZoom = Camera.main.orthographicSize;
    }

    private void LateUpdate()
    {
        Vector3 movePos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePos, ref velocity, damping);
        Vector2 currentPos = transform.position;
        if (currentPos.x>-bossArea.x &&
            currentPos.x<bossArea.x &&
            currentPos.y>-bossArea.y &&
            currentPos.y<bossArea.y)
        {
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetZoom, 3 * Time.deltaTime);
        }
        else
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, defaultZoom, 3 * Time.deltaTime);

    }
}
