using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button claimBtn;

    private void Start()
    {
        claimBtn.onClick.AddListener(() =>
        {
            Close(0);
            GameManager.Ins.state = GameManager.GameState.WaitToStart;
            UIManager.Ins.OpenUI<HomeUI>();
        });
    }

    public void ChangeRewardText(int value)
    {
        rewardText.text = value.ToString();
    }
}
