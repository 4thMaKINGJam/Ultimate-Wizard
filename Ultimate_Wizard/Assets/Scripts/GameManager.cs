using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    public Image[] lifeImage;
    public GameObject gameOverSet;

    public GameObject GamoverUI;

    public static GameManager instance = null;

    private void Awake()
    {
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

        gameOverSet.SetActive(false);
        GamoverUI.SetActive(false);
    }


    private void Update()
    {


    }

    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }

        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void GameClear()
    {
        Time.timeScale = 0f;
        Debug.Log("GameManager 호출 성공");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverSet.SetActive(true);
        GamoverUI.SetActive(true);
    }

    public void GameRetry()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(1);
    }
}