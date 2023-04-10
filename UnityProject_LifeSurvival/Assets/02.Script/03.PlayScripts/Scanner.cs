using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    // ��ĵ�� �� ����
    public float scanRange;
    // ������ ��ĵ�Ұ��ΰ�
    public LayerMask targetLayer;
    // ���� ��������� ��ĵ�� �迭
    public RaycastHit2D[] targets;
    public Transform nearestTarget;
    
    void FixedUpdate()
    {
        // CircleCastAll : ������ ĳ��Ʈ�� ��� ��� ����� ��ȯ�ϴ� �Լ�
        // CircleCastAll(ĳ���� ���� ��ġ, ���� ������, ĳ���� ����, ĳ���� ����, ��� ���̾�)
        targets = Physics2D.CircleCastAll(transform.position, scanRange, 
            Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    // ���� ����� ���� Ž�� �Լ�
    Transform GetNearest()
    {
        Transform result = null;

        // �Ÿ����
        float diff = 100f;

        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;

            // Distance(a, b) : ���� a�� b�� �Ÿ��� ������ִ� �Լ�
            float curDiff = Vector3.Distance(myPos, targetPos);

            // �ݺ����� ���� ������ �Ÿ��� ����� �Ÿ����� ������ Ÿ�� ��ü
            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
}
