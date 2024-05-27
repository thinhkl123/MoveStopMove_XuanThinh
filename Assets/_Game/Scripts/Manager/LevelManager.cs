using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private List<LevelSO> levelSOList;

    private int curIdxLevel = 1;

    private void Start()
    {
        OnLoadLevel();
    }

    private void OnLoadLevel()
    {
        int initalCharAmount = levelSOList[curIdxLevel-1].initalAmount;

        for (int i = 0; i < initalCharAmount; i++)
        {
            EnemySpawner.Ins.SpawnEnemy();
        }
    }
}
