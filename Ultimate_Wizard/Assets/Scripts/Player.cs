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

    public int life = 3;
    private bool attack = true;

    //珥  議곗
    public float maxShotDelay;
    public float curShotDelay;

    private bool isDragActive = false;
    //곗  
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
            // 留곗 대┃ 吏   
            touchStart = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // 留곗 대┃ 吏 怨  留곗 移 李⑥대  怨
            Vector2 delta = (Vector2)Input.mousePosition - touchStart;

            // 罹由  대
            MoveCharacter(delta);

            // 留곗 대┃ 吏  
            touchStart = Input.mousePosition;
        }


    }

    void MoveCharacter(Vector2 delta)
    {
        if (isTouchTop && delta.y > 0 || isTouchBottom && delta.y < 0)
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
        if ((curShotDelay < maxShotDelay) || (attack = false))
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



            }
        }

        else if (collision.gameObject.tag == "MonsterBullet")
        {


            Destroy(collision.gameObject);

            if (isHurt)
            {
                return;
            }
            isHurt = true;


            life--;
            GameManager.instance.UpdateLifeIcon(life);
            RespawnPlayer();



            if (life == 0)
            {
                Destroy(this.gameObject);
                Time.timeScale = 0;
                GameManager.instance.GameOver();
            }
            else
            {
                RespawnPlayer();
            }
        }

    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 3f);
        // player.transform.position = Vector3.down * 3.5;
        //player.SetActive(true);
    }

    public void RespawnPlayerExe()
    {
        Player playerLogic = gameObject.GetComponent<Player>();
        playerLogic.isHurt = false;
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