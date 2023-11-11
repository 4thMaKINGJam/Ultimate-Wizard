using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private float power;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;
    public bool isHurt;

    public GameManager manager;

    public int life = 3;
    private bool attack = true;

    //�Ѿ� �ӵ� ����
    public float maxShotDelay;
    public float curShotDelay;




    private bool isDragActive = false;
    //��ġ ������
    private Vector2 touchStart;


    public GameObject bulletObjA;
    

    //Animator anim;

    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();

        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 Ŭ���� ���� ����
            touchStart = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // ���콺 Ŭ���� ������ ���� ���콺 ��ġ�� ���̸� ���
            Vector2 delta = (Vector2)Input.mousePosition - touchStart;

            // ĳ���� �̵�
            MoveCharacter(delta);

            // ���콺 Ŭ���� ���� ������Ʈ
            touchStart = Input.mousePosition;
        }
 

    }

    void MoveCharacter(Vector2 delta)
    {
        if (isTouchTop && delta.y>0 || isTouchBottom && delta.y<0)
        {
   
            speed = 0;
        }
        else
        {
     
            speed = 2; 
        }
        transform.position += new Vector3(0, delta.y * speed * Time.deltaTime, 0);

    }


   

    void Fire()
    {
        if ((curShotDelay < maxShotDelay)||(attack = false))
            return;
        

        GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * 15, ForceMode2D.Impulse);

        curShotDelay = 0;
    
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }


    void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.gameObject.tag == "Border")
         {
             switch (collision.gameObject.name)
             {
                 case "Top":
                     isTouchTop = true;
                     break;
                 case "Bottom":
                     isTouchBottom = true;

                     break;

               /*  case "Left":
                     isTouchLeft = true;

                     break;

                 case "Right":
                     isTouchRight = true;

                     break;*/

             }
         }

         else if (collision.gameObject.tag == "MonsterBullet")
        {

            if (isHurt)
                return;

            isHurt = true;

            attack = false;
            life--;
            manager.UpdateLifeIcon(life);
            manager.RespawnPlayer();// �ִϸ��̼� ���, ����x, 3�� ���� �ʿ�
            manager.RespawnPlayerExe(attack);
            if (life ==0)
            {
                Destroy(gameObject);
                manager.GameOver();
            }
            else
            {
                manager.RespawnPlayer();
                
            }
            //gameObject.SetActive(false);
              Destroy(collision.gameObject);
        

        }
     }

    void OnTriggerExit2D(Collider2D collision)
     {
         if (collision.gameObject.tag == "Border")
         {
             switch (collision.gameObject.name)
             {
                 case "Top":
                     isTouchTop = false;
                     break;
                 case "Bottom":
                     isTouchBottom = false;

                     break;

               /*  case "Left":
                     isTouchLeft = false;

                     break;

                 case "Right":
                     isTouchRight = false;

                     break;*/

             }
         }
     }
}
