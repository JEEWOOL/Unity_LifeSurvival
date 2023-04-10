using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 자식 오브젝트의 트랜스폼을 담을 배열 변수 선언
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
        // 타이머가 일정 시간값에 도달하면 소환하도록 설정
        timer += Time.deltaTime;

        // 적절한 숫자로 나누어 시간에 맞춰 레벨이 상승
        // Mathf.FloorToInt() : 소수점 아래는 버리고 int형으로 바꾸는 함수
        // Mathf.CeilToInt() : 소수점 아래를 올리고 int형으로 바꾸는 함수
        level = Mathf.FloorToInt(GameManager.Instance.gameTime / 6f);
        //level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 6f),
        //    spawnData.Length - 1);
        Debug.Log(level);
        if(level == 9)
        {
            Debug.Log("게임에서 승리했습니다.");
        }

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0f;
            Spawn();
        }
    }

    // 적을 스폰위치에 출현시키는 함수
    void Spawn()
    {
        // 레벨에 따라서 적 출현
        GameObject enemy = GameManager.Instance.ObjpoolManager.Get(0);

        // Enemy가 생성되는 Spawner 위치
        // Random.Range의 최소값이 1인 이유는 GetComponentsInChildren<>은
        // 자기 자신도 포함되기 때문에 자식 오브젝트에서만 선택되도록 1을 선택
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;

        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

// 직렬화(Serialization) : 개체를 저장 혹은 전송하기 위해 변환
[System.Serializable]
public class Spawndata
{
    // 소환시간
    public float spawnTime;

    // 스프라이트 타입
    public int spriteType;    
    // 체력
    public int health;
    // 속도
    public float speed;    
}