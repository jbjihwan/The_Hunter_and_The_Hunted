using UnityEngine;

/// <summary>
/// [DragonFollower]
/// - Stage3에서 드래곤이 플레이어 뒤 일정 거리를 유지하며 추적
/// - Z축 기준으로 플레이어보다 뒤에서 따라옴
/// - 애니메이션 자동 제어 ("Run", "BasicAttack", "FireballShoot", "Scream", "TailAttack" 등)
/// </summary>
public class DragonFollower : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("추적할 플레이어 Transform")]
    public Transform player;

    [Header("Follow Settings")]
    [Tooltip("플레이어 뒤에서 유지할 Z축 거리")]
    public float followDistanceZ = 20f;

    [Tooltip("X축 오프셋 (0 = 플레이어와 같은 X 위치)")]
    public float offsetX = 0f;

    [Tooltip("Y축 오프셋 (플레이어보다 높이 조정)")]
    public float offsetY = 0f;

    [Header("Movement")]
    [Tooltip("드래곤 이동 속도 (부드러운 추적)")]
    public float smoothSpeed = 5f;

    [Tooltip("즉시 따라가기 (true = 부드러운 이동 비활성화)")]
    public bool instantFollow = false;

    [Header("Animation")]
    [Tooltip("드래곤 Animator 컴포넌트")]
    public Animator dragonAnimator;

    [Tooltip("재생할 애니메이션 트리거 이름 목록")]
    public string[] animationTriggers = { "Run"/*, "BasicAttack", "FireballShoot", "Scream", "TailAttack"*/ };

    [Tooltip("애니메이션 전환 간격 (초)")]
    public float animationChangeInterval = 3f;

    [Tooltip("랜덤 애니메이션 재생")]
    public bool randomAnimation = true;

    [Tooltip("애니메이션 자동 재생 활성화")]
    public bool enableAnimation = true;

    private float animationTimer = 0f;
    private int currentAnimationIndex = 0;

    private void Start()
    {
        // 플레이어 자동 찾기 (할당 안 되어있을 경우)
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("[DragonFollower] Player not found! Assign player transform manually.");
            }
        }

        // Animator 자동 찾기
        if (dragonAnimator == null)
        {
            dragonAnimator = GetComponent<Animator>();
            if (dragonAnimator == null)
            {
                dragonAnimator = GetComponentInChildren<Animator>();
            }
        }

        // 첫 애니메이션 재생
        if (enableAnimation && dragonAnimator != null && animationTriggers.Length > 0)
        {
            PlayAnimation(0);
        }
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // 목표 위치 계산
        Vector3 targetPosition = CalculateTargetPosition();

        // 위치 업데이트
        if (instantFollow)
        {
            // 즉시 이동
            transform.position = targetPosition;
        }
        else
        {
            // 부드럽게 이동
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                smoothSpeed * Time.deltaTime
            );
        }

        // 드래곤이 플레이어 방향을 바라보도록
        LookAtPlayer();

        // 애니메이션 업데이트
        UpdateAnimation();
    }

    /// <summary>
    /// 드래곤의 목표 위치 계산
    /// </summary>
    private Vector3 CalculateTargetPosition()
    {
        return new Vector3(
            0,                                      // X: 0
            0,                                      // Y: 0
            player.position.z - followDistanceZ     // Z: 플레이어 뒤 거리
        );
    }

    /// <summary>
    /// 드래곤이 플레이어를 바라보도록 회전
    /// </summary>
    private void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        
        // Y축 회전만 적용 (수평 회전)
        direction.y = 0f;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                smoothSpeed * Time.deltaTime
            );
        }
    }

    /// <summary>
    /// 애니메이션 자동 전환 업데이트
    /// </summary>
    private void UpdateAnimation()
    {
        if (!enableAnimation || dragonAnimator == null || animationTriggers.Length == 0)
            return;

        animationTimer += Time.deltaTime;

        if (animationTimer >= animationChangeInterval)
        {
            animationTimer = 0f;

            if (randomAnimation)
            {
                // 랜덤 애니메이션 재생 (현재와 다른 애니메이션)
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, animationTriggers.Length);
                }
                while (randomIndex == currentAnimationIndex && animationTriggers.Length > 1);

                PlayAnimation(randomIndex);
            }
            else
            {
                // 순차 애니메이션 재생
                currentAnimationIndex = (currentAnimationIndex + 1) % animationTriggers.Length;
                PlayAnimation(currentAnimationIndex);
            }
        }
    }

    /// <summary>
    /// 특정 인덱스의 애니메이션 재생
    /// </summary>
    private void PlayAnimation(int index)
    {
        if (dragonAnimator == null || animationTriggers.Length == 0)
            return;

        if (index < 0 || index >= animationTriggers.Length)
        {
            Debug.LogWarning($"[DragonFollower] Invalid animation index: {index}");
            return;
        }

        string triggerName = animationTriggers[index];
        
        if (!string.IsNullOrEmpty(triggerName))
        {
            // 모든 트리거 리셋 (중복 방지)
            foreach (string trigger in animationTriggers)
            {
                if (!string.IsNullOrEmpty(trigger))
                {
                    dragonAnimator.ResetTrigger(trigger);
                }
            }

            // 새 트리거 설정
            dragonAnimator.SetTrigger(triggerName);
            currentAnimationIndex = index;

            Debug.Log($"[DragonFollower] Playing animation: {triggerName}");
        }
    }

    /// <summary>
    /// 수동으로 특정 애니메이션 재생 (외부 호출용)
    /// </summary>
    public void PlayAnimationByName(string animationName)
    {
        if (dragonAnimator == null) return;

        // 모든 트리거 리셋
        foreach (string trigger in animationTriggers)
        {
            if (!string.IsNullOrEmpty(trigger))
            {
                dragonAnimator.ResetTrigger(trigger);
            }
        }

        dragonAnimator.SetTrigger(animationName);
        animationTimer = 0f; // 타이머 리셋
    }

    /// <summary>
    /// 애니메이션 전환 간격 변경
    /// </summary>
    public void SetAnimationInterval(float interval)
    {
        animationChangeInterval = Mathf.Max(0.1f, interval);
    }

    /// <summary>
    /// 플레이어와의 현재 거리 반환 (Z축 기준)
    /// </summary>
    public float GetDistanceToPlayer()
    {
        if (player == null) return 0f;
        return player.position.z - transform.position.z;
    }

    /// <summary>
    /// 디버그용: 씬 뷰에서 추적 거리 시각화
    /// </summary>
    private void OnDrawGizmos()
    {
        if (player == null) return;

        // 목표 위치 표시
        Vector3 targetPos = CalculateTargetPosition();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(targetPos, 1f);

        // 플레이어와 드래곤 사이 선
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, player.position);

        // 거리 표시
        Gizmos.color = Color.cyan;
        Vector3 midPoint = (transform.position + player.position) / 2f;
        Gizmos.DrawSphere(midPoint, 0.5f);
    }
}