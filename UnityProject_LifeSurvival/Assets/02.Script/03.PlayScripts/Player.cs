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
        // 물리이동 1. 힘을 가한다 = AddForce(방향)
        //rigid.AddForce(inputVec);

        // 물리이동 2. 속도를 직접 제어한다 = Velocity
        //rigid.velocity = inputVec;

        // 물리이동 3. 위치를 옮긴다 = MovePosition(물리위치 + 방향)
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        // 캐릭터 이동 애니메이션
        // SetFloat("파라미터 이름", 벡터의 순수크기 값));
        anim.SetFloat("Speed", inputVec.magnitude);

        // 캐릭터 오브젝트 좌우변경(inputVec.x가 0보다 작으면)
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
