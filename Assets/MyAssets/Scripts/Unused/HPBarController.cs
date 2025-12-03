using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// [HPBarController]
/// - 플레이어의 체력을 UI 슬라이더에 반영하는 스크립트.
/// - PlayerEvent와 연결되어 currentHP / maxHP 기준으로 슬라이더를 업데이트.
/// </summary>
public class HPBarController : MonoBehaviour
{
    [Header("HP slider")]
    public Slider hpSlider;

    private PlayerEvent player;

    private void Start()
    {
        // 씬 안에서 PlayerEvent 자동 탐색
        player = FindObjectOfType<PlayerEvent>();

        // 슬라이더 기본값 세팅
        if (player != null && hpSlider != null)
        {
            hpSlider.maxValue = player.maxHP;
            hpSlider.value = player.currentHP;
        }
    }

    /// <summary>
    /// PlayerEvent에서 데미지가 발생할 때 호출하는 함수
    /// </summary>
    public void UpdateHP(int currentHP)
    {
        if (hpSlider != null)
            hpSlider.value = currentHP;
    }
}
