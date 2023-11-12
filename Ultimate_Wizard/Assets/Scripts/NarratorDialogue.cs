using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NarratorDialogue : MonoBehaviour
{
    [SerializeField] private Image image; // 페이드아웃 효과에 쓰일 이미지
    [SerializeField] private float delay_time = 1.0f;
    [SerializeField] private Animator anim; // 대화창 open, close 애니메이터
    [SerializeField] private string scene_name;

    [SerializeField] private Text sentence; // 대화 내용
    [SerializeField] private Image dialogue_box; // 다이얼로그 박스 제어 변수
    [SerializeField] private GameObject button_ui; // 상단 건너뛰기 & 끝내기 버튼

    private bool isDialogue = false; // 대화가 진행중인지
    private int count = 0; // 대화 얼마나 진행되었는지

    [SerializeField] private Narrator[] dialogues;

    public string text = "";

    // 사운드
    [SerializeField] private AudioSource bgm;

    // 초기 세팅
    private void Awake() {
        image.gameObject.SetActive(true);
        SoundManager.instance.SetBgm(bgm);
        sentence.text = "";
    }

    private void Start() {
        Time.timeScale = 1f;
        Debug.Log("4_Ending_Narr 진행 시작");
        // 대화 화면 비활성화
        StartDialogue(false);
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
        // 대화창 애니메이션 시작
        anim.SetBool("isOpen", true);

        // 다음 애니메이션 전 딜레이
        yield return new WaitForSeconds(delay_time);
        StopCoroutine("FadeInCoroutine");

        // 다이얼로그 시작
        ShowDialogue();
    }

    public void ShowDialogue() {
        // 대화 시작
        StartDialogue(true);
        count = 0;

        // 다음 대화 진행
        StartCoroutine("Next");
    }

    public void EndDialogue() {
        StopCoroutine("Next");
        // 텍스트 초기화
        sentence.text = "";

        // 대화 종료
        StartDialogue(false);
        anim.SetBool("isOpen", false);

        // 화면 전환
        StartCoroutine("FadeOutCoroutine");
    }

    private void StartDialogue(bool flag) {
        button_ui.SetActive(flag);
        isDialogue = flag;
    }

    // 다이얼로그 시작
    IEnumerator Next() {
        // 아직 스크립트 남아있을 경우
        if(count < dialogues.Length) {
            // 텍스트 타이핑 효과
            string s = dialogues[count].dialogue;
            count++;
            text = "";
            sentence.text = "";
            yield return new WaitForSeconds(0.3f);

            for (int i = 0; i < s.Length; i++) {
                text += s[i];
                sentence.text = text;
                yield return new WaitForSeconds(0.05f);
            }
        } else { // 모든 스크립트 출력 끝나면 다음 장면으로 넘어감
            EndDialogue();
        }
    }

    // 다이얼로그 창 누르면 전체 대화 출력
    public void DialogueOnClick() {
        StopCoroutine("Next");
        if(count > 0) {
            sentence.text = dialogues[--count].dialogue;
            count++;
        }
    }

    public void NextButton() {
        // 전체 대화 출력 후 다음 대화로 넘어가기
        DialogueOnClick();
        StartCoroutine("Next");
    }

    public void End() {
        SceneManager.LoadScene("0_Main");
    }

    public void Skip() {
        StopCoroutine("Next");
        EndDialogue();
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
        SceneManager.LoadScene(scene_name);
    }
}