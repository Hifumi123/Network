using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;

    public float followSpeed = 100;

    [NonSerialized]
    public Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        if (target)
            transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * followSpeed);
    }
}
