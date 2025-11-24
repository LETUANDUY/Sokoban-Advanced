
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerDataManager
{
    private PlayerData playerData = new();
    public PlayerData PlayerData => playerData;


    private int maxEnergy;


    public async Task InitializeAsync(int coinDefaultAmount, int MaxEnergy)
    {

        await Load(coinDefaultAmount, MaxEnergy);
        maxEnergy = MaxEnergy;
    }

    //====Xử lý dữ liệu xu=========================
    public void AddCoin(int amount)
    {
        if (amount <= 0) return;
        playerData.Coin += amount;
        SaveSystem.SavePlayerData(playerData);
    }

    public bool CheckCoinEnough(int amount)
    {
        return amount <= playerData.Coin;
    }

    public bool SpendCoin(int amount)
    {
        if (amount <= 0 || playerData.Coin < amount) return false;
        playerData.Coin -= amount;
        SaveSystem.SavePlayerData(playerData);
        return true;
    }


    //====Xử lý dữ liệu năng lượng=========================
    public void AddEnergy(int amount)
    {
        if (amount <= 0) return;
        playerData.Energy += amount;
        SaveSystem.SavePlayerData(playerData);
    }

    public bool SpendEnergy(int amount)
    {
        playerData.Energy -= amount;
        SaveSystem.SavePlayerData(playerData);
        return true;
    }

    public void SaveEnergyTimer(DateTime lastTime, float time)
    {
        playerData.LastQuitTime = lastTime;
        playerData.EnergyTimer = time;
        SaveSystem.SavePlayerData(playerData);
    }
    
    //====Xử lý dữ liệu cài đặt=========================
    public void SaveSettingsData(float musicVolume, float sfxVolume)
    {
        playerData.MusicVolume = musicVolume;
        playerData.SFXVolume = sfxVolume;

        SaveSystem.SavePlayerData(playerData);
    }

    

    public void Save()
    {
        SaveSystem.SavePlayerData(playerData);
    }


    private async Task Load(int coinDefaultAmount, int MaxEnergy)
    {
        // Load player data from file
        playerData = await SaveSystem.LoadPlayerData();

        if (playerData == null)
        {
            playerData = new PlayerData
            {
                Coin = coinDefaultAmount,
                Energy = MaxEnergy,
                SkinOwned = new List<int> { 0 },
                SkinEquipped = 0,
                MusicVolume = 1f,
                SFXVolume = 1f,
                LastQuitTime = DateTime.Now,
                EnergyTimer = 0f,

                DailyTaskID = new List<int>(),
                DailyTaskProgress = new List<int>(),
                DailyClaimed = 0

            };
        }
    }
    
   
}