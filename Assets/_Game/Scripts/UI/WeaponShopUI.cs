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

    private int weaponId = 1;

    private void OnEnable()
    {
        weaponId = DataManager.Ins.GetCurrentWeaponId();
    }

    private void Start()
    {
        homeBtn.onClick.AddListener(() =>
        {
            Close(0);
            UIManager.Ins.OpenUI<HomeUI>();
            UIManager.Ins.GetUI<HomeUI>().UpdateVisual();
            Player.Instance.ChangeCurrentSkin();
        });

        nextBtn.onClick.AddListener(() =>
        {
            weaponId++;
            if (weaponId > SOManager.Ins.GetWeaponSOCount())
            {
                weaponId = 1;
            }
            ShowWeapon();
        });

        prevBtn.onClick.AddListener(() => 
        {
            weaponId--;
            if (weaponId < 0)
            {
                weaponId = SOManager.Ins.GetWeaponSOCount();
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
        WeaponSO weaponSO = SOManager.Ins.GetWeaponSO(weaponId-1);
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

        BuyFunction(DataManager.Ins.GetValueWeapon(weaponId));

        if (!isBought)
        {
            buyBtn.gameObject.SetActive(true);
            selectBtn.gameObject.SetActive(false);

            buyBtn.onClick.RemoveAllListeners();
            buyBtn.onClick.AddListener(() =>
            {
                if (DataManager.Ins.GetCoin() >= weaponSO.price)
                {
                    DataManager.Ins.UpdateCoin(-weaponSO.price);
                    DataManager.Ins.UpdateWeaponIds(weaponId, 2);

                    Player.Instance.ChangeWeapon(weaponId);

                    UpdateCoin();

                    //Show selected
                    buyBtn.gameObject.SetActive(false);
                    selectBtn.gameObject.SetActive(true);
                    selectImg.sprite = hasSelectBG;
                }
            });
        }
        else
        {
            buyBtn.gameObject.SetActive(false);
            selectBtn.gameObject.SetActive(true);

            if (isSelected)
            {
                selectImg.sprite = hasSelectBG;
                selectBtn.onClick.RemoveAllListeners();
            }
            else
            {
                selectImg.sprite = selectBG;
                selectBtn.onClick.RemoveAllListeners();
                selectBtn.onClick.AddListener(() =>
                {
                    selectImg.sprite = hasSelectBG;
                    DataManager.Ins.UpdateWeaponIds(weaponId, 2);
                });
            }
        }
    }

    private void BuyFunction(int value)
    {
        if (value == 0)
        {
            isBought = false;
        }
        else
        {
            isBought = true;
            if (value == 1)
            {
                isSelected = false;
            }
            else
            {
                isSelected = true;
            }
        }
    }
}
