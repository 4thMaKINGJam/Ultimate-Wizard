using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float health = 1f;

    [SerializeField]
    private float delay = 2f; // 기준 딜레이
    public float curDelay;        // 현재 딜레이 시간

    [SerializeField]
    private int roundNum = 30;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject bullet6;
    [SerializeField]
    private GameObject bullet7;

    [SerializeField]
    private GameObject player;

    private bool canShoot = true;
    private float cooldown = 5f; // 5초 동안 공격 X

    private int simulNum = 3;

    private float changeTime = 0f;
    private float duration = 0f;
    private int curPattern = 0;

    void Start()
    {

    }

    void Update()
    {
        if (health <= 0) // 플레이어의 승리
        {
            Win();
        }

        // 공격 패턴 변경
        if (Time.time >= changeTime - 1f)
        {
            curPattern = Random.Range(1, 8); // 랜덤 패턴 1~7
            duration = Random.Range(5f, 10f); 
            changeTime = Time.time + duration; // 다음 패턴 변경 시간 설정
            Debug.Log(curPattern);
        }

        // 현재 패턴 실행
        Attack(curPattern);
        Reload();
    }

    void Reload()
    {
        curDelay += Time.deltaTime;
    }

    void Attack(int pattern)
    {
        if (curDelay < delay)
        {
            return;
        }

        // 총알 생성
        switch (pattern)
        {
            case 1:
                AttackPattern1();
                break;
            case 2:
                AttackPattern2();
                break;
            case 3:
                AttackPattern3();
                break;
            case 4:
                AttackPattern1();
                break;
            case 5:
                AttackPattern2();
                break;
            case 6:
                if (canShoot)
                {
                    AttackPattern6();
                    StartCoroutine(ShootCooldown());
                }
                break;
            case 7:
                AttackPattern7();
                break;
        }

        curDelay = 0;
    }

    // 몬스터 공격 패턴 1 - 홀수형
    void AttackPattern1()
    {
        // 랜덤 숫자
        int r = Random.Range(0, 3);
        int lines = r * 2 + 3; // 3, 5, 7

        float angleDiff = 20f; // 중심으로부터의 각도

        // 총알 생성
        for (int i = 0; i < lines; i++)
        {
            GameObject b = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody2D rigid = b.GetComponent<Rigidbody2D>();

            // 총알 방향
            Vector2 center = (Vector2)player.transform.position - (Vector2)transform.position;
            Vector2 direction;

            // 중간 갈래는 플레이어를 향하도록 설정
            if (i == lines / 2)
            {
                direction = center;
            }
            else
            {
                // 나머지 갈래는 중심으로부터 일정 각도씩 차이 나도록 설정
                float angle = (i < lines / 2) ? i * angleDiff : (i - lines / 2) * -angleDiff;
                direction = Quaternion.Euler(0, 0, angle) * center.normalized;
            }

            rigid.AddForce(direction.normalized * 10, ForceMode2D.Impulse);
        }
    }

    void AttackPattern2()
    {
        // 랜덤 숫자
        int r = Random.Range(0, 3);
        int lines = r * 2 + 2; // 2, 4, 6

        float angleDiff = 20f; // 중심으로부터의 각도

        // 총알 생성
        for (int i = 0; i < lines; i++)
        {
            GameObject b = Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody2D rigid = b.GetComponent<Rigidbody2D>();

            // 총알 방향
            Vector2 center = (Vector2)player.transform.position - (Vector2)transform.position;
            Vector2 direction;
            float angle;

            if (lines % 2 == 0)
            {
                // 짝수일 때 중앙이 빈 곳을 향하도록 설정
                angle = (i < lines / 2) ? i * angleDiff : (i - (lines - 1) / 2) * -angleDiff;
            }
            else
            {
                // 홀수일 때 중앙 갈래는 플레이어를 향하도록 설정
                angle = (i == lines / 2) ? 0 : (i < lines / 2) ? i * angleDiff : (i - (lines - 1) / 2) * -angleDiff;
            }
            direction = Quaternion.Euler(0, 0, angle) * center.normalized;
            rigid.AddForce(direction.normalized * 10, ForceMode2D.Impulse);
        }
    }

    void AttackPattern3()
    {
        int roundNumA = 50;
        int roundNumB = 30;

        if (roundNum == roundNumA)
        {
            roundNum = roundNumB;
        }
        else
        {
            roundNum = roundNumA;
        }

        for (int i = 0; i < roundNum; i++)
        {
            GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
            Rigidbody2D rigid = b.GetComponent<Rigidbody2D>();

            Vector2 direction = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNum)
                                                                           , Mathf.Sin(Mathf.PI * 2 * i / roundNum));
            rigid.AddForce(direction.normalized * 5, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * i / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }
    }

    IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
    void AttackPattern6()
    {
        int bulletCount = 20; // 동시에 나오는 총알 수

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject b = Instantiate(bullet6, transform.position, transform.rotation);
            Rigidbody2D rigid = b.GetComponent<Rigidbody2D>();

            // 랜덤 각도 및 속도
            float angle = Random.Range(-25f, 25f);
            float speed = Random.Range(3f, 15f);

            // 총알 방향
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right.normalized;

            rigid.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
        }
    }

    void AttackPattern7()
    {
        int bulletCount = 5; // 동시에 나오는 총알 수

        CircleCollider2D collider = bullet7.GetComponent<CircleCollider2D>();
        float bulletWidth = 0;

        if (collider != null)
        {
            bulletWidth = collider.radius * 2;
        }

        for (int j = 0; j < simulNum; j++)
        {
            // 랜덤 높이
            float height = Random.Range(-4.5f, 4.5f);

            // 총알 시작 위치
            Vector3 startPosition = transform.position + new Vector3(0, height, 0);

            // 총알 생성
            for (int i = 0; i < bulletCount; i++)
            {
                // 각 총알의 x 위치 계산
                float xPos = startPosition.x + i * bulletWidth + j;

                // 총알 생성
                GameObject b = Instantiate(bullet7, new Vector3(xPos, height, 0), Quaternion.identity);
                Rigidbody2D rigid = b.GetComponent<Rigidbody2D>();

                rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Debug.Log(health);
            Destroy(collision.gameObject);
            Minus();
        }
    }


    void Minus()
    {
        // 플레이어로부터 오는 공격
        health -= 1f;
    }

    void Win()
    {
        Time.timeScale = 0f;
        Debug.Log("승리");
        GameManager.instance.GameClear();
    }
}