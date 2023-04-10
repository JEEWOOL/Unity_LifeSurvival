using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    AudioSource audio;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        audio = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // �����̵� 1. ���� ���Ѵ� = AddForce(����)
        //rigid.AddForce(inputVec);

        // �����̵� 2. �ӵ��� ���� �����Ѵ� = Velocity
        //rigid.velocity = inputVec;

        // �����̵� 3. ��ġ�� �ű�� = MovePosition(������ġ + ����)
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        // ĳ���� �̵� �ִϸ��̼�
        // SetFloat("�Ķ���� �̸�", ������ ����ũ�� ��));
        anim.SetFloat("Speed", inputVec.magnitude);

        // ĳ���� ������Ʈ �¿캯��(inputVec.x�� 0���� ������)
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (GameManager.Instance.health <= 0)
            {                
                gameObject.tag = "DIe";
                anim.SetTrigger("Dead");
                StartCoroutine(scenceGo());
            }

            GameManager.Instance.health = GameManager.Instance.health - 10;
        }
    }

    IEnumerator scenceGo()
    {
        audio.Play();
        yield return new WaitForSeconds(1.5f);
        this.gameObject.SetActive(false);
        SceneManager.LoadScene("06.Ending");
    }
}
