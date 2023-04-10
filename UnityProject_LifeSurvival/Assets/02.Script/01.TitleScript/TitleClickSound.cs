using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleClickSound : MonoBehaviour
{
    AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audio.Play();
        }
    }
}
