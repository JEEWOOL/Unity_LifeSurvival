using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematic_TeamName : MonoBehaviour
{
    AudioSource audio;
    WaitForSeconds wait;
    public GameObject title_name;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        wait = new WaitForSeconds(3f);

        StartCoroutine(Cinematic());
    }    

    IEnumerator Cinematic()
    {
        yield return wait;
        title_name.SetActive(true);
        audio.Play();
        yield return wait ;
        SceneManager.LoadScene("01.Title");
    }
}
