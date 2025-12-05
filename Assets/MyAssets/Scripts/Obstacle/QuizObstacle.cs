// 생성될 때 QuizManager에서 문제를 가져와서 큐브에 랜덤으로 배치함
using UnityEngine;

public class QuizObstacle : MonoBehaviour
{
    public QuizCube[] quizCubes;

    private bool triggered;

    void Start()
    {
        triggered = false;
        ImplantOptions();
    }

    public void ImplantOptions()
    {

    }

    public void Triggered(int order)
    {
        if(triggered)
        {
            return;
        }

        triggered = true;

    }
}
