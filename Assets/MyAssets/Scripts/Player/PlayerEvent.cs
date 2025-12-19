using System.Collections;
using UnityEngine;

/// <summary>
/// [PlayerEvent]
/// - 플레이어 체력 및 데미지 처리
/// - 아이템 픽업 FX/SFX 처리
/// </summary>
public class PlayerEvent : MonoBehaviour
{
    public enum DifficultyLevel
    {
        EASY,
        NORMAL,
        HARD
    }

    /* =======================
     * HP
     * ======================= */
    [Header("HP")]
    public int maxHP = 5;
    public int easyHP;
    public int normalHP;
    public int hardHP;

    public int currentHP { get; private set; }

    /* =======================
     * Invincible
     * ======================= */
    [Header("Invincible")]
    public float invincibleDuration = 3f;
    public float flickerInterval = 0.15f;
    public Renderer[] targetRenderers;

    /* =======================
     * UI
     * ======================= */
    [Header("UI")]
    public CircleHpUI hpUI;

    /* =======================
     * Effects
     * ======================= */
    [Header("Trap Hit Effect")]
    public GameObject HitStage12Effect;
    public GameObject HitStage3Effect;

    [Header("Quiz Correct Effect")]
    public GameObject quizCorrectEffect;

    /* =======================
     * Pickup FX / SFX
     * ======================= */
    [Header("Pickup FX")]
    public GameObject coinPickupFx;
    public GameObject heartPickupFx;
    public float pickupFxLifeTime = 1.2f;
    public Vector3 pickupFxOffset = Vector3.up;

    

    public bool isInvincible { get; private set; }
    public bool isDead { get; private set; }

    Coroutine invincibleCoroutine;

    /* =======================
     * Unity LifeCycle
     * ======================= */
    void Awake()
    {
        currentHP = maxHP;

        if (targetRenderers == null || targetRenderers.Length == 0)
            targetRenderers = GetComponentsInChildren<Renderer>();
    }

    void Start()
    {
        if (hpUI == null && UIManager.Instance != null)
            hpUI = UIManager.Instance.circleHpUI;

        if (hpUI != null)
        {
            hpUI.SetMaxValue(maxHP);
            hpUI.SetValue(currentHP);
        }
    }

    /* =======================
     * Damage / Heal
     * ======================= */
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        if (damage > 0 && isInvincible) return;

        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);

        if (hpUI != null)
            hpUI.SetValue(currentHP);

        if (currentHP <= 0)
        {
            Die();
            return;
        }

        if (damage > 0)
        {
            if (invincibleCoroutine != null)
                StopCoroutine(invincibleCoroutine);

            invincibleCoroutine = StartCoroutine(InvincibleRoutine());
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHP = Mathf.Clamp(currentHP + amount, 0, maxHP);

        if (hpUI != null)
            hpUI.SetValue(currentHP);
    }

    public void SetHP(int max, int curr)
    {
        maxHP = max;
        currentHP = Mathf.Clamp(curr, 0, maxHP);

        if (hpUI != null)
        {
            hpUI.SetMaxValue(maxHP);
            hpUI.SetValue(currentHP);
        }
    }

    /* =======================
     * Pickup Events
     * ======================= */
    public void OnPickupCoin(int points)
    {
        GameManager.Instance.AddScore(points);
        PlayPickupFx(coinPickupFx);

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySfx(9);
    }

    public void OnPickupHeart(int healAmount)
    {
        Heal(healAmount);
        PlayPickupFx(heartPickupFx);

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySfx(10);
    }

    void PlayPickupFx(GameObject fxPrefab)
    {
        if (fxPrefab == null) return;

        Vector3 spawnPos = transform.position + pickupFxOffset;

        GameObject fx = Instantiate(
            fxPrefab,
            spawnPos,
            Quaternion.identity,
            transform
        );

        Destroy(fx, pickupFxLifeTime);
    }
    /* =======================
     * Trap / Quiz
     * ======================= */
    public void OnTrapHit(int damage)
    {
        if (isDead || isInvincible) return;

        PlayTrapHitFeedback(transform.position);
        TakeDamage(damage);
    }

    public void OnQuizWrong(int damage)
    {
        if (isDead || isInvincible) return;

        PlayTrapHitFeedback(transform.position);

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySfx(7);

        TakeDamage(damage);
    }

    public void OnQuizCorrect()
    {
        if (quizCorrectEffect != null)
            Instantiate(quizCorrectEffect, transform.position, Quaternion.identity, transform);

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySfx(6);
    }

    /* =======================
     * Death / Invincible
     * ======================= */
    void Die()
    {
        if (isDead) return;
        isDead = true;

        if (invincibleCoroutine != null)
            StopCoroutine(invincibleCoroutine);

        SetRenderersVisible(true);
        isInvincible = false;

        if (GameManager.Instance != null)
            GameManager.Instance.GameOver();
    }

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

        SetRenderersVisible(true);
        isInvincible = false;
    }

    void SetRenderersVisible(bool visible)
    {
        foreach (Renderer r in targetRenderers)
            if (r != null) r.enabled = visible;
    }

    void PlayTrapHitFeedback(Vector3 pos)
    {
        if (GameManager.Instance == null) return;

        int stage = GameManager.Instance.CurrentStage;
        GameObject prefab = (stage == 3) ? HitStage3Effect : HitStage12Effect;

        if (prefab != null)
            Instantiate(prefab, pos + Vector3.up, Quaternion.identity, transform);

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySfx(stage == 3 ? 5 : 4);
    }

    public void ChangeDifficultyLevel(int level)
    {
        if (level == (int)DifficultyLevel.EASY)
        {
            SetHP(easyHP, easyHP);
        }
        else if (level == (int)DifficultyLevel.NORMAL)
        {
            SetHP(normalHP, normalHP);
        }
        else if (level == (int)DifficultyLevel.HARD)
        {
            SetHP(hardHP, hardHP);
        }

        UIManager.Instance.OffDifficultyLevelUI();
    }
}
