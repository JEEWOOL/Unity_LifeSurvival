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
                // �������� ȸ��
                transform.Rotate(Vector3.back * speed * Time.deltaTime);                
                break;         
                
            default:
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0;
                    // �Ѿ� �߻� �Լ�
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

    // ���ⷹ���� �Լ�
    public void LevelUp(float damage, int count)
    {
        if(id == 1)
        {
            this.damage += damage;
            this.speed -= 0.01f;
        }        

        // ���� �����ϰ�쿡�� ������ ���⿡ ��ġ�� �ٽ� ��������� �ϱ⶧����
        // location()�Լ��� ȣ��
        // ���Ⱑ �þ�� switch������ ���
        if (id == 0)
        {
            this.damage += damage;
            this.count += count;
            location();
        }
    }

    // ���⸦ �ʱ�ȭ
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

    // ������ ���⸦ ��ġ�Ѵ� �Լ�
    void location()
    {
        for (int i = 0; i < count; i++)
        {
            // ���� ������Ʈ�� �ִ� �������⸦ ��������� ��
            // ���ڶ� �������⸦ Ǯ������ ��������
            Transform bullet;
            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.Instance.ObjpoolManager.Get(prefabId).transform;
                // parent �Ӽ��� ���� �θ� ����
                bullet.parent = transform;
            }

            // ��ġ�ϸ鼭 ���� ��ġ, ȸ�� �ʱ�ȭ
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // ȸ���ϴ� ����
            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero); // -1�� �������� �����ϴ� ��������
        }
    }

    // �Ѿ� �߻� �Լ�
    void Fire()
    {
        if (!GameObject.FindWithTag("Player").GetComponent<Scanner>().nearestTarget)
        {
            return;
        }

        // �Ѿ��� ���ư��� �ϴ� ����
        //Vector3 targetPos = GameManager.Instance.player.scanner.nearestTarget.position;
        target = GameObject.FindWithTag("Player").GetComponent<Scanner>();
        Vector3 targetPos = target.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.ObjpoolManager.Get(prefabId).transform;
        bullet.position = transform.position;

        // Quaternion.FromToRotation : ������ ���� �߽����� ��ǥ�� ���� ȸ���ϴ� �Լ�
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, 0, dir);
    }
}
