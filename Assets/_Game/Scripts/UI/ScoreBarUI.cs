using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarUI : MonoBehaviour
{
    //[SerializeField] private Character character;
    [SerializeField] private Image colorBar;
    [SerializeField] private TextMeshProUGUI scoreText;

    public void UpdateBar(int value)
    {
        scoreText.text = value.ToString();
    }

    public void ChangeColorBar(Color color)
    {
        colorBar.color = color;
    }
}
