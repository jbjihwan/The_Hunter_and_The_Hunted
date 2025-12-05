using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        // 컴포넌트 가져오기
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. 입력 받기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 이동 방향 설정 (로컬 기준이 아닌 월드 기준 변환 필요 시 transform.TransformDirection 사용)
        // 여기서는 간단하게 보는 방향 기준으로 이동 구현
        Vector3 moveDirection = new Vector3(h, 0, v);

        // 로컬 좌표(캐릭터 기준)를 월드 좌표로 변환 (캐릭터가 회전해도 앞이 '앞'이 되도록)
        moveDirection = transform.TransformDirection(moveDirection);

        // 3. 실제 이동 (SimpleMove는 중력을 자동으로 적용해줌)
        controller.SimpleMove(moveDirection * speed);

        // 4. 애니메이션 연동 (선택 사항)
        // 움직임이 있을 때(벡터 길이가 0.1 이상일 때) 걷는 애니메이션 켜기
        if (moveDirection.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
