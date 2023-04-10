using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Next : MonoBehaviour
{
    Image image;  //�̹����� ���� ����
    public Sprite[] sprites;  //public���� sprite[] �迭����

    int index = 0; //sprite�� �迭������ ���� ù��° 0���ͽ���
    void Start()
    {
        image = GetComponent<Image>(); // ��ŸƮ�Ҷ� �̹����� GetComponent(inspectorâ)�� image
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))   //���콺�� ���ʹ�ư�� ������ ���� UP
        {
            image.sprite = sprites[index];  //image�� sprite�� �ҷ��ͼ� index ������ ��
            index++; // index�� ���� �þ
            if (sprites.Length == index) //sprite�� ���̰� index�� ����ŭ
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