using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInShooting : MonoBehaviour
{
    public Image img;
    public void EndDialogue()
    {
        // 화면 전환
        StartCoroutine("FadeInCoroutine");
    }

    IEnumerator FadeInCoroutine()
    {
        // 처음 알파값
        float fadeCount = 0;

        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f); // 0.01초마다 실행
            img.color = new Color(0, 0, 0, fadeCount);
        }

        StopCoroutine("FadeInCoroutine");
        // 화면 전환
        SceneManager.LoadScene("6_Ending");
    }
}
