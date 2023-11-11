using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    public Player player;

    [SerializeField]
    private dummy dummy;

    private textchange textChangeInstance;

    public Image[] lifeImage;
    public Sprite fulllife;
    public Sprite emptylife;
    public Sprite Sonic_h;
    public Sprite Hercules_h;
    public Sprite Zombie_h;
    public Sprite Sonic;
    public Sprite Hercules;
    public Sprite Zombie;

    //   public GameObject gameOverSet;

    Sprite selectedSprite = null;

    Sprite selectedSpritebody = null;

    //  public GameObject GamoverUI;

    public GameObject EvolutionUI;

    public static GameManager instance = null;


    private void Awake()

    {
        textChangeInstance = FindObjectOfType<textchange>();


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        EvolutionUI.SetActive(false);
    }


    private void Update()
    {
      
    }
    public void textchange_(int candidates)
    {

        dummy.changeText(candidates);

    }
    public void SelectDummy(int candidates)
    {
        player = GameObject.FindObjectOfType<Player>();

        switch (candidates)
            {

            case 0:
                selectedSprite = Sonic_h;
                selectedSpritebody = Sonic;
                dummy.ChangeDummyImage(selectedSpritebody);
                Evolution_dummy();
                //    player.GetComponent<Player>().speed = 5.0f;

                break;
            case 1:
                selectedSprite = Hercules_h;
                selectedSpritebody = Hercules;
            //    player.GetComponent<Player>().power = 2.0f;
                dummy.ChangeDummyImage(selectedSpritebody);
                Evolution_dummy();
                break;
            case 2:
                selectedSprite = Zombie_h;
                selectedSpritebody = Zombie;
                dummy.ChangeDummyImage(selectedSpritebody);
                Evolution_dummy();
                //   player.GetComponent<Player>().life = 4;
                break;
            
            default:
                selectedSprite = Sonic_h;
                selectedSpritebody = Sonic;
                dummy.ChangeDummyImage(selectedSpritebody);
                Evolution_dummy();
                // player.GetComponent<Player>().speed = 5.0f;
                break;

        }

        dummy.GetComponent<dummy>().MoveToPosition(0f, 0f);

        if (selectedSprite != null)
        {


            // 플레이어의 이미지 변경
          //  player.GetComponent<Image>().sprite = selectedSprite;
        }
    }

    public void GameClear()
    {
        Time.timeScale = 0f;
        Debug.Log("GameManager 호출 성공");
        // TODO: Ending 씬 불러오기
    }
    public void Evolution_dummy()
    {
        EvolutionUI.SetActive(true);
    }

    public void Letsgo()
    {
        SceneManager.LoadScene(7);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        UIManager ui = GameObject.FindObjectOfType<UIManager>();
        ui.ShowGameOverUI();        
    }
    public void GameRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}