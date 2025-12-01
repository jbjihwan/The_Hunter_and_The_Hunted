using UnityEngine;
using TMPro;

public class CubeQuizItem : MonoBehaviour
{
    [Header("Cube Role")]
    public bool isAnswer;                // 정답 큐브면 true, 오답은 false

    [Header("Value Display")]
    public TextMeshPro textMesh;         // 큐브 위에 표시되는 숫자 텍스트

    public int shownValue;               // 이번 라운드에서 이 큐브가 가진 값

    private void OnEnable()
    {
        // 큐브 스폰되자마자 퀴즈 매니저에게 등록
        QuizManager.Instance.RegisterCube(this);
    }

    public void SetValue(int value)
    {
        shownValue = value;

        if (textMesh != null)
            textMesh.text = value.ToString();
    }
}
