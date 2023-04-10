using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCursorBlink : MonoBehaviour
{
    IEnumerator blink;
    Text flashText;
    WaitForSeconds wait;

    void Awake()
    {
        flashText = GetComponent<Text>();
        wait = new WaitForSeconds(0.5f);
        blink = BlinkText();        
    }

    private void Start()
    {
        StartCoroutine(blink);
    }

    void Update()
    {
        
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            flashText.text = "";
            yield return wait;
            flashText.text = "¢º";
            yield return wait;
        }        
    }
}
