// 플레이어에 부딫히면 자신의 순서를 퀴즈 장애물에 전달
using UnityEngine;
using TMPro;

public class QuizCube : MonoBehaviour
{
    public int order;
    public TextMeshProUGUI option;

    private QuizObstacle parent;

    private void Start()
    {
        parent = transform.parent.GetComponent<QuizObstacle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            parent.Triggered(order, other.gameObject.GetComponent<PlayerEvent>());
        }
    }
}
