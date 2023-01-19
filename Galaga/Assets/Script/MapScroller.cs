using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScroller : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollRange = 9.9f;
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.back;

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;

        if (transform.position.z <= -scrollRange)
        {
            transform.position = target.position + Vector3.forward * scrollRange;
        }
    }
}
