using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Quiz
    {
        [TextArea(1, 3)]
        public string quiz;

        [TextArea(1, 4)]
        public string[] options;

        public int answerIndex;
    }

    public static QuizManager Instance;

    public Quiz[] quizzes;
    public TextMeshProUGUI quizText;
    public float quizDist;

    private int quizIndex;
    private Queue<QuizObstacle> quizQueue;
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

    private void Start()
    {
        Helper.Shuffle(quizzes);
        quizIndex = 0;
        quizQueue = new Queue<QuizObstacle>();
        quizUI = quizText.transform.parent.gameObject;
    }

    private void Update()
    {
        UpdateQuizUI();
    }

    public void UpdateQuizUI()
    {
        if (quizQueue.Count > 0 && quizQueue.Peek().transform.position.z < quizDist)
        {
            quizUI.SetActive(true);
            quizText.text = quizQueue.Peek().quiz.quiz;
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
        return quiz;
    }

    public void PushQuiz(QuizObstacle quizObstacle)
    {
        quizQueue.Enqueue(quizObstacle);
    }

    public void PopQuiz()
    {
        quizQueue.Dequeue();
    }
}
