using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event EventHandler OnUpdateAlivechar;
    public event EventHandler OnStateChange;
    public event EventHandler OnWin;

    public enum GameState
    {
        WaitToStart,
        CountDownToStart,
        Playing,
        Pause,
        GameOver,
    }

    public GameState state;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera canvasCamera;

    private float countDownToStartTime = 3f;
    private int reward;
    private float goldBuff;

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
        Player.OnLose += Player_OnLose;
        Player.OnGetReward += Player_OnGetReward;

        //UIManager.Ins.OpenUI<GamePlayUI>();

        UIManager.Ins.OpenUI<HomeUI>();
        UIManager.Ins.GetUI<HomeUI>().UpdateVisual();

        UIManager.Ins.OpenUI<PauseUI>();
        UIManager.Ins.CloseUI<PauseUI>();

        UIManager.Ins.OpenUI<SettingUI>();
        UIManager.Ins.CloseUI<SettingUI>();

        ChangeToMainCamera();

    }

    private void Player_OnGetReward(object sender, Player.OnGetRewardEventArgs e)
    {
        this.reward = e.reward;
        this.goldBuff = e.buff;
    }

    private void Player_OnLose(object sender, EventArgs e)
    {
        state = GameState.GameOver;
        Invoke(nameof(Lose), 1.5f);
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
                    SoundManager.Ins.PlayBGMusic();
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
        OnUpdateAlivechar?.Invoke(this, EventArgs.Empty);
        if (aliveChar == 0)
        {
            state = GameState.GameOver;
            OnWin?.Invoke(this, EventArgs.Empty);
            LevelManager.Ins.curIdxLevel++;
            LevelManager.Ins.UpdateLevel();
            Invoke(nameof(Win), 1.5f);
        }

        if (aliveChar > 0)
        {
            EnemySpawner.Ins.SpawnEnemy();
        }
    }

    private void Win()
    {
        SoundManager.Ins.StopBGMsuic();
        SoundManager.Ins.PlayWinSound();
        UIManager.Ins.OpenUI<WinUI>();
        reward += (int) (reward * (goldBuff / 100));
        UIManager.Ins.GetUI<WinUI>().ChangeRewardText(reward);
    }

    private void Lose()
    {
        SoundManager.Ins.StopBGMsuic();
        SoundManager.Ins.PlayLoseSound();
        UIManager.Ins.OpenUI<LostUI>();
        UIManager.Ins.GetUI<LostUI>().ChangeRewardText(reward);
        UIManager.Ins.GetUI<LostUI>().ChangeRankText(aliveChar);
    }

    public float GetCountDownTime()
    {
        return countDownToStartTime;
    }

    public void ChangeToMainCamera()
    {
        mainCamera.gameObject.SetActive(true);
        canvasCamera.gameObject.SetActive(false);
    }

    public void ChangeToCanvasCamera()
    {
        Player.Instance.OnInit();
        mainCamera.gameObject.SetActive(false);
        canvasCamera.gameObject.SetActive(true);
    }
}
