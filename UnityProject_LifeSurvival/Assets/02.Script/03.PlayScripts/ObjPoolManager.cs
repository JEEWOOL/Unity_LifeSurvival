using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjPoolManager : MonoBehaviour
{
    // �������� ������ ����
    public GameObject[] prefabs;

    // ������Ʈ Ǯ���� ������ �迭 ���� ����
    List<GameObject>[] pools;

    void Awake()
    {
        // Ǯ(�迭)�� �ʱ�ȭ
        pools = new List<GameObject>[prefabs.Length];

        // ����Ʈ�� �ʱ�ȭ
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }        
    }

    // �ʿ��� ������(���ӿ�����Ʈ)�� �ҷ�(��ȯ�ϴ�)���� �Լ�
    public GameObject Get(int index)
    {
        GameObject select = null;

        // ������ Ǯ�� �����(��Ȱ��ȭ ��) ���ӿ�����Ʈ�� ����        
        foreach(GameObject item in pools[index])
        {
            // activeSelf : ���빰 ������Ʈ�� ��Ȱ��ȭ(������)���� Ȯ���ϴ� �Լ�
            if (!item.activeSelf)
            {
                // ���ٿ� �����ϸ� select ������ �Ҵ�
                select = item;
                // ��Ȱ��ȭ(������) ������Ʈ�� ã���� SetActive �Լ��� Ȱ��ȭ
                select.SetActive(true);
                break;
            }
        }

        // ���ٿ� �����ϸ�
        if(select == null)
        {
            // ���Ӱ� �����ϰ� select ������ �Ҵ�
            // ���θ��� ������Ʈ�� �ڽĿ�����Ʈ�� �ְڴ�.
            select = Instantiate(prefabs[index], transform);

            // �̷��� ������ ������Ʈ�� pools�� �־��ش�.
            pools[index].Add(select);
        }

        return select;
    }    
}
