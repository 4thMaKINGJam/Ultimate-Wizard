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
    public float curDelay;      // 현재 딜레이 시간

    [SerializeField]
    private GameObject bullet;

    void Start()
    {
        
    }

    void Update()
    {
        if (health <= 0)
        {
            // 플레이어의 승리
            Win();
        }

        Attack(1);
        Reload();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            health -= 1f;
        }
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
                
        }

        curDelay = 0;
    }

    // 몬스터 공격 패턴 1 - 홀수형
    void AttackPattern1()
    {
        int lines = 3;

        // 총알 생성
        GameObject b = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rigid = b.GetComponent<Rigidbody2D>();

        // 총알 움직임
        rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);

    }

    void Reload()
    {
        curDelay += Time.deltaTime;
    }

    void Minus()
    {
        // 플레이어로부터 오는 공격 처리

    }

    void Win()
    {

    }
    
 }
