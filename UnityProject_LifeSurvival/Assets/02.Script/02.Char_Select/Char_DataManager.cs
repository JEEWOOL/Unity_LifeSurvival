using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    Woman,
    Man
}

public class Char_DataManager : MonoBehaviour
{
    public static Char_DataManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public Character currentCharacter;
}
