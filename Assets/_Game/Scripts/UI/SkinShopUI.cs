using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinShopUI : UICanvas
{
    public enum SkinType
    {
        Hair,
        Pant,
        Shield,
        SetFull,
    }

    private SkinType skinType;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Button homeBtn;

    [SerializeField] private Image skinIcon;
    [SerializeField] private TextMeshProUGUI buffText;
    [SerializeField] private TextMeshProUGUI valueBuffText;
    [SerializeField] private TextMeshProUGUI priceText;

    [SerializeField] private Button hairBtn;
    [SerializeField] private Button pantBtn;
    [SerializeField] private Button shieldBtn;
    [SerializeField] private Button setFullBtn;

    [SerializeField] private GameObject hairFocus;
    [SerializeField] private GameObject pantFocus;
    [SerializeField] private GameObject shieldFocus;
    [SerializeField] private GameObject setFullFocus;

    [SerializeField] private Button nextBtn;
    [SerializeField] private Button prevBtn;

    [SerializeField] private Button buyBtn;
    [SerializeField] private Button selectBtn;
    [SerializeField] private Image selectImg;
    [SerializeField] private Sprite selectBG;
    [SerializeField] private Sprite hasSelectBG;

    private bool isBought = true;
    private bool isSelected = true;

    private int hairId = 1;
    private int pantId = 1;
    private int shieldId = 1;
    private int setFullId = 1;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        hairId = DataManager.Ins.GetCurrentHairId() != 0 ? DataManager.Ins.GetCurrentHairId() : 1;
        pantId = DataManager.Ins.GetCurrentPantId() != 0 ? DataManager.Ins.GetCurrentPantId() : 1;
        shieldId = DataManager.Ins.GetCurrentShieldId() != 0 ? DataManager.Ins.GetCurrentShieldId() : 1;
    }

    private void Start()
    {
        homeBtn.onClick.AddListener(() =>
        {
            Close(0);
            UIManager.Ins.OpenUI<HomeUI>();
            UIManager.Ins.GetUI<HomeUI>().UpdateVisual();
            GameManager.Ins.ChangeToMainCamera();
            Player.Instance.ChangeCurrentSkin();
        });

        nextBtn.onClick.AddListener(() =>
        {
            NextSkinId(skinType);
        });

        prevBtn.onClick.AddListener(() =>
        {
            PrevSkinId(skinType);
        });

        hairBtn.onClick.AddListener(() =>
        {
            ChangeSkinType(SkinType.Hair);
        });

        pantBtn.onClick.AddListener(() =>
        {
            ChangeSkinType(SkinType.Pant);
        });

        shieldBtn.onClick.AddListener(() =>
        {
            ChangeSkinType(SkinType.Shield);
        });

        setFullBtn.onClick.AddListener(() =>
        {
            ChangeSkinType(SkinType.SetFull);
        });
        
        //Debug.Log(hairId + " " + pantId + " " + shieldId);
    }

    public void UpdateVisual()
    {
        ChangeCoin();
        ChangeSkinType(SkinType.Hair);
        //ShowSkin(skinType);
    }

    private void ChangeCoin()
    {
        coinText.text = DataManager.Ins.GetCoin().ToString();
    }

    private void ChangeSkinType(SkinType newType)
    {
        Player.Instance.ChangeCurrentSkin();

        hairFocus.SetActive(false); 
        pantFocus.SetActive(false);
        shieldFocus.SetActive(false);
        setFullFocus.SetActive(false);

        skinType = newType;

        switch (newType)
        {
            case SkinType.Hair:
                hairFocus.SetActive(true);
                ShowHair();
                break;
            case SkinType.Pant:
                pantFocus.SetActive(true);
                ShowPant();
                break;
            case SkinType.Shield:
                shieldFocus.SetActive(true);
                ShowShield();
                break;
            case SkinType.SetFull:
                setFullFocus.SetActive(true);
                ShowSetFull();
                break;
        }
    }

    private void NextSkinId(SkinType type)
    {
        switch (type)
        {
            case SkinType.Hair:
                hairId++;
                if (hairId > SOManager.Ins.GetHairSOCount())
                {
                    hairId = 1;
                }
                ShowHair();
                break;
            case SkinType.Pant:
                pantId++;
                if (pantId > SOManager.Ins.GetPantSOCount())
                {
                    pantId = 1;
                }
                ShowPant();
                break;
            case SkinType.Shield:
                shieldId++;
                if (shieldId > SOManager.Ins.GetShieldSOCount())
                {
                    shieldId = 1;
                }
                ShowShield();
                break;
            case SkinType.SetFull:
                setFullId++;
                if (setFullId > SOManager.Ins.GetSetFullSOCount())
                {
                    setFullId = 1;
                }
                ShowSetFull();
                break;
        }
    }

    private void PrevSkinId(SkinType type)
    {
        switch (type)
        {
            case SkinType.Hair:
                hairId--;
                if (hairId <= 0)
                {
                    hairId = SOManager.Ins.GetHairSOCount();
                }
                ShowHair();
                break;
            case SkinType.Pant:
                pantId--;
                if (pantId <= 0)
                {
                    pantId = SOManager.Ins.GetPantSOCount();
                }
                ShowPant();
                break;
            case SkinType.Shield:
                shieldId--;
                if (shieldId <= 0)
                {
                    shieldId = SOManager.Ins.GetShieldSOCount();
                }
                ShowShield();
                break;
            case SkinType.SetFull:
                setFullId--;
                if (setFullId <= 0)
                {
                    setFullId = SOManager.Ins.GetSetFullSOCount();
                }
                ShowSetFull();
                break;
        }
    }

    public void ShowSkin(SkinType type)
    {
        switch (type)
        {
            case SkinType.Hair:
                ShowHair(); 
                break;
            case SkinType.Pant:
                ShowPant();
                break;
            case SkinType.Shield:
                ShowShield();
                break;
            case SkinType.SetFull:
                ShowSetFull();
                break;
        }
    }

    private void ShowHair()
    {
        HairSO hairSO = SOManager.Ins.GetHairSO(hairId-1);
        skinIcon.sprite = hairSO.icon;
        buffText.text = "Range";
        valueBuffText.text = "+" + hairSO.rangeBuf.ToString() + "%";
        priceText.text = hairSO.price.ToString();

        Player.Instance.ChangeHair(hairId);

        BuyFunction(DataManager.Ins.GetValueHair(hairId));

        if (!isBought)
        {
            buyBtn.gameObject.SetActive(true);
            selectBtn.gameObject.SetActive(false);

            buyBtn.onClick.RemoveAllListeners();
            buyBtn.onClick.AddListener(() =>
            {
                if (DataManager.Ins.GetCoin() >= hairSO.price)
                {
                    DataManager.Ins.UpdateCoin(-hairSO.price);
                    DataManager.Ins.UpdateHairIds(hairId, 2);

                    ChangeCoin();

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
                    DataManager.Ins.UpdateHairIds(hairId, 2);
                });
            }
        }
    }

    private void ShowPant()
    {
        PantSO pantSO = SOManager.Ins.GetPantSO(pantId-1);
        skinIcon.sprite = pantSO.icon;
        buffText.text = "Attack Speed";
        valueBuffText.text = "+" + pantSO.speedBuf.ToString() + "%";
        priceText.text = pantSO.price.ToString();

        Player.Instance.ChangePant(pantId);

        BuyFunction(DataManager.Ins.GetValuePant(pantId));

        if (!isBought)
        {
            buyBtn.gameObject.SetActive(true);
            selectBtn.gameObject.SetActive(false);

            buyBtn.onClick.RemoveAllListeners();
            buyBtn.onClick.AddListener(() =>
            {
                if (DataManager.Ins.GetCoin() >= pantSO.price)
                {
                    DataManager.Ins.UpdateCoin(-pantSO.price);
                    DataManager.Ins.UpdatePantIds(pantId, 2);

                    ChangeCoin();

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
                    DataManager.Ins.UpdatePantIds(pantId, 2);
                });
            }
        }
    }

    private void ShowShield()
    {
        ShieldSO shieldSO = SOManager.Ins.GetShielSO(shieldId - 1);
        skinIcon.sprite = shieldSO.icon;
        buffText.text = "Gold";
        valueBuffText.text = "+" + shieldSO.goldBuf.ToString() + "%";
        priceText.text = shieldSO.price.ToString();

        Player.Instance.ChangeShield(shieldId);

        BuyFunction(DataManager.Ins.GetValueShield(shieldId));

        if (!isBought)
        {
            buyBtn.gameObject.SetActive(true);
            selectBtn.gameObject.SetActive(false);

            buyBtn.onClick.RemoveAllListeners();
            buyBtn.onClick.AddListener(() =>
            {
                if (DataManager.Ins.GetCoin() >= shieldSO.price)
                {
                    DataManager.Ins.UpdateCoin(-shieldSO.price);
                    DataManager.Ins.UpdateShieldIds(shieldId, 2);

                    ChangeCoin();

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
                    DataManager.Ins.UpdateShieldIds(shieldId, 2);
                });
            }
        }
    }

    private void ShowSetFull()
    {
        SetFullSO setFullSO = SOManager.Ins.GetSetFullSO(setFullId-1);
        skinIcon.sprite = setFullSO.icon;
        if (setFullSO.speedBuf != 0)
        {
            buffText.text = "Attack Speed";
            valueBuffText.text = "+" + setFullSO.speedBuf.ToString() + "%";
        }
        else if (setFullSO.rangeBuf != 0) 
        {
            buffText.text = "Range";
            valueBuffText.text = "+" + setFullSO.rangeBuf.ToString() + "%";
        }
        else
        {
            buffText.text = "Gold";
            valueBuffText.text = "+" + setFullSO.goldBuf.ToString() + "%";
        }
        priceText.text = setFullSO.price.ToString();

        isBought = false;

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
