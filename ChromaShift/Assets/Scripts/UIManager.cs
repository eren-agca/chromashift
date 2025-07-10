using UnityEngine;
using UnityEngine.UI;   
public class UIManager : MonoBehaviour
{
    public PlayerController playerController;

    public void OnRedButtonClicked()
    {
        playerController.SetRedColor();
    }

    public void OnBlueButtonClicked()
    {
        playerController.SetBlueColor();
    }
    
}