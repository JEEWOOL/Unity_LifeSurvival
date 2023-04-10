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
        // ���� ��� ���� �ʴٸ� ������ �������� �ʴ´�.
        // GetCurrentAnimatorStateInfo(���� �ִϸ��̼� ���̾�).IsName(�۵��ϴ� �ִϸ��̼� �̸�) : ���� ���� ������ �������� �Լ�
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
        {
            return;
        }

        // Ÿ����ġ - ������ġ = ��ġ����
        Vector2 dirVec = target.position - rigid.position;

        // ��ġ������ ����ȭ : Nomalized
        // ������ ������ ��ġ��
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;

        // ���� ���ڽ��� ��ġ + ������ ������ ��ġ��
        rigid.MovePosition(rigid.position + nextVec);

        // �����ӵ��� �̵��� ������ ���� �ʵ��� �ӵ� ����
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        // ���� ��� ���� �ʴٸ� ������ �������� �ʴ´�.
        if (!isLive)
        {
            return;
        }

        // �÷��̾��� x��(��ġ)�� ���� x��(��ġ)���� �۴ٸ� �¿����
        spriter.flipX = target.position.x < rigid.position.x;
    }

    // OnEnable() : ��ũ��Ʈ�� Ȱ��ȭ �� ��, ȣ��Ǵ� �̺�Ʈ �Լ�
    private void OnEnable()
    {
        // ��ũ��Ʈ�� ȣ��ɶ� Enemy�� �ڵ����� Ÿ���� ����
        //target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        // ���� �����ɶ� �ٽ� �������ͽ��� �ʱ�ȭ
        isLive = true;
        health = maxHealth;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
    }

    // ��ȯ �����͸� ���� �Լ�
    public void Init(Spawndata data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    // ȸ�� ����� �浹
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ȸ�� ����� �浹���� �ʾҴٸ� if�� �Ʒ� ������ ���������ʰ� return
        if (!collision.CompareTag("Bullet") || !isLive)
        {
            return;
        }

        // �� ü�¿��� ���� ������ ��ŭ ü���� ����
        health -= collision.GetComponent<Bullet>().damage;
        audioSource.Play();
        StartCoroutine(EnemyHitBack());

        if(health > 0)
        {
            // ���� ����ְ�, ������ ���ϸ�
            anim.SetTrigger("Hit");
        }
        else
        {
            // ���� ���
            isLive = false;
            coll.enabled = false;
            // simulated : ������ٵ� ��Ȱ��ȭ
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead",true);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();
        }
    }

    // �� �ڷ� �о������ �ڷ�ƾ
    IEnumerator EnemyHitBack()
    {
        yield return wait;

        // �÷��̾�� �ݴ�� �б�
        //Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 playerPos = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().transform.position;
        // �÷��̾� �ݴ���� : ���� ��ġ - �÷��̾� ��ġ
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
