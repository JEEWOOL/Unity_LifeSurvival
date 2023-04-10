using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    Itemcolider item;
    AudioSource audioSource;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
        coll = GetComponent<Collider2D>();
        target = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        item = GameObject.Find("ItemDropManager").GetComponent<Itemcolider>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if(target == null)
        {
            target = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        }
        // 적이 살아 있지 않다면 로직을 실행하지 않는다.
        // GetCurrentAnimatorStateInfo(현재 애니메이션 레이어).IsName(작동하는 애니메이션 이름) : 현재 상태 정보를 가져오는 함수
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }

        // 타겟위치 - 나의위치 = 위치차이
        Vector2 dirVec = target.position - rigid.position;

        // 위치차이의 정규화 : Nomalized
        // 다음에 가야할 위치값
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        // 지금 내자신의 위치 + 다음에 가야할 위치값
        rigid.MovePosition(rigid.position + nextVec);

        // 물리속도가 이동에 영향을 주지 않도록 속도 제거
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        // 적이 살아 있지 않다면 로직을 실행하지 않는다.
        if (!isLive)
        {
            return;
        }

        // 플레이어의 x값(위치)이 적에 x값(위치)보다 작다면 좌우반전
        spriter.flipX = target.position.x < rigid.position.x;
    }

    // OnEnable() : 스크립트가 활성화 될 때, 호출되는 이벤트 함수
    private void OnEnable()
    {
        // 스크립트가 호출될때 Enemy가 자동으로 타겟을 지정
        //target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        // 적이 생성될때 다시 스테이터스를 초기화
        isLive = true;
        health = maxHealth;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
    }

    // 소환 데이터를 받을 함수
    public void Init(Spawndata data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    // 회전 무기와 충돌
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 회전 무기와 충돌하지 않았다면 if문 아래 로직은 실행하지않고 return
        if (!collision.CompareTag("Bullet") || !isLive)
        {
            return;
        }

        // 적 체력에서 무기 데미지 만큼 체력을 차감
        health -= collision.GetComponent<Bullet>().damage;
        audioSource.Play();
        StartCoroutine(EnemyHitBack());

        if(health > 0)
        {
            // 적이 살아있고, 공격을 당하면
            anim.SetTrigger("Hit");
        }
        else
        {
            // 적이 사망
            isLive = false;
            coll.enabled = false;
            // simulated : 리지드바디를 비활성화
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead",true);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();
        }
    }

    // 적 뒤로 밀어버리는 코루틴
    IEnumerator EnemyHitBack()
    {
        yield return wait;

        // 플레이어와 반대로 밀기
        //Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 playerPos = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().transform.position;
        // 플레이어 반대방향 : 현재 위치 - 플레이어 위치
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3f, ForceMode2D.Impulse);
    }

    void Dead()
    {
        gameObject.SetActive(false);

        int rand = Random.Range(0, 1000);

        Debug.Log(rand);
        if (rand < 969)
        {
            
        }
        else if (rand <= 979 && rand >= 970)
        {            
            Instantiate(item.speedBoots, transform.position,
                item.speedBoots.transform.rotation);
        }
        else if (rand <= 989 && rand >= 980)
        {
            Instantiate(item.healItem, transform.position,
                item.healItem.transform.rotation);
        }
        else if (rand <= 995 && rand >= 990)
        {
            Instantiate(item.itemLevel1, transform.position,
                item.itemLevel1.transform.rotation);
        }
        else if (rand >= 996 && 999 >= rand)
        {
            Instantiate(item.itemLevel2, transform.position,
                item.itemLevel2.transform.rotation);
        }
    }
}
