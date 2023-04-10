using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundBtn : MonoBehaviour
{
    AudioSource audio;
    bool isBgm;

    public Image preImage;
    public Sprite playSprite;
    public Sprite muteSprite;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        isBgm = true;
        preImage.sprite = playSprite;
    }

    private void Start()
    {
        audio.Play();
    }
    
    public void BgmToggle()
    {
        if (isBgm)
        {
            isBgm = false;
            audio.Pause();
            preImage.sprite = muteSprite;
        }
        else
        {
            isBgm = true;
            audio.Play();
            preImage.sprite = playSprite;
        }
    }
}
