using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event EventHandler OnUpdateAlivechar;

    public int AliveChar
    {
        get
        {
            return aliveChar;
        }
    }
    
    private int aliveChar;

    private void Start()
    {
        LevelManager.Ins.OnLoadLevel += LevelManager_OnLoadLevel;
    }

    private void LevelManager_OnLoadLevel(object sender, EventArgs e)
    {
        OnInit();
        OnUpdateAlivechar?.Invoke(this, EventArgs.Empty);
    }

    private void OnInit()
    {
        aliveChar = LevelManager.Ins.LevelCharNum;
        Debug.Log(aliveChar);
    }

    public void UpdateAliveChar()
    {
        aliveChar--;
        EnemySpawner.Ins.SpawnEnemy();
        OnUpdateAlivechar?.Invoke(this, EventArgs.Empty);
    }
}
