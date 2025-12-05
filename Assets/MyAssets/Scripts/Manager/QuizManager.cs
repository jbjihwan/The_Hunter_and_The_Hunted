using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Quiz
    {
        public string[] options;
        public int answerIndex;
    }

    public Quiz[] quizzes;

    void Start()
    {

    }

    void Update()
    {

    }
}
