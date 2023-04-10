using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Itemcolider : MonoBehaviour
{
    public GameObject itemLevel1;
    public GameObject itemLevel2;
    public GameObject speedBoots;
    public GameObject healItem;

    public Text text1;
    public Text text2;
    public Text text3;
    public Text text4;

    public int oneUp = 0;
    public int twoUp = 0;
    public int speedUp = 0;
    public int potion = 0;

    private void Start()
    {
        Screen.SetResolution(600, 1900, true);
    }
}
