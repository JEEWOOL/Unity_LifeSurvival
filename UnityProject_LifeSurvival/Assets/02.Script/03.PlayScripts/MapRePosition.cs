using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRePosition : MonoBehaviour
{
    // Collider2D�� �⺻ ������ ��� Collider2D�� ����
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();        
    }

    // Tile Map ���ġ
    // Ʈ���Ű� üũ�� Collider���� ������ �� �߻�
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Area�±װ� �ƴ϶�� return(�Ʒ� ������ �������� �ʰڴ�)
        if (!collision.CompareTag("Area"))
        {
            return;
        }

        // �÷��̾� ��ġ ����
        //Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 playerPos = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().transform.position;

        // Ÿ�ϸ� ��ġ ����
        Vector3 myPos = transform.position;

        // �÷��̾� ��ġ - Ÿ�ϸ� ��ġ = �Ÿ�(������ ����� ���;���)
        // Mathf.Abs : ������ ����� ������ִ� ���밪 �Լ�
        float disX = Mathf.Abs(playerPos.x - myPos.x);
        float disY = Mathf.Abs(playerPos.y - myPos.y);

        // �÷��̾��� �̵� ������ �����ϱ� ���� ����
        //Vector3 playerDir = GameManager.Instance.player.inputVec;
        Vector3 playerDir = GameObject.FindWithTag("Player").GetComponent<Player>().inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;
        
        switch (transform.tag)
        {
            // �� ���ġ
            case "Ground":
                // �� ������Ʈ�� �Ÿ� ���̿��� X���� Y�ຸ�� ũ�� ���� �̵�
                if(disX > disY)
                {
                    // Translate((��ġ * ���� * ũ��)) : ������ �� ��ŭ ���� ��ġ�̵�
                    transform.Translate(Vector3.right * dirX * 40);
                }
                // �� ������Ʈ�� �Ÿ� ���̿��� Y���� X�ຸ�� ũ�� ���� �̵�
                else if (disX < disY)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }
                break;

            // ���� ���ġ
            case "Enemy":
                // �÷��̾��� �̵� ���⿡ ���� ���� ���� �����ϵ��� �̵�
                if (coll.enabled)
                {
                    // ������ġ�� �����ϰ� �ϱ����ؼ� Random.Range() ���
                    transform.Translate(playerDir * 20 + 
                        new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0));
                }
                break;
        }
    }
}
