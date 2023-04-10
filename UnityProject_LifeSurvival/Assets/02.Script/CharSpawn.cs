using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSpawn : MonoBehaviour
{
    public GameObject[] charPrefabs;
    public GameObject player;

    void Start()
    {
        player = Instantiate(charPrefabs[(int)Char_DataManager.instance.currentCharacter]);
        player.transform.position = transform.position;
    }
}
