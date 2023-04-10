using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ItemData : MonoBehaviour
{
    Itemcolider itcData;
    public string type;

    private void Awake()
    {
        itcData = GameObject.Find("ItemDropManager").GetComponent<Itemcolider>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Player player = collision.gameObject.GetComponent<Player>();

            switch (type)
            {
                case "Level_One_UP":
                    itcData.oneUp++;
                    itcData.text3.text = itcData.oneUp.ToString();
                    //GameManager.Instance.weapon.LevelUp(2, 1);
                    GameObject.FindWithTag("melee").GetComponent<Weapon>().LevelUp(2, 1);
                    GameObject.FindWithTag("ranged").GetComponent<Weapon>().LevelUp(2, 1);
                    Debug.Log("레벨 1업");
                    break;

                case "Level_Two_UP":
                    itcData.twoUp++;
                    itcData.text4.text = itcData.twoUp.ToString();
                    GameObject.FindWithTag("melee").GetComponent<Weapon>().LevelUp(4, 2);
                    GameObject.FindWithTag("ranged").GetComponent<Weapon>().LevelUp(4, 2);
                    Debug.Log("레벨 2업");
                    break;

                case "SpeedUp":
                    itcData.speedUp++;
                    itcData.text2.text = itcData.speedUp.ToString();
                    GameObject.FindWithTag("Player").GetComponent<Player>().speed = GameObject.FindWithTag("Player").GetComponent<Player>().speed + 0.1f;
                    Debug.Log("속도 업!");
                    break;

                case "Potion":
                    itcData.potion++;
                    itcData.text1.text = itcData.potion.ToString();                    
                    GameManager.Instance.health = GameManager.Instance.health + 5;
                    Debug.Log("체력회복");
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
