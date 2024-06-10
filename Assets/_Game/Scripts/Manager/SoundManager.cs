using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource bgMusic;
    [SerializeField] private AudioSource clickBtnSound;
    [SerializeField] private AudioSource throwWeaponSound;
    [SerializeField] private AudioSource collideWeaponSound;
    [SerializeField] private AudioSource countDownSound;
    [SerializeField] private AudioSource winSound;
    [SerializeField] private AudioSource loseSound;

    public void ChangeVolumeBGMusic(float value)
    {
        bgMusic.volume = value;
    }    

    public void ChangeVolumeVFXSound(float value)
    {
        throwWeaponSound.volume = value;
        collideWeaponSound.volume = value;
        countDownSound.volume = value;
        winSound.volume = value;
        loseSound.volume = value;
    }

    public void PlayBGMusic()
    {
        bgMusic.Play();
    }

    public void StopBGMsuic()
    {
        bgMusic.Stop();
    }

    public void PlayClickBtnSound()
    {
        clickBtnSound.Play();
    }

    public void PlayThrowWeaponSound()
    {
        throwWeaponSound.Play();
    }

    public void PlayCollideWeaponSound()
    {
        collideWeaponSound.Play();
    }

    public void PlayCountDownSound()
    {
        countDownSound.Play();
    }

    public void PlayWinSound()
    {
        winSound.Play();
    }

    public void StopWinSound()
    {
        winSound.Stop();
    }

    public void PlayLoseSound()
    {
        loseSound.Play();
    }

    public void StopLoseSound()
    {
        loseSound.Stop();
    }
}
