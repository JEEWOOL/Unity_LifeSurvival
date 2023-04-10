using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjPoolManager : MonoBehaviour
{
    // 프리펩을 보관할 변수
    public GameObject[] prefabs;

    // 오브젝트 풀들을 저장할 배열 변수 선언
    List<GameObject>[] pools;

    void Awake()
    {
        // 풀(배열)을 초기화
        pools = new List<GameObject>[prefabs.Length];

        // 리스트를 초기화
        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }        
    }

    // 필요한 프리팹(게임오브젝트)을 불러(반환하는)오는 함수
    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀에 저장된(비활성화 된) 게임오브젝트에 접근        
        foreach(GameObject item in pools[index])
        {
            // activeSelf : 내용물 오브젝트가 비활성화(대기상태)인지 확인하는 함수
            if (!item.activeSelf)
            {
                // 접근에 성공하면 select 변수에 할당
                select = item;
                // 비활성화(대기상태) 오브젝트를 찾으면 SetActive 함수로 활성화
                select.SetActive(true);
                break;
            }
        }

        // 접근에 실패하면
        if(select == null)
        {
            // 새롭게 생성하고 select 변수에 할당
            // 새로만든 오브젝트는 자식오브젝트로 넣겠다.
            select = Instantiate(prefabs[index], transform);

            // 이렇게 생성된 오브젝트를 pools에 넣어준다.
            pools[index].Add(select);
        }

        return select;
    }    
}
