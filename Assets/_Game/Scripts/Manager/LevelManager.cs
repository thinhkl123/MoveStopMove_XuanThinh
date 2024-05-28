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

    private int curIdxLevel = 1;

    private void Start()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {

        int initalCharAmount = levelSOList[curIdxLevel-1].initalAmount;

        for (int i = 0; i < initalCharAmount; i++)
        {
            EnemySpawner.Ins.SpawnEnemy();
        }

        OnLoadLevel?.Invoke(this, EventArgs.Empty);
    }
}
