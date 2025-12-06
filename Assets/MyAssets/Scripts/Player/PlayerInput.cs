using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool moveLeft { get; private set; }
    public bool moveRight { get; private set; }
    public bool jump { get; private set; }
    public bool slide { get; private set; }

    private Vector2 touchStartPos;
    private bool isSwiping;

    void Start()
    {
        moveLeft = false;
        moveRight = false;
        jump = false;
        slide = false;

        touchStartPos = Vector2.zero;
        isSwiping = false;
    }

    void Update()
    {
        if (!GameManager.Instance.IsPlaying())
        {
            return;
        }

        // Windows Input
        moveLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        moveRight = Input.GetKeyDown(KeyCode.RightArrow);
        jump = Input.GetKeyDown(KeyCode.UpArrow);
        slide = Input.GetKeyDown(KeyCode.DownArrow);

        // Mobile Input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isSwiping = true;
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                isSwiping = false;
                Vector2 swipeDelta = touch.position - touchStartPos;

                if (swipeDelta.magnitude > 200f)
                {
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    {
                        if (swipeDelta.x < 0) moveLeft = true;
                        else moveRight = true;
                    }
                    else
                    {
                        if (swipeDelta.y > 0) jump = true;
                        else slide = true;
                    }
                }

            }
        }
    }
}
