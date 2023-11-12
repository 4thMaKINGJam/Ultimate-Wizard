using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] private Image image; // 페이드아웃 효과에 쓰일 이미지
    [SerializeField] private GameObject start_button; // 시작 버튼
    [SerializeField] private GameObject end_button; // 시작 버튼
    [SerializeField] private GameObject title;

    private void Start() {
        SoundManager.instance.BgmPlay(4);
    }

    public void FadeButton() {
        Debug.Log("게임 시작");
        SoundManager.instance.DestroyBgm();
        title.SetActive(false);
        start_button.SetActive(false); // 버튼 클릭시 버튼 비활성화
        end_button.SetActive(false); // 버튼 클릭시 버튼 비활성화
        StartCoroutine("FadeOutCoroutine");
    }

    IEnumerator FadeOutCoroutine() {
        // 처음 알파값
        float fadeCount = 0;

        while(fadeCount < 1.0f) {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01초마다 실행
            image.color = new Color(0, 0, 0, fadeCount);
        }

        StopCoroutine("FadeOutCoroutine");
        // 화면 전환
        SceneManager.LoadScene("1_StoryFirst");
    }

    public void EndButton()
    {
        Application.Quit();
    }
}
