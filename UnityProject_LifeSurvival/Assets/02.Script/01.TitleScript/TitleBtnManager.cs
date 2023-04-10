using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBtnManager : MonoBehaviour
{
    public GameObject startBtn;
    public GameObject exitBtn;
    public GameObject touchText;

    void Awake()
    {
        startBtn.SetActive(false);
        exitBtn.SetActive(false);
        touchText.SetActive(true);
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startBtn.SetActive(true);
            exitBtn.SetActive(true);
            touchText.SetActive(false);
        }
    }

    public void StartScene()
    {
        SceneManager.LoadScene("02.Tutorial");
    }

    public void ExitScene()
    {
        Application.Quit();
    }
}
