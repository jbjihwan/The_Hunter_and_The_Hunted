// 생성될 때 QuizManager에서 문제를 가져와서 큐브에 랜덤으로 배치함
using UnityEngine;

public class QuizObstacle : MonoBehaviour
{
    public QuizCube[] quizCubes;

    private QuizManager.Quiz quiz;
    private bool triggered;
    private int answerCubeOrder;

    void Start()
    {
        quiz = QuizManager.Instance.GetQuiz();
        triggered = false;

        ImplantOptions();
    }

    public void ImplantOptions()
    {
        Helper.Shuffle(quizCubes);

        for (int i = 0; i < 3; ++i)
        {
            quizCubes[i].option.text = quiz.options[i];

            if (i == quiz.answerIndex)
            {
                answerCubeOrder = quizCubes[i].order;
            }
        }
    }

    public void Triggered(int order, PlayerEvent player)
    {
        if (triggered)
        {
            return;
        }

        triggered = true;
        QuizManager.Instance.PopQuiz();

        if (order == answerCubeOrder)
        {
            // 정답
            return;
        }
        else
        {
            // 오답
            player.OnQuizWrong(1);
        }
    }
}
