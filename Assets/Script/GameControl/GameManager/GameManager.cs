using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using ObserverPattern;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private int NumberOfLevelsAvailable;
    [SerializeField] private int CoinDefaultAmount;
    [SerializeField] private int MaxEnergy;
    [SerializeField] private int currentEnergy;

    public PlayerDataManager PlayerDataManager { get; private set; }
    public LevelManager LevelManager { get; private set; }


    //====Singleton================
    private async void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            PlayerDataManager = new PlayerDataManager();
            LevelManager = new LevelManager();

        }
        else
        {
            Destroy(gameObject);
            return;
        }


        //Khởi tạo PlayerData và LevelData
        await PlayerDataManager.InitializeAsync(CoinDefaultAmount, MaxEnergy);

        DateTime ntpTime = await TimeManager.TryGetNetworkTimeUntilSuccess();

        Debug.Log($"Current time: {ntpTime}");
       
        await Task.Delay(100); // Đợi một chút để đảm bảo dữ liệu đã được tải
    }

    void Update()
    {
        currentEnergy = PlayerDataManager.PlayerData.Energy;
    }


    //====Xử lý trạng thái game=========================
    public void GameWin()
    {
        //tính sao
        int star = LevelManager.CalculateStar();
        int starAmountDifference = LevelManager.UpdateLevelStatus(star);

        //trừ đi năng lượng
        PlayerDataManager.SpendEnergy(1);

        //hiển thị ra ui 
        UIController.Instance.ShowLevelCompletePanel(star, starAmountDifference * 10);
        UIController.Instance.MinusEnergy();

        //mở khóa level tiếp theo
        LevelManager.UnlockNextLevel(NumberOfLevelsAvailable);

        //cập nhật dữ liệu vào file
        LevelManager.Save();
        PlayerDataManager.AddCoin(starAmountDifference * 10);

    }

    public void GameOver()
    {
        UIController.Instance.ShowGameOverPanel();
    }


}

public struct LevelID
{
    public int Stage;
    public int Index;
} 
