using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuestionController : MonoBehaviour
{
    [SerializeField] private Image image; // 페이드아웃 효과에 쓰일 이미지
    [SerializeField] private float delay_time = 0.5f;

    // 제일 상단 헤더와 바디 내용
    [SerializeField] private GameObject quest_box;
    [SerializeField] private Text head;
    [SerializeField] private Text body;
    [SerializeField] private GameObject next_button;

    // 상단 건너뛰기 & 끝내기 버튼
    [SerializeField] private GameObject button_ui;

    // 캐릭터 & 더미
    [SerializeField] private SpriteRenderer sprite_character;
    [SerializeField] private SpriteRenderer sprite_dummy;

    // 출력 내용
    [SerializeField] private Character[] questions;
    private string text = "";
    private int count = 0; // 대화 얼마나 진행되었는지

    // 초기 세팅
    private void Awake() {
        image.gameObject.SetActive(true);
        ShowQuestion(false);
    }

    // Start is called before the first frame update
    void Start() {
        Debug.Log("3_Dummy 트레이닝 시작");

        // 배경 이미지 페이드 인 효과
        StartCoroutine("FadeInCoroutine");
    }

    IEnumerator FadeInCoroutine() {
        // 처음 알파값
        float fadeCount = 1;

        while(fadeCount > 0.0f) {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }

        // 다음 애니메이션 전 딜레이
        yield return new WaitForSeconds(delay_time);
        StopCoroutine("FadeInCoroutine");

        // 훈련 시작
        StartQuestion();
    }

    private void StartQuestion() {
        Debug.Log("훈련 시작");
        // 훈련 UI 활성화
        ShowQuestion(true);
        count = 0;

        // 다음 대화 진행
        StartCoroutine("Next");
    }

    private void ShowQuestion(bool flag) {
        quest_box.gameObject.SetActive(flag);
        next_button.gameObject.SetActive(flag);
        button_ui.SetActive(flag);
        sprite_character.gameObject.SetActive(flag);
        sprite_dummy.gameObject.SetActive(flag);
    }

    // 다이얼로그 시작 (Question_Box)
    IEnumerator Next() {
        if(count < questions.Length) {
            // 헤더
            head.text = questions[count].name;

            // 텍스트 타이핑 효과
            string s = questions[count].dialogue;
            count++;

            text = "";
            body.text = "";
            yield return new WaitForSeconds(0.3f);

            for (int i = 0; i < s.Length; i++) {
                text += s[i];
                body.text = text;
                yield return new WaitForSeconds(0.05f);
            }
        } else {
            Debug.Log("출력 완료");
        }
    }

    // 다이얼로그 창 누르면 전체 대화 출력
    public void DialogueOnClick() {
        if(count > 0) {
            body.text = questions[--count].dialogue;
            count++;
        }
    }

    public void printQuest() {
        if(count < questions.Length) {
            // 헤더
            head.text = questions[count].name;

            // 바디
            body.text = questions[count].dialogue;
            count++;
        } else {
            Debug.Log("출력 완료");
        }
    }

    // 1주차 ~ 5주차 훈련 시작
    public void NextButton() {
        next_button.gameObject.SetActive(false);
        // 전체 대화 출력 후 다음 대화로 넘어가기
        printQuest();
    }

    public void End() {
        SceneManager.LoadScene("0_Main");
    }

    public void Retry() {
        ShowQuestion(false);
        StartQuestion();
    }
}