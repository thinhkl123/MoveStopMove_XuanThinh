using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button skinBtn;
    [SerializeField] private Button weaponBtn;
    [SerializeField] private Button playBtn;

    [SerializeField] private List<LevelStageUI> levelStageUIList;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform levelContainer;

    private void Start()
    {
        LevelStageUI.OnChoseLevelChange += LevelStageUI_OnChoseLevelChange;

        settingBtn.onClick.AddListener(() =>
        {
            SoundManager.Ins.PlayClickBtnSound();
            UIManager.Ins.OpenUI<SettingUI>();
        });

        playBtn.onClick.AddListener(() =>
        {
            Close(0);
            SoundManager.Ins.PlayClickBtnSound();
            LevelManager.Ins.LoadLevel();
        });

        weaponBtn.onClick.AddListener(() =>
        {
            Close(0);
            SoundManager.Ins.PlayClickBtnSound();
            UIManager.Ins.OpenUI<WeaponShopUI>();
            UIManager.Ins.GetUI<WeaponShopUI>().UpdateVisual();
        });

        skinBtn.onClick.AddListener(() =>
        {
            Close(0);
            SoundManager.Ins.PlayClickBtnSound();
            UIManager.Ins.OpenUI<SkinShopUI>();
            UIManager.Ins.GetUI<SkinShopUI>().UpdateVisual();
            GameManager.Ins.ChangeToCanvasCamera();
        });
    }

    private void OnDisable()
    {
        LevelStageUI.OnChoseLevelChange -= LevelStageUI_OnChoseLevelChange;
    }

    private void LevelStageUI_OnChoseLevelChange(object sender, System.EventArgs e)
    {
        levelStageUIList[LevelManager.Ins.curIdxLevel-1].UnFocus();
    }

    public void UpdateVisual()
    {
        UpdateCoin();
        UpdateStage();
    }

    private void UpdateCoin()
    {
        coinText.text = DataManager.Ins.GetCoin().ToString();
    }

    private void UpdateStage()
    {
        int maxLevel = LevelManager.Ins.curMaxLevel;

        for (int i = 0; i < maxLevel-1; i++)
        {
            levelStageUIList[i].UnLockStage();
            levelStageUIList[i].UnFocus();
        }

        levelStageUIList[maxLevel-1].UnLockStage();
        levelStageUIList[maxLevel-1].SetFocus();

        SnapTo(levelStageUIList[maxLevel-1].GetComponentInParent<RectTransform>());

        for (int i = maxLevel; i <  levelStageUIList.Count; i++)
        {
            levelStageUIList[i].LockStage();
            levelStageUIList[i].UnFocus();
        }
    }

    public void SnapTo(RectTransform target)
    {
        Canvas.ForceUpdateCanvases();

        levelContainer.anchoredPosition =
                (Vector2)scrollRect.transform.InverseTransformPoint(levelContainer.position)
                - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
    }
}
