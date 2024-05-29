using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI textAlive;
    [SerializeField] private Button settingBtn;

    private void Start()
    {
        GameManager.Ins.OnUpdateAlivechar += GameManager_OnUpdateAlivechar;

        settingBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.OpenUI<PauseUI>();
        });
    }

    private void OnDestroy()
    {
        GameManager.Ins.OnUpdateAlivechar -= GameManager_OnUpdateAlivechar;
    }

    private void GameManager_OnUpdateAlivechar(object sender, System.EventArgs e)
    {
        UpdateTextAlive();
    }

    private void UpdateTextAlive()
    {
        textAlive.text = "Alive:  " + GameManager.Ins.AliveChar.ToString();
    }
}
