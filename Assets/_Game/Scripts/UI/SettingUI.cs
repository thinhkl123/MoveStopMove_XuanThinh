using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUI : UICanvas
{
    [SerializeField] private Button closeBtn;
    [SerializeField] private Slider soundVFXVolume;
    [SerializeField] private Slider musicVolume;

    private void Start()
    {
        soundVFXVolume.value = 1;
        musicVolume.value = 1;

        closeBtn.onClick.AddListener(() =>
        {
            Close(0);
            SoundManager.Ins.PlayClickBtnSound();
        });

        soundVFXVolume.onValueChanged.AddListener(delegate
        {
            SoundManager.Ins.ChangeVolumeBGMusic(soundVFXVolume.value);
        });

        musicVolume.onValueChanged.AddListener(delegate
        {
            SoundManager.Ins.ChangeVolumeVFXSound(musicVolume.value);
        });
    }
}
