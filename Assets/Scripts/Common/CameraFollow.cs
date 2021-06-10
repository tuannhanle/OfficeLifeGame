using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;

    //[Range(0.01f, 0.99f)]
    //[SerializeField] private float smoothSpeed;

    private Vector3 offset;
    private void Awake()
    {
        offset = Camera.main.transform.position - _followTarget.position;
    }
    private void LateUpdate()
    {
        this.transform.position = _followTarget.position + offset;
    }
}
