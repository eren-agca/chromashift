using UnityEngine;
using UnityEditor;

public class ClearPlayerPrefsTool
{
    // Unity menüsüne yeni bir seçenek ekler: "Tools/Clear Player-Prefs"
    [MenuItem("Tools/Clear Player-Prefs")]
    public static void DeleteAllPlayerPrefs()
    {
        // PlayerPrefs'teki tüm kayıtları siler.
        PlayerPrefs.DeleteAll();
        Debug.Log("SUCCESS: All PlayerPrefs data has been cleared!");
    }
}