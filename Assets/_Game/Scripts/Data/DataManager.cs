using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private string folderPath;
    private string filePath;

    private GameData gameData;

    private void Awake()
    {
        // Set the folder path to the "Data" folder within the project
        folderPath = Path.Combine(Application.dataPath, "_Game", "Resources", "Data");
        // Ensure the folder exists
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        // Set the file path to a specific file in the "Data" folder
        filePath = Path.Combine(folderPath, "gameData.json");

        gameData = LoadData();
    }

    private void Start()
    {
        /*
        gameData = new GameData
        {
            coin = 0,
            level = 1,
            weaponIds = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0 },
            hairIds = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            pantIds = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            shieldIds = new int[] { 0, 0 },
            setFullIds = new int[] { 0, 0, 0, 0, 0}
        };

        SaveData(gameData);
        */
    }

    public void SaveData(GameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(filePath, json);
        Debug.Log("File Saved");
    }

    public GameData LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        else 
        {
            gameData = new GameData
            {
                coin = 0,
                level = 1,
                weaponIds = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                hairIds = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                pantIds = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                shieldIds = new int[] { 0, 0 },
                setFullIds = new int[] { 0, 0, 0, 0, 0 }
            };

            SaveData(gameData);

            return gameData; 
        }
    }

    private void OnApplicationQuit()
    {
        SaveData(gameData);
    }

    public int GetCoin()
    {
        return gameData.coin;
    }

    public void UpdateCoin(int value)
    {
        gameData.coin += value;
    }

    public int GetLevel()
    {
        return gameData.level;
    }

    public void UpdateLevel(int value)
    {
        gameData.level = value;
    }

    public int[] GetWeaponIds()
    {
        return gameData.weaponIds;
    }

    public void UpdateWeaponIds(int[] value)
    {
        gameData.weaponIds = value;
    }

    public int[] GetHairIds()
    {
        return gameData.hairIds;
    }

    public void UpdateHairIds(int[] value)
    {
        gameData.hairIds = value;
    }

    public int[] GetShieldIds()
    {
        return gameData.shieldIds;
    }

    public void UpdateShieldIds(int[] value)
    {
        gameData.shieldIds = value;
    }

    public int[] GetSetFullIds()
    {
        return gameData.setFullIds;
    }

    public void UpdateSetFullIds(int[] value)
    {
        gameData.setFullIds = value;
    }
}
