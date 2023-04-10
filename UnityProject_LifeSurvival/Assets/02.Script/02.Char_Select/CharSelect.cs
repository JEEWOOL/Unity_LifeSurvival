using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    public Character character;    
    
    public Image image;
    public Sprite sprite1;
    public Sprite sprite2;    

    private void OnMouseUpAsButton()
    {
        Char_DataManager.instance.currentCharacter = character;
        OnSelect();
    }

    void OnSelect()
    {
        if(character == Character.Woman)
        {            
            image.sprite = sprite1;
        }
        else if(character == Character.Man)
        {
            image.sprite = sprite2;
        }
    }
}
