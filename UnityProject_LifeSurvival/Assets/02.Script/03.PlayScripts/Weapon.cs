using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class Weapon : MonoBehaviour
{
    Scanner target;    

    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;

    private void Start()
    {
        Init();
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                // 근접무기 회전
                transform.Rotate(Vector3.back * speed * Time.deltaTime);                
                break;         
                
            default:
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0;
                    // 총알 발사 함수
                    Fire();
                }
                break;
        }

        if(GameObject.FindWithTag("Player") == null)
        {
            this.gameObject.SetActive(false);
        }
       
        // Levelup Test
        if (Input.GetButtonDown("Jump"))
        {
            LevelUp(2, 1);
        }
    }

    // 무기레벨업 함수
    public void LevelUp(float damage, int count)
    {
        if(id == 1)
        {
            this.damage += damage;
            this.speed -= 0.01f;
        }        

        // 근접 무기일경우에는 생성된 무기에 배치를 다시 설정해줘야 하기때문에
        // location()함수를 호출
        // 무기가 늘어나면 switch문으로 사용
        if (id == 0)
        {
            this.damage += damage;
            this.count += count;
            location();
        }
    }

    // 무기를 초기화
    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -150;
                location();
                break;
            
            default:
                speed = 0.3f;
                break;
        }
    }

    // 생성된 무기를 배치한는 함수
    void location()
    {
        for (int i = 0; i < count; i++)
        {
            // 기존 오브젝트에 있던 근접무기를 먼저사용한 후
            // 모자란 근접무기를 풀링에서 가저오기
            Transform bullet;
            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.Instance.ObjpoolManager.Get(prefabId).transform;
                // parent 속성을 통해 부모를 변경
                bullet.parent = transform;
            }

            // 배치하면서 먼저 위치, 회전 초기화
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // 회전하는 벡터
            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1은 무한으로 관통하는 근접무기
        }
    }

    // 총알 발사 함수
    void Fire()
    {
        if (!GameObject.FindWithTag("Player").GetComponent<Scanner>().nearestTarget)
        {
            return;
        }

        // 총알이 날아가야 하는 방향
        //Vector3 targetPos = GameManager.Instance.player.scanner.nearestTarget.position;
        target = GameObject.FindWithTag("Player").GetComponent<Scanner>();
        Vector3 targetPos = target.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.ObjpoolManager.Get(prefabId).transform;
        bullet.position = transform.position;

        // Quaternion.FromToRotation : 지정된 축을 중심으로 목표를 향해 회전하는 함수
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, 0, dir);
    }
}
