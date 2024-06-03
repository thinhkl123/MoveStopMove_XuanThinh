using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UICanvas
{
    [SerializeField] private Button closeBtn;

    private void Start()
    {
        closeBtn.onClick.AddListener(() =>
        {
            Close(0);
        });
    }
}
