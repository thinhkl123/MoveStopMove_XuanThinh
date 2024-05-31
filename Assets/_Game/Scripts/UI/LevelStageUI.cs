using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStageUI : MonoBehaviour
{
    [SerializeField] private Sprite unLockImage;
    [SerializeField] private Sprite lockImage;
    [SerializeField] private Image stageImage;
    [SerializeField] private GameObject focus;

    public void UnLockStage()
    {
        stageImage.sprite = unLockImage;
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
}
