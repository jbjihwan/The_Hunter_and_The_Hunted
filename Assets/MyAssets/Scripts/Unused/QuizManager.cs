using UnityEngine;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // 문제 정보
    public int a, b, correctAnswer;

    // 현재 라운드 큐브들
    private List<CubeQuizItem> activeCubes = new List<CubeQuizItem>();

    public string CurrentQuestionString => $"{a} x {b} = ?";

    public void RegisterCube(CubeQuizItem cube)
    {
        activeCubes.Add(cube);

        if (activeCubes.Count == 3)
        {
            SetupQuestion();
        }
    }

    private void SetupQuestion()
    {
        // 문제 생성
        a = Random.Range(2, 10);
        b = Random.Range(2, 10);
        correctAnswer = a * b;

        // 오답 2개
        int wrong1 = correctAnswer + Random.Range(1, 5);
        int wrong2 = correctAnswer - Random.Range(1, 5);
        if (wrong2 <= 0) wrong2 = correctAnswer + Random.Range(6, 12);

        int[] wrongs = { wrong1, wrong2 };
        int wi = 0;

        foreach (var cube in activeCubes)
        {
            if (cube.isAnswer)
                cube.SetValue(correctAnswer);
            else
            {
                cube.SetValue(wrongs[wi]);
                wi++;
            }
        }

        activeCubes.Clear();
    }
}
