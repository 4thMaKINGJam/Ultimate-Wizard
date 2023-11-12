using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainingController : MonoBehaviour
{
    // 훈련 내용
    [SerializeField] private GameObject ui;
    [SerializeField] private Text train_1;
    [SerializeField] private Text train_2;
    [SerializeField] private Text train_3;

    [SerializeField] private Narrator[] options;
    private int count = 0; // 대화 얼마나 진행되었는지

    // 훈련 결과
    // 순서대로 스피드, 공격력, 체력
    private List<int> types;
    private List<int> candidates = new List<int>();
    private List<string> type_name = new List<string>() {"스피드", "공격", "체력"};

    // 결과 화면[SerializeField]
    [SerializeField] private GameObject EvolutionUI;
    [SerializeField] private Text text;
    [SerializeField] private Image[] dummies;
    [SerializeField] private AudioSource bgm;

    // 초기 세팅
    private void Awake() {
        // textChangeInstance = FindObjectOfType<textchange>();
        EvolutionUI.SetActive(false);
        ShowTrain(false);
    }

    private void ShowTrain(bool flag) {
        types = new List<int>() {0, 0, 0};
        candidates = new List<int>();
        count = 0;
        ui.gameObject.SetActive(flag);
    }

    public void NextButton() {
        ShowTrain(true);
        count = 0;

        Choice();
    }

    public void Choice() {
        // 선택지 남아있을 경우
        if(count < options.Length) {
            // 3개 선택지 출력
            train_1.text = options[count++].dialogue;
            train_2.text = options[count++].dialogue;
            train_3.text = options[count++].dialogue;
        } else { // 선택지 출력 끝난 경우
            DummyResult();
        }
    }

    public void CreateDummy(int type) {
        types[type]++;
    }

    private void DummyResult() {
        // 최댓값 받기
        int maxVal = types.Max();

        // types 배열에서 최댓값에 해당하는 인덱스 번호 저장
        for (int i = 0; i < types.Count; i++){
            if(maxVal == types[i]) {
                candidates.Add(i);
            }
        }

        // 결과 반환
        int random = Random.Range(0, candidates.Count);
        Debug.Log($"result(0 스피드, 1 공격력, 2 체력): '{type_name[candidates[random]]}'");

        PlayerPrefs.SetInt("dum", candidates[random]);

        Evolution_dummy(candidates[random]);
        changeText(candidates[random]);
    }

    private void changeText(int candidates)
    {
        switch (candidates)
        {
            case 0:
                text.text = "더미가 소닉으로 진화했어!";
                break;

            case 1:
                text.text = "더미가 헤라클레스로 진화했어!";
                break;

            case 2:
                text.text = "더미가 좀비로 진화했어!";
                break;
        }
    }

    public void Evolution_dummy(int idx) {
        dummies[idx].gameObject.SetActive(true);
        EvolutionUI.SetActive(true);
        bgm.Play();
    }

    public void NextScene(string scene_name) {
        // 화면 전환
        SoundManager.instance.DestroyBgm();
        SceneManager.LoadScene(scene_name);
    }

    public void Retry() {
        SoundManager.instance.DestroyBgm();
        SceneManager.LoadScene("2_DummySelect");
        ShowTrain(false);
    }
}
