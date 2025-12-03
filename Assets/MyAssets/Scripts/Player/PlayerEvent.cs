using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// [PlayerEvent]
/// - 플레이어 체력 및 데미지 처리
/// - 함정/오답 등 모든 데미지 이벤트 처리
/// - 피격 후 일정 시간 무적 + 깜빡임 효과 제공
/// - UIManager.Instance.hpSlider 를 이용해 HP바 표시
/// </summary>
public class PlayerEvent : MonoBehaviour
{
    [Header("HP")]
    public int maxHP = 5;

    [Header("Invincible")]
    public float invincibleDuration = 3f;
    public float flickerInterval = 0.15f;
    public Renderer[] targetRenderers;

    [Header("UI")]
    public Slider hpSlider;   // 인스펙터에서 안 넣어도 Start에서 UIManager에서 가져옴

    public bool isInvincible { get; private set; } = false;
    public bool isDead { get; private set; } = false;
    public int currentHP { get; private set; }

    // 무적 코루틴 중복 방지
    Coroutine invincibleCoroutine;

    void Awake()
    {
        // 체력 초기화
        currentHP = maxHP;

        // targetRenderers 자동 등록 (Inspector에서 설정 안 했을 때)
        if (targetRenderers == null || targetRenderers.Length == 0)
            targetRenderers = GetComponentsInChildren<Renderer>();
    }

    void Start()
    {
        // HP 슬라이더를 인스펙터에서 안 넣었으면 UIManager에서 가져옴
        if (hpSlider == null && UIManager.Instance != null)
        {
            hpSlider = UIManager.Instance.hpSlider;
        }

        // HP 슬라이더 초기 세팅
        if (hpSlider != null)
        {
            hpSlider.minValue = 0;
            hpSlider.maxValue = maxHP;
            hpSlider.value = currentHP;
        }
    }

    /// <summary>
    /// 공통 데미지 처리
    /// </summary>
    public void TakeDamage(int damage)
    {
        // 죽었거나 무적이면 데미지 무시
        if (isDead || isInvincible) return;

        int finalDamage = Mathf.Max(1, damage); // 최소 데미지 1
        currentHP -= finalDamage;

        Debug.Log($"[PlayerEvent] Took {finalDamage} damage → Current HP: {currentHP}");

        // HP UI 업데이트
        if (hpSlider != null)
            hpSlider.value = currentHP;

        // 사망 처리
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
            return;
        }

        // 무적 + 깜빡임 시작
        if (invincibleCoroutine != null)
            StopCoroutine(invincibleCoroutine);

        invincibleCoroutine = StartCoroutine(InvincibleRoutine());
    }

    /// <summary>
    /// 함정 충돌 시 호출
    /// </summary>
    public void OnTrapHit(int baseDamage)
    {
        Debug.Log("[PlayerEvent] Hit by trap");
        TakeDamage(baseDamage);
    }

    /// <summary>
    /// 오답으로 데미지를 받을 때 호출
    /// </summary>
    public void OnWrongAnswer(int baseDamage)
    {
        Debug.Log("[PlayerEvent] Wrong answer damage");
        TakeDamage(baseDamage);
    }

    /// <summary>
    /// 플레이어 사망 처리
    /// </summary>
    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("[PlayerEvent] Player died");

        // 깜빡임 종료 및 정상 표시
        if (invincibleCoroutine != null)
            StopCoroutine(invincibleCoroutine);
        SetRenderersVisible(true);
        isInvincible = false;

        // GameManager GameOver 호출
        if (GameManager.Instance != null)
            GameManager.Instance.GameOver();
        else
            Debug.LogWarning("[PlayerEvent] GameManager not found");
    }

    /// <summary>
    /// 무적 + 깜빡임 코루틴
    /// </summary>
    IEnumerator InvincibleRoutine()
    {
        isInvincible = true;
        float elapsed = 0f;
        bool visible = true;

        while (elapsed < invincibleDuration)
        {
            visible = !visible;
            SetRenderersVisible(visible);

            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval;
        }

        // 무적 종료
        SetRenderersVisible(true);
        isInvincible = false;
    }

    /// <summary>
    /// 깜빡임용 Renderer 보이기/숨기기 조절
    /// </summary>
    void SetRenderersVisible(bool visible)
    {
        if (targetRenderers == null) return;

        foreach (Renderer rend in targetRenderers)
        {
            if (rend != null)
                rend.enabled = visible;
        }
    }
}
