using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event EventHandler OnUpdateAlivechar;
    public event EventHandler OnStateChange;

    public enum GameState
    {
        WaitToStart,
        CountDownToStart,
        Playing,
        Pause,
        GameOver,
    }

    public GameState state;

    private float countDownToStartTime = 3f;

    private void Awake()
    {
        state = GameState.WaitToStart;
    }

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

        UIManager.Ins.OpenUI<HomeUI>();
        UIManager.Ins.GetUI<HomeUI>().UpdateVisual();
    }

    private void LevelManager_OnLoadLevel(object sender, EventArgs e)
    {
        OnInit();
        OnUpdateAlivechar?.Invoke(this, EventArgs.Empty);
        state = GameState.CountDownToStart;
        UIManager.Ins.OpenUI<CountDownUI>();
    }

    private void OnInit()
    {
        aliveChar = LevelManager.Ins.LevelCharNum;
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.WaitToStart:
                break;
            case GameState.CountDownToStart:
                countDownToStartTime -= Time.deltaTime;
                if (countDownToStartTime <= 0f)
                {
                    state = GameState.Playing;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                    UIManager.Ins.CloseUI<CountDownUI>();
                    countDownToStartTime = 3f;
                }
                break;
            case GameState.Playing:
                break;
            case GameState.Pause:
                break;
            case GameState.GameOver:
                break;
        }
    }

    public void UpdateAliveChar()
    {
        aliveChar--;
        EnemySpawner.Ins.SpawnEnemy();
        OnUpdateAlivechar?.Invoke(this, EventArgs.Empty);
    }

    public float GetCountDownTime()
    {
        return countDownToStartTime;
    }
}
