using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ���ӸŴ����� �޸𸮿� �÷����´�.
    public static GameManager Instance;

    //Header : �ν������� �Ӽ����� ���н����ִ� Ÿ��Ʋ
    [Header("# Game Control")]
    // ���� ���ӽð�
    public float gameTime;
    // �ִ� ���ӽð� (�����ð� * ��)
    public float maxGameTime = 5 * 60f;

    [Header("# Game Player Info")]
    // óġ ������
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
        // �ν��Ͻ� ������ �ڱ��ڽ� this�� �ʱ�ȭ
        Instance = this;
    }

    private void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        // ���ӽð�
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
