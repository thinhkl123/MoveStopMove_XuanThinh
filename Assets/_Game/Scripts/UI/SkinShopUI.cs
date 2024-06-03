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
    }

    public void UpdateVisual()
    {
        ChangeCoin();
        ChangeSkinType(SkinType.Hair);
        ShowSkin(skinType);
    }

    private void ChangeCoin()
    {
        coinText.text = DataManager.Ins.GetCoin().ToString();
    }

    private void ChangeSkinType(SkinType newType)
    {
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
                if (hairId < 0)
                {
                    hairId = SOManager.Ins.GetHairSOCount();
                }
                ShowHair();
                break;
            case SkinType.Pant:
                pantId--;
                if (pantId < 0)
                {
                    pantId = SOManager.Ins.GetPantSOCount();
                }
                ShowPant();
                break;
            case SkinType.Shield:
                shieldId--;
                if (shieldId < 0)
                {
                    shieldId = SOManager.Ins.GetShieldSOCount();
                }
                ShowShield();
                break;
            case SkinType.SetFull:
                setFullId--;
                if (setFullId < 0)
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

    private void ShowPant()
    {
        PantSO pantSO = SOManager.Ins.GetPantSO(pantId-1);
        skinIcon.sprite = pantSO.icon;
        buffText.text = "Attack Speed";
        valueBuffText.text = "+" + pantSO.speedBuf.ToString() + "%";
        priceText.text = pantSO.price.ToString();

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

    private void ShowShield()
    {
        ShieldSO shieldSO = SOManager.Ins.GetShielSO(shieldId - 1);
        skinIcon.sprite = shieldSO.icon;
        buffText.text = "Gold";
        valueBuffText.text = "+" + shieldSO.goldBuf.ToString() + "%";
        priceText.text = shieldSO.price.ToString();

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
