using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShopUI : UICanvas
{
    [SerializeField] private Button homeBtn;
    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private Image weaponIcon;
    [SerializeField] private TextMeshProUGUI buffText;
    [SerializeField] private TextMeshProUGUI valueBuffText;
    [SerializeField] private TextMeshProUGUI priceText;

    [SerializeField] private Button nextBtn;
    [SerializeField] private Button prevBtn;

    [SerializeField] private Button buyBtn;
    [SerializeField] private Button selectBtn;
    [SerializeField] private Image selectImg;
    [SerializeField] private Sprite selectBG;
    [SerializeField] private Sprite hasSelectBG;

    private bool isBought = true;
    private bool isSelected = true;

    private int initId = 1;

    private void Start()
    {
        homeBtn.onClick.AddListener(() =>
        {
            Close(0);
            UIManager.Ins.OpenUI<HomeUI>();
            UIManager.Ins.GetUI<HomeUI>().UpdateVisual();
        });

        nextBtn.onClick.AddListener(() =>
        {
            initId++;
            if (initId > SOManager.Ins.GetWeaponSOCount())
            {
                initId = 1;
            }
            ShowWeapon();
        });

        prevBtn.onClick.AddListener(() => 
        {
            initId--;
            if (initId < 0)
            {
                initId = SOManager.Ins.GetWeaponSOCount();
            }
            ShowWeapon();
        });
    }

    public void UpdateVisual()
    {
        UpdateCoin();
        ShowWeapon();
    }

    public void UpdateCoin()
    {
        coinText.text = DataManager.Ins.GetCoin().ToString();
    }

    public void ShowWeapon()
    {
        WeaponSO weaponSO = SOManager.Ins.GetWeaponSO(initId-1);
        weaponName.text = weaponSO.weaponName;
        weaponIcon.sprite = weaponSO.icon;
        if (weaponSO.speedBuf != 0)
        {
            buffText.text = "Attack Speed";
            valueBuffText.text = "+" + weaponSO.speedBuf.ToString() + "%";
        }
        else
        {
            buffText.text = "Range";
            valueBuffText.text = "+" + weaponSO.rangeBuf.ToString() + "%";
        }
        priceText.text = weaponSO.price.ToString();

        if (!isBought)
        {
            buyBtn.gameObject.SetActive(true);
            selectBtn.gameObject.SetActive(false);
        }
        else
        {
            buyBtn.gameObject.SetActive(false);
            selectBtn.gameObject.SetActive(true);

            if (isSelected)
            {
                selectImg.sprite = hasSelectBG;
            }
            else
            {
                selectImg.sprite = selectBG;
            }
        }
    }
}
