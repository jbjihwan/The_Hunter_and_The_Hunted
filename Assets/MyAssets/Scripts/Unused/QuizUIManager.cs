using UnityEngine;
using TMPro;
using System.Collections;

public class QuizUIManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public float showTime = 3f;

    Coroutine routine;

    public void ShowQuestion(string text)
    {
        if (routine != null)
            StopCoroutine(routine);

        routine = StartCoroutine(Show(text));
    }

    IEnumerator Show(string text)
    {
        questionText.text = text;
        questionText.gameObject.SetActive(true);

        yield return new WaitForSeconds(showTime);

        questionText.gameObject.SetActive(false);
        routine = null;
    }
}