using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임매니저를 메모리에 올려놓는다.
    public static GameManager Instance;

    //Header : 인스펙터의 속성들을 구분시켜주는 타이틀
    [Header("# Game Control")]
    // 실제 게임시간
    public float gameTime;
    // 최대 게임시간 (실제시간 * 초)
    public float maxGameTime = 5 * 60f;

    [Header("# Game Player Info")]
    // 처치 데이터
    public int health;
    public int maxHealth = 100;
    public int level = 0;
    public int kill = 0;
    public int exp = 0;
    public int[] nextExp = {10, 50, 100, 200, 300, 600, 1200, 2400, 3800, 7600};

    [Header("# GameObject")]
    public ObjPoolManager ObjpoolManager;
    public Player player;
    public ItemData itemData;
    public Weapon weapon;
    public HUD hud;

    void Awake()
    {
        // 인스턴스 변수를 자기자신 this로 초기화
        Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        // 게임시간
        gameTime += Time.deltaTime;
        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            kill = 5000;
            SceneManager.LoadScene("06.Ending");
        }

        //if (gameTime <= 0)
        //{
            
        //}

        //if(health <= 0)
        //{
        //    player.gameObject.tag = "Die";
        //    weapon.gameObject.tag = "Die";
        //    player
        //}
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
