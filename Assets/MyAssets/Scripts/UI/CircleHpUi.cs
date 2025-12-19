using UnityEngine;
using UnityEngine.UI;

public class CircleHpUI : MonoBehaviour
{
    [Header("HP Fill")]
    public Image fillImage;   // 원형 HP (Radial)

    [Header("Face Images (4 -> 0 HP)")]
    public GameObject[] faceImages;
    // index 0 : HP 4/4 (최대)
    // index 1 : HP 3/4
    // index 2 : HP 2/4
    // index 3 : HP 1/4

    int maxValue = 1;
    int currentValue = 1;

    /* =========================
     * HP 설정 (hpSlider 스타일)
     * ========================= */
    public void SetMaxValue(int max)
    {
        maxValue = Mathf.Max(1, max);
        UpdateFill();
        UpdateFace();
    }

    public void SetValue(int value)
    {
        currentValue = Mathf.Clamp(value, 0, maxValue);
        UpdateFill();
        UpdateFace();
    }

    public void Set(int max, int value)
    {
        maxValue = Mathf.Max(1, max);
        currentValue = Mathf.Clamp(value, 0, maxValue);
        UpdateFill();
        UpdateFace();
    }

    /* =========================
     * 내부 처리
     * ========================= */
    void UpdateFill()
    {
        if (fillImage == null) return;
        fillImage.fillAmount = (float)currentValue / maxValue;
    }

    void UpdateFace()
    {
        if (faceImages == null || faceImages.Length == 0) return;

        // 전부 끄기
        for (int i = 0; i < faceImages.Length; i++)
            faceImages[i].SetActive(false);

        float ratio = (maxValue <= 0) ? 0f : (float)currentValue / maxValue;

        // faceImages는 4개라고 가정 (0:좋음, 3:최악)
        int idx;

        if (ratio >= 0.75f) idx = 0;        // 100~75
        else if (ratio >= 0.50f) idx = 1;   // 75~50
        else if (ratio >= 0.25f) idx = 2;   // 50~25
        else idx = 3;                       // 25~0

        // 안전장치 (혹시 faceImages 길이가 4가 아니어도 터지지 않게)
        idx = Mathf.Clamp(idx, 0, faceImages.Length - 1);

        faceImages[idx].SetActive(true);
    }

    int GetFaceIndex()
    {
        // 예: maxHP = 4 기준
        // 4 → 0, 3 → 1, 2 → 2, 1 → 3
        int lostHp = maxValue - currentValue;
        return Mathf.Clamp(lostHp, 0, faceImages.Length - 1);
    }
}