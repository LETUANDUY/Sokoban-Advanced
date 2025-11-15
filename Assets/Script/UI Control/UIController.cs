
using UnityEngine;


public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }
   

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public async void MinusEnergy()
    {
       Debug.Log("Minus Energy UIController");
    }

    //===========================================
    public void ShowGameOverPanel()
    {
        Debug.Log("Show Game Over Panel");
    }


    //====== Xử lý hiển thị win panel========
    public void ShowLevelCompletePanel(int stars, int rewardAmount)
    {
        Debug.Log($"Show Level Complete Panel with {stars} stars and reward {rewardAmount} coins");
    }


    public void OnClickBackToLevelSelect()
    {
        Debug.Log("Back to Level Select");
    }

    public void SetMoveCountUI(int moveCount, int moveCountLimit, int[] moveToGetStar)
    {
        Debug.Log($"Set Move Count UI: {moveCount}/{moveCountLimit}");
    }

    public void ShowInPlayLevelUI()
    {
        Debug.Log("Show In-Play Level UI");
    }

    public void ShowConnecting()
    {
        Debug.Log("Showing Connecting UI...");
    }

    public void ShowDisconnect()
    {
        Debug.Log("Showing Disconnect UI...");
    }

    public void HideConnectPanel()
    {
        Debug.Log("Hiding Connect Panel UI...");
    }
}
