using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LostUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private Button claimBtn;

    private void Start()
    {
        claimBtn.onClick.AddListener(() =>
        {
            Close(0);
            GameManager.Ins.state = GameManager.GameState.WaitToStart;
            UIManager.Ins.OpenUI<HomeUI>();
            UIManager.Ins.GetUI<HomeUI>().UpdateVisual();
        });
    }

    public void ChangeRankText(int value)
    {
        rankText.text = "#" + value.ToString();
    }

    public void ChangeRewardText(int value)
    {
        rewardText.text = value.ToString();
        DataManager.Ins.UpdateCoin(value);
    }
}
