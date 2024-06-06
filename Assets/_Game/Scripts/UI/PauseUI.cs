using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : UICanvas
{
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button HomeBtn;

    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        continueBtn.onClick.AddListener(() =>
        {
            Close(0);
            SoundManager.Ins.PlayClickBtnSound();
            GameManager.Ins.state = GameManager.GameState.Playing;
            UIManager.Ins.OpenUI<GamePlayUI>();
        });

        settingBtn.onClick.AddListener(() =>
        {
            SoundManager.Ins.PlayClickBtnSound();
            UIManager.Ins.OpenUI<SettingUI>();
        });

        HomeBtn.onClick.AddListener(() =>
        {
            Close(0);
            SoundManager.Ins.PlayClickBtnSound();
            GameManager.Ins.state = GameManager.GameState.WaitToStart;
            UIManager.Ins.OpenUI<HomeUI>();
        });
    }

    public void ChangeTextLevel()
    {
        levelText.text = "Level" + LevelManager.Ins.curIdxLevel.ToString();
    }
}
