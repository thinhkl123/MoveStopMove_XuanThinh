using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelStageUI : MonoBehaviour
{
    public static event EventHandler OnChoseLevelChange;

    [SerializeField] private Sprite unLockImage;
    [SerializeField] private Sprite lockImage;
    [SerializeField] private Image stageImage;
    [SerializeField] private GameObject focus;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private Button button;


    public void UnLockStage()
    {
        stageImage.sprite = unLockImage;
        button.onClick.AddListener(() =>
        {
            if (GetLevelIdx() == LevelManager.Ins.curIdxLevel)
            {
                return;
            }
            OnChoseLevelChange?.Invoke(this, EventArgs.Empty);
            LevelManager.Ins.curIdxLevel = GetLevelIdx();
            SetFocus();
        });
    }

    public void LockStage()
    {
        stageImage.sprite = lockImage;
    }

    public void SetFocus()
    {
        focus.SetActive(true);
    }

    public void UnFocus()
    {
        focus.SetActive(false);
    }

    public int GetLevelIdx()
    {
        return int.Parse(level.text);
    }
}
