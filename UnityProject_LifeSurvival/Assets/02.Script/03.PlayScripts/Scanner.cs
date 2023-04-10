using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // 스캔을 할 범위
    public float scanRange;
    // 누구를 스캔할것인가
    public LayerMask targetLayer;
    // 가장 가까운적을 스캔할 배열
    public RaycastHit2D[] targets;
    public Transform nearestTarget;
    
    void FixedUpdate()
    {
        // CircleCastAll : 원형에 캐스트를 쏘고 모든 결과를 반환하는 함수
        // CircleCastAll(캐스팅 시작 위치, 원의 반지름, 캐스팅 방향, 캐스팅 길이, 대상 레이어)
        targets = Physics2D.CircleCastAll(transform.position, scanRange, 
            Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    // 가장 가까운 몬스터 탐색 함수
    Transform GetNearest()
    {
        Transform result = null;

        // 거리계산
        float diff = 100f;

        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;

            // Distance(a, b) : 벡터 a와 b의 거리를 계산해주는 함수
            float curDiff = Vector3.Distance(myPos, targetPos);

            // 반복문을 돌며 가져온 거리가 저장된 거리보다 작으면 타겟 교체
            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
