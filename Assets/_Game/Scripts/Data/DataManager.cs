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

    //Coin
    public int GetCoin()
    {
        return gameData.coin;
    }

    public void UpdateCoin(int value)
    {
        gameData.coin += value;
    }

    //Level
    public int GetLevel()
    {
        return gameData.level;
    }

    public void UpdateLevel(int value)
    {
        gameData.level = value;
    }

    //Weapon
    public int[] GetWeaponIds()
    {
        return gameData.weaponIds;
    }

    public int GetValueWeapon(int id)
    {
        id--;
        return gameData.hairIds[id];
    }

    public int GetCurrentWeaponId()
    {
        int idx = -1;
        for (int i = 0; i < gameData.weaponIds.Length; i++)
        {
            if (gameData.weaponIds[i] == 2)
            {
                idx = i;
                break;
            }
        }
        return idx + 1;
    }

    public void UpdateWeaponIds(int id, int value)
    {
        if (value == 2)
        {
            if (GetCurrentWeaponId() != 0)
            {
                gameData.hairIds[GetCurrentWeaponId() - 1] = 1;
            }
            gameData.weaponIds[id - 1] = value;
        }
        else
        {
            gameData.weaponIds[id - 1] = value;
        }
    }

    //Hair
    public int[] GetHairIds()
    {
        return gameData.hairIds;
    }

    public int GetValueHair(int id)
    {
        id--;
        return gameData.hairIds[id];
    }

    public int GetCurrentHairId()
    {
        int idx = -1;
        for (int i = 0; i < gameData.hairIds.Length; i++)
        {
            if (gameData.hairIds[i] == 2)
            {
                idx = i;
                break;
            }
        }
        return idx+1;
    }

    public void UpdateHairIds(int id, int value)
    {
        if (value == 2)
        {
            if (GetCurrentHairId() != 0)
            {
                gameData.hairIds[GetCurrentHairId() -1] = 1;
            }
            gameData.hairIds[id-1] = value;
        }
        else
        {
            gameData.hairIds[id-1] = value;
        }
    }

    //Pant
    public int[] GetPantIds()
    {
        return gameData.pantIds;
    }

    public int GetValuePant(int id)
    {
        id--;
        return gameData.pantIds[id];
    }

    public int GetCurrentPantId()
    {
        int idx = -1;
        for (int i = 0; i < gameData.pantIds.Length; i++)
        {
            if (gameData.pantIds[i] == 2)
            {
                idx = i;
                break;
            }
        }
        return idx + 1;
    }

    public void UpdatePantIds(int id, int value)
    {
        if (value == 2)
        {
            if (GetCurrentPantId() != 0)
            {
                gameData.pantIds[GetCurrentPantId() - 1] = 1;
            }
            gameData.pantIds[id-1] = value;
        }
        else
        {
            gameData.pantIds[id-1] = value;
        }
    }

    //Shield
    public int[] GetShieldIds()
    {
        return gameData.shieldIds;
    }

    public int GetValueShield(int id)
    {
        id--;
        return gameData.shieldIds[id];
    }

    public int GetCurrentShieldId()
    {
        int idx = -1;
        for (int i = 0; i < gameData.shieldIds.Length; i++)
        {
            if (gameData.shieldIds[i] == 2)
            {
                idx = i;
                break;
            }
        }
        return idx + 1;
    }

    public void UpdateShieldIds(int id, int value)
    {
        if (value == 2)
        {
            if (GetCurrentShieldId() != 0)
            {
                gameData.shieldIds[GetCurrentShieldId() - 1] = 1;
            }
            gameData.shieldIds[id-1] = value;
        }
        else
        {
            gameData.shieldIds[id-1] = value;
        }
    }

    //Set Full
    public int[] GetSetFullIds()
    {
        return gameData.setFullIds;
    }

    public void UpdateSetFullIds(int[] value)
    {
        gameData.setFullIds = value;
    }
}
