using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Quiz
    {
        public string quiz;
        public string[] options;
        public int answerIndex;
    }

    public static QuizManager Instance;
    public Quiz[] quizzes;
    public TextMeshProUGUI quizText;

    private int quizIndex;
    private Queue<string> quizQueue;
    private GameObject quizUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Helper.Shuffle(quizzes);
        quizIndex = 0;
        quizQueue = new Queue<string>();
        quizUI = quizText.transform.parent.gameObject;
    }

    public void UpdateQuizUI()
    {
        if (quizQueue.Count > 0)
        {
            quizUI.SetActive(true);
            quizText.text = quizQueue.Peek();
        }
        else
        {
            quizUI.SetActive(false);
        }
    }

    public Quiz GetQuiz()
    {
        Quiz quiz = quizzes[quizIndex];
        quizIndex = (quizIndex + 1) % quizzes.Length;

        quizQueue.Enqueue(quiz.quiz);
        UpdateQuizUI();

        return quiz;
    }

    public void PopQuiz()
    {
        quizQueue.Dequeue();
        UpdateQuizUI();
    }
}
