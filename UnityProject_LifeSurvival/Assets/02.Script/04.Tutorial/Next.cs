using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Next : MonoBehaviour
{
    Image image;  //이미지에 대한 선언
    public Sprite[] sprites;  //public으로 sprite[] 배열선언

    int index = 0; //sprite의 배열에대한 순서 첫번째 0부터시작
    void Start()
    {
        image = GetComponent<Image>(); // 스타트할때 이미지는 GetComponent(inspector창)의 image
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))   //마우스의 왼쪽버튼을 눌럿다 뗄때 UP
        {
            image.sprite = sprites[index];  //image의 sprite를 불러와서 index 순으로 함
            index++; // index가 점점 늘어남
            if (sprites.Length == index) //sprite의 길이가 index의 수만큼
            {
                SceneManager.LoadScene("03.Character_Select");
            }

        }
    }

    public void Tutorial_Skip()
    {
        SceneManager.LoadScene("03.Character_Select");
    }
}