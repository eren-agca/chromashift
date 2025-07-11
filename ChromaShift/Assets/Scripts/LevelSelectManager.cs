using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    void OnEnable() { UpdateLevelButtons(); }
    void Start()    { UpdateLevelButtons(); }

    void UpdateLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("LevelUnlocked", 1);

        Button[] buttons = GetComponentsInChildren<Button>(true);

        foreach (Button btn in buttons)
        {
            string label = "";
            TMP_Text tmpText = btn.GetComponentInChildren<TMP_Text>();
            if (tmpText != null)
                label = tmpText.text.Trim();
            else
            {
                Text classicText = btn.GetComponentInChildren<Text>();
                if (classicText != null)
                    label = classicText.text.Trim();
            }

            int levelNum = 0;
            if (int.TryParse(label, out levelNum))
            {
                bool isUnlocked = (levelNum <= unlockedLevel);
                btn.interactable = isUnlocked;

                // İsmi "lock" (küçük l) içeren tüm image objelerini aç/kapat
                Image[] childImages = btn.GetComponentsInChildren<Image>(true);
                foreach (var img in childImages)
                {
                    if (img.gameObject.name.ToLower().Contains("lock"))
                        img.gameObject.SetActive(!isUnlocked);
                }
            }
            else
            {
                btn.interactable = false;
                Image[] childImages = btn.GetComponentsInChildren<Image>(true);
                foreach (var img in childImages)
                {
                    if (img.gameObject.name.ToLower().Contains("lock"))
                        img.gameObject.SetActive(true);
                }
            }

            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => LoadLevelFromButton(btn));
        }
    }

    public void LoadLevelFromButton(Button btn)
    {
        string label = "";
        TMP_Text tmpText = btn.GetComponentInChildren<TMP_Text>();
        if (tmpText != null)
            label = tmpText.text.Trim();
        else
        {
            Text classicText = btn.GetComponentInChildren<Text>();
            if (classicText != null)
                label = classicText.text.Trim();
        }

        int levelNum = 1;
        if (int.TryParse(label, out levelNum))
        {
            int unlockedLevel = PlayerPrefs.GetInt("LevelUnlocked", 1);
            if (levelNum <= unlockedLevel)
                SceneManager.LoadScene("Level" + levelNum); // veya Build index ile: SceneManager.LoadScene(levelNum);
        }
    }
}
