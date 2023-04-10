using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public Text scoreText;
    private int totScore = 0;
    public float duringTime = 5.0f;
    public GameObject Button_Start;
    public GameObject Button_Exit;
    string grade;

    void Start()
    {
        totScore = (GameManager.Instance.kill) * 100;

        switch (totScore / 1000)
        {
            case 0:
                grade = "F";
                break;
            case 1:
                grade = "E";
                break;
            case 2:
                grade = "D";
                break;
            case 3:
                grade = "C";
                break;
            case 4:
                grade = "B";
                break;
            case 5:
                grade = "A";
                break;
            default:
                grade = "S";
                break;
        }

        StartCoroutine(Count(totScore, 0));
    }


    IEnumerator Count(float totscore, float current)
    {
        float duration = 2f; // ī���ÿ� �ɸ��� �ð� ����. 
        float offset = (totscore - current) / duration; // ���ڰ� �ö󰡴� �ð�

        while (current < totscore)
        {
            current += offset * Time.deltaTime;

            scoreText.text = ((int)current).ToString();
            yield return null;
        }

        current = totscore;     // �� �ö󰡰� ����

        scoreText.text = ((int)current).ToString();

        yield return new WaitForSeconds(1f);
        scoreText.text = grade;
        Button_Start.SetActive(true);
        Button_Exit.SetActive(true);
    }

    public void RestartClick()
    {
        SceneManager.LoadScene("03.Character_Select");
    }

    public void ExitClick()
    {
        Application.Quit();
    }
}
