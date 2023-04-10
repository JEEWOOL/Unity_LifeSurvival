using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // �ڽ� ������Ʈ�� Ʈ�������� ���� �迭 ���� ����
    public Transform[] spawnPoint;
    public Spawndata[] spawnData;

    int level;
    float timer;    

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        // Ÿ�̸Ӱ� ���� �ð����� �����ϸ� ��ȯ�ϵ��� ����
        timer += Time.deltaTime;

        // ������ ���ڷ� ������ �ð��� ���� ������ ���
        // Mathf.FloorToInt() : �Ҽ��� �Ʒ��� ������ int������ �ٲٴ� �Լ�
        // Mathf.CeilToInt() : �Ҽ��� �Ʒ��� �ø��� int������ �ٲٴ� �Լ�
        level = Mathf.FloorToInt(GameManager.Instance.gameTime / 6f);
        //level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 6f),
        //    spawnData.Length - 1);
        Debug.Log(level);
        if(level == 9)
        {
            Debug.Log("���ӿ��� �¸��߽��ϴ�.");
        }

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0f;
            Spawn();
        }
    }

    // ���� ������ġ�� ������Ű�� �Լ�
    void Spawn()
    {
        // ������ ���� �� ����
        GameObject enemy = GameManager.Instance.ObjpoolManager.Get(0);

        // Enemy�� �����Ǵ� Spawner ��ġ
        // Random.Range�� �ּҰ��� 1�� ������ GetComponentsInChildren<>��
        // �ڱ� �ڽŵ� ���ԵǱ� ������ �ڽ� ������Ʈ������ ���õǵ��� 1�� ����
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

// ����ȭ(Serialization) : ��ü�� ���� Ȥ�� �����ϱ� ���� ��ȯ
[System.Serializable]
public class Spawndata
{
    // ��ȯ�ð�
    public float spawnTime;

    // ��������Ʈ Ÿ��
    public int spriteType;    
    // ü��
    public int health;
    // �ӵ�
    public float speed;    
}