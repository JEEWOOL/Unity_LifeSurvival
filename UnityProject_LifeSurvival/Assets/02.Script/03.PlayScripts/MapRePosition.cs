using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRePosition : MonoBehaviour
{
    // Collider2D는 기본 도형의 모든 Collider2D를 포함
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();        
    }

    // Tile Map 재배치
    // 트리거가 체크된 Collider에서 나갔을 때 발생
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Area태그가 아니라면 return(아래 로직을 실행하지 않겠다)
        if (!collision.CompareTag("Area"))
        {
            return;
        }

        // 플레이어 위치 저장
        //Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 playerPos = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().transform.position;

        // 타일맵 위치 저장
        Vector3 myPos = transform.position;

        // 플레이어 위치 - 타일맵 위치 = 거리(무조건 양수가 나와야함)
        // Mathf.Abs : 음수도 양수로 만들어주는 절대값 함수
        float disX = Mathf.Abs(playerPos.x - myPos.x);
        float disY = Mathf.Abs(playerPos.y - myPos.y);

        // 플레이어의 이동 방향을 저장하기 위한 변수
        //Vector3 playerDir = GameManager.Instance.player.inputVec;
        Vector3 playerDir = GameObject.FindWithTag("Player").GetComponent<Player>().inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;
        
        switch (transform.tag)
        {
            // 맵 재배치
            case "Ground":
                // 두 오브젝트의 거리 차이에서 X축이 Y축보다 크면 수평 이동
                if(disX > disY)
                {
                    // Translate((위치 * 방향 * 크기)) : 지정된 값 만큼 현재 위치이동
                    transform.Translate(Vector3.right * dirX * 40);
                }
                // 두 오브젝트의 거리 차이에서 Y축이 X축보다 크면 수직 이동
                else if (disX < disY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            // 몬스터 재배치
            case "Enemy":
                // 플레이어의 이동 방향에 따라 맞은 편에서 등장하도록 이동
                if (coll.enabled)
                {
                    // 생성위치는 랜덤하게 하기위해서 Random.Range() 사용
                    transform.Translate(playerDir * 20 + 
                        new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;
        }
    }
}
