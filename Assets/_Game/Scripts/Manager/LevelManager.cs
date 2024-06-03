using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public event EventHandler OnLoadLevel;

    [SerializeField] private List<LevelSO> levelSOList;

    public int LevelCharNum
    {
        get
        {
            return levelSOList[curIdxLevel-1].charAmount;
        }
    }

    public int curMaxLevel;
    public int curIdxLevel = 1;

    private void Awake()
    {
        curMaxLevel = DataManager.Ins.GetLevel();
        curIdxLevel = curMaxLevel;
    }

    private void Start()
    {
        //LoadLevel();

        
    }

    public void LoadLevel()
    {
        int initalCharAmount = levelSOList[curIdxLevel-1].initalAmount;

        OnLoadLevel?.Invoke(this, EventArgs.Empty);

        for (int i = 0; i < initalCharAmount; i++)
        {
            EnemySpawner.Ins.SpawnEnemy();
        }
    }

    public void UpdateLevel()
    {
        if (curIdxLevel > curMaxLevel)
        {
            curMaxLevel = curIdxLevel;
            DataManager.Ins.UpdateLevel(curMaxLevel);
        }
    }
}
