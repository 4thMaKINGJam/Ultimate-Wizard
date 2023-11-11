using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] private AudioSource bgm;

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
    }

    public void GameClear() {
       Time.timeScale = 0f;
        Debug.Log("GameManager 호출 성공");

        // TODO: Ending 씬 불러오기
        //SceneManager.LoadScene("4_Ending_Narr");
    }

    public void GameOver() {
        bgm.Play();
        Time.timeScale = 0f;
        UIManager ui = GameObject.FindObjectOfType<UIManager>();
        ui.ShowGameOverUI();        
    }

    public void Letsgo(string scene_name) {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scene_name);
    }

}