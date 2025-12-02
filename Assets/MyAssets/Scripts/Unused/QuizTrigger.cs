using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    public QuizUIManager uiManager;

    private void OnTriggerEnter(Collider other)
    {
        CubeQuizItem cube = other.GetComponent<CubeQuizItem>();
        if (cube == null) return;

        if (cube.isAnswer)
        {
            uiManager.ShowQuestion(QuizManager.Instance.CurrentQuestionString);
        }
    }
}