using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SelectImage : MonoBehaviour
{
    public Character character;

    SpriteRenderer sr;
    public Image image;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    void OnSelect()
    {
        if (character == Character.Woman)
        {

        }
    }
}
