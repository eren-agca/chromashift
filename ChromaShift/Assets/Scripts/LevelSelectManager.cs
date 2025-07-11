using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class LevelSlot
{
    [Tooltip("Bu slotun temsil ettiği seviyenin Build Index'i")]
    public int levelIndex;
    [Tooltip("Tıklanacak olan asıl Buton component'i")]
    public Button button;
    [Tooltip("Kilitli olduğunda gösterilecek olan ikon")]
    public GameObject lockIcon;
    [Tooltip("Seviye numarasını gösteren TextMeshPro objesi")]
    public TextMeshProUGUI levelText;
}

public class LevelSelectManager : MonoBehaviour
{
    [Header("Seviye Slotları")]
    [Tooltip("Her bir seviye için UI elemanlarını buraya sürükleyin")]
    public List<LevelSlot> levelSlots = new List<LevelSlot>();

    void Start()
    {
        int highestLevelReached = PlayerPrefs.GetInt("highestLevelReached", 1);
        Debug.Log($"Oyuncunun ulaştığı en yüksek seviye: {highestLevelReached}");

        foreach (var slot in levelSlots)
        {
            if (slot.levelText != null)
            {
                slot.levelText.text = slot.levelIndex.ToString();
            }

            if (slot.levelIndex <= highestLevelReached)
            {
                UnlockSlot(slot);
            }
            else
            {
                LockSlot(slot);
            }
        }
    }

    private void LockSlot(LevelSlot slot)
    {
        if (slot.button != null) slot.button.interactable = false;
        if (slot.lockIcon != null) slot.lockIcon.SetActive(true);
    }

    private void UnlockSlot(LevelSlot slot)
    {
        if (slot.button != null)
        {
            slot.button.interactable = true;
            slot.button.onClick.RemoveAllListeners();
            slot.button.onClick.AddListener(() => LoadLevel(slot.levelIndex));
        }
        
        if (slot.lockIcon != null) slot.lockIcon.SetActive(false);
    }

    public void LoadLevel(int index)
    {
        Debug.Log($"Seviye {index} yükleniyor...");
        SceneManager.LoadScene(index);
    }
}