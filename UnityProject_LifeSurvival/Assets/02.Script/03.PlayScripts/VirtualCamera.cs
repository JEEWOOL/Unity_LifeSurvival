using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamera : MonoBehaviour
{
    public CinemachineVirtualCamera virCam;

    private void Awake()
    {
        virCam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        virCam.Follow = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().transform;
    }
}
