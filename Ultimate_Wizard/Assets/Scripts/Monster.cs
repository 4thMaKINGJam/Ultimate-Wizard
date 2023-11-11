using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private float health = 1000f;

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float delay = 0.2f; // 기준 딜레이
    public float curDelay;        // 현재 딜레이 시간

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if (health <= 0) // 플레이어의 승리
        {
            Win();
        }

        Attack(2);
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
        switch(pattern)
        {
            case 1:
                AttackPattern1();
                break;
            case 2:
                AttackPattern2();
                break;
                
        }

        curDelay = 0;
    }

    // 몬스터 공격 패턴 1 - 홀수형
    void AttackPattern1()
    {
        // 랜덤 
        int lines = 7;
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
                float angle = (i < lines / 2) ? i * angleDiff : (i - lines / 2) * - angleDiff;
                direction = Quaternion.Euler(0, 0, angle) * center.normalized;
            }

            rigid.AddForce(direction.normalized * 10, ForceMode2D.Impulse);
        }
    }

    void AttackPattern2()
    {
        // 랜덤 
        int lines = 7;
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
                angle = (i < lines / 2) ? i * angleDiff : (i - (lines - 1) / 2) * - angleDiff;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Minus();
        }
    }


    void Minus()
    {
        // 플레이어로부터 오는 공격 처리
        health -= 1f;
    }

    void Win()
    {

    }
    
 }
