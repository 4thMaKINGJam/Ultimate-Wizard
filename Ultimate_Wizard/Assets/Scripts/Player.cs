using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed=0.5f;
    public float power;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;
    public bool isHurt;
    public float shootspeed=4f;

    public int life = 3;

    private int dum = -1;

    //public GameObject dummyHead;
    Sprite selectedSprite = null;

    public Sprite Sonic_h;
    public Sprite Hercules_h;
    public Sprite Zombie_h;

    public float maxShotDelay=0.4f;
    public float curShotDelay;

    private Vector2 touchStart;

    public GameObject bulletS;
    public GameObject bulletH;
    public GameObject bulletZ;
    private UIManager ui;
    //Blinking
    public float blinkTimer = 0.0f;
    public float blinkDuration = 0.2f;
    public float blinkTotalTime = 0.0f;
    public float blinkTotalDuration = 0.2f;
    public bool startBlinking = false;


    [SerializeField] private AudioSource bgm;

    //Animator anim;

    private void Start() {
        ui = GameObject.FindObjectOfType<UIManager>();
        facechange();
    }

    // Update is called once per frame
    void Update() {

        if (dum == 0) {
            FireS();
        }

        else if (dum == 1)
        {
            FireH();
        }

        else if (dum == 2)
        {
            FireZ();
        }

        Reload();

        if (Input.GetMouseButtonDown(0)) {
            touchStart = Input.mousePosition;
        } else if (Input.GetMouseButton(0)) {
            Vector2 delta = (Vector2)Input.mousePosition - touchStart;

            MoveCharacter(delta);

            touchStart = Input.mousePosition;
        }
    }

    void facechange() {
        dum = PlayerPrefs.GetInt("dum");
        switch (dum){
            case 0:
                selectedSprite = Sonic_h;
                ChangeDummyface(selectedSprite);
                maxShotDelay = 0.2f;
                speed = 0.5f;
                power = 1;
                break;

            case 1:
                selectedSprite = Hercules_h;
                ChangeDummyface(selectedSprite);
                maxShotDelay = 0.4f;
                power =2;
                speed = 0.5f;
                break;
                
            case 2:
                selectedSprite = Zombie_h;
                ChangeDummyface(selectedSprite);
                maxShotDelay = 0.2f;
                power = 1;
                speed = 0.5f;
                life = 4;
                break;

            /*default:
                selectedSprite = Sonic_h;
                ChangeDummyface(selectedSprite);
                speed = 1.5f;
                break;*/
        }
    }

    public void ChangeDummyface(Sprite newSprite) {
        SpriteRenderer dummyRenderer = gameObject.GetComponent<SpriteRenderer>();
        dummyRenderer.sprite = newSprite;
    }

    void MoveCharacter(Vector2 delta) {
        float newSpeed;
        if (isTouchTop && delta.y > 0 || isTouchBottom && delta.y < 0) {
            newSpeed = 0;
        } else{
            newSpeed = speed;
        }

        transform.position += new Vector3(0, delta.y * newSpeed * Time.deltaTime, 0);
    }

    void FireH()
    {
        if ((curShotDelay < maxShotDelay) || (isHurt == true))
        {
            return;
        }

        GameObject bullet = Instantiate(bulletH, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * shootspeed, ForceMode2D.Impulse);

        curShotDelay = 0;
    }
    void FireZ()
    {
        if ((curShotDelay < maxShotDelay) || (isHurt == true))
        {
            return;
        }

        GameObject bullet = Instantiate(bulletZ, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * shootspeed, ForceMode2D.Impulse);

        curShotDelay = 0;
    }

   
 

IEnumerator BlinkCoroutine(int blinkCount)
{
    startBlinking = true;

    for (int i = 0; i < blinkCount; i++)
    {
        yield return new WaitForSecondsRealtime(blinkDuration); // Realtime�� ����Ͽ� timescale�� ���� ���� �ʵ��� ��
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
    }

    startBlinking = false;
    GetComponent<SpriteRenderer>().enabled = true;
}
void FireS() {
        if ((curShotDelay < maxShotDelay) ||(isHurt == true))
        {
            return;
        }

        GameObject bullet = Instantiate(bulletS, transform.position, transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.left * shootspeed, ForceMode2D.Impulse);

        curShotDelay = 0;
    }

    void Reload() {
        curShotDelay += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Border") {
            switch (collision.gameObject.name) {
                case "Top":
                    isTouchTop = true;
                    break;

                case "Bottom":
                    isTouchBottom = true;
                    break;
            }
        } else if (collision.gameObject.tag == "MonsterBullet") {
            Destroy(collision.gameObject);

            if (isHurt) {
                return;
            }

            life--;
            bgm.Play();
            ui.UpdateLifeIcon(life);

            if (life <= 0)
            {
                bgm.Play();
                Invoke("DestroyObject", 3f);
                SoundManager.instance.DestroyBgm();
                GameManager.instance.GameOver();
            }
            else
            {
                RespawnPlayer();
            }

            if (isHurt == false)
            {
                StartCoroutine(BlinkCoroutine(10)); // 5�� �����
            }
            isHurt = true;

            RespawnPlayer();
        }
        else if (collision.gameObject.CompareTag("Monster"))
        {
            if (isHurt)
            {
                return;
            }

            life--;
            bgm.Play();
            ui.UpdateLifeIcon(life);

            if (life <= 0)
            {
                bgm.Play();
                Invoke("DestroyObject", 3f);
                SoundManager.instance.DestroyBgm();
                GameManager.instance.GameOver();
            }
            else
            {
                RespawnPlayer();
            }

            if (isHurt == false)
            {
                StartCoroutine(BlinkCoroutine(10)); // 5�� �����
            }
            isHurt = true;

            RespawnPlayer();
        }
    }

    private void DestroyObject() {
        Destroy(this.gameObject);
        Time.timeScale = 0;
    }

    public void RespawnPlayer() {

        Invoke("RespawnPlayerExe", 2f);
        // player.transform.position = Vector3.down * 3.5;
        //player.SetActive(true);
    }

    public void RespawnPlayerExe() {

        Player playerLogic = gameObject.GetComponent<Player>();
        playerLogic.isHurt = false;

    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Border") {
            switch (collision.gameObject.name) {
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