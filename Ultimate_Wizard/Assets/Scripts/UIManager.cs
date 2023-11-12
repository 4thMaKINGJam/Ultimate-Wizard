using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Image[] lifeImage;
    public Sprite fulllife;
    public Sprite emptylife;
    public GameObject gameOverSet;

    public GameObject GameoverUI;

    public Image fadeInImg;

    private int dum = -1;

    // Start is called before the first frame update
    void Awake() {
        lifeImage[3].gameObject.SetActive(false);
        dum = PlayerPrefs.GetInt("dum");
        // 좀비인 경우
        if(dum == 2) {
            lifeImage[3].gameObject.SetActive(true);
        }
        HideGameOverUI();
        
    }

    public void HideGameOverUI()
    {
        gameOverSet.SetActive(false);
        GameoverUI.SetActive(false);
    }

    public void ShowGameOverUI()
    {
        gameOverSet.SetActive(true);
        GameoverUI.SetActive(true);
    }

    public void UpdateLifeIcon(int life)
    {
        foreach (Image img in lifeImage)
        {
            img.sprite = emptylife;
            for (int index = 0; index < life; index++)
            {
                lifeImage[index].sprite = fulllife;
            }
        }
    }

    public void FadeIn()
    {
        // 화면 전환
        StartCoroutine(FadeInCoroutine());
        SoundManager.instance.DestroyBgm();
        Debug.Log("FadeIN 호출");
    }

    IEnumerator FadeInCoroutine()
    {
        // 처음 알파값
        float fadeCount = 0;

        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            fadeInImg.color = new Color(0, 0, 0, fadeCount);
            yield return new WaitForSecondsRealtime(0.01f);
        }

        // 화면 전환
        SceneManager.LoadScene("4_Ending_Narr");
        Debug.Log("씬 로딩");
    }
}
