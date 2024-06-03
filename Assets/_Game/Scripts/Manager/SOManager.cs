using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOManager : Singleton<SOManager>
{
    //Color
    [SerializeField] private List<ColorSO> colorSOList;

    public ColorSO GetColorSO(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, colorSOList.Count);
            return colorSOList[idx];
        }
        else
        {
            return colorSOList[idx];
        }
    }

    //Weapon
    [SerializeField] private List<WeaponSO> weaponSOList;

    public int GetWeaponSOCount()
    {
        return weaponSOList.Count;
    }

    public WeaponSO GetWeaponSO(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, weaponSOList.Count);
            return weaponSOList[idx];
        }
        else
        {
            return weaponSOList[idx];
        }
    }

    public Weapon GetWeapon(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, weaponSOList.Count);
            return weaponSOList[idx].weapon;
        }
        else
        {
            return weaponSOList[idx].weapon;
        }
    }

    public GameObject GetWeaponOnHand(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, weaponSOList.Count);
            return weaponSOList[idx].weaponOnHand;
        }
        else
        {
            return weaponSOList[idx].weaponOnHand;
        }
    }

    //Hair
    [SerializeField] private List<HairSO> hairSOList;

    public int GetHairSOCount()
    {
        return hairSOList.Count;
    }

    public HairSO GetHairSO(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, hairSOList.Count);
            return hairSOList[idx];
        }
        else
        {
            return hairSOList[idx];
        }
    }

    public GameObject GetHairPrefab(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, hairSOList.Count);
            return hairSOList[idx].prefab;
        }
        else
        {
            return hairSOList[idx].prefab;
        }
    }

    //Pant
    [SerializeField] private List<PantSO> pantSOList; 

    public int GetPantSOCount()
    {
        return pantSOList.Count;
    }

    public PantSO GetPantSO(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, pantSOList.Count);
            return pantSOList[idx];
        }
        else
        {
            return pantSOList[idx];
        }
    }

    public Material GetPantMaterial(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, pantSOList.Count);
            return pantSOList[idx].material;
        }
        else
        {
            return pantSOList[idx].material;
        }
    }

    //Shield
    [SerializeField] private List<ShieldSO> shieldSOList;

    public int GetShieldSOCount()
    {
        return shieldSOList.Count;
    }

    public ShieldSO GetShielSO(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, shieldSOList.Count);
            return shieldSOList[idx];
        }
        else
        {
            return shieldSOList[idx];
        }
    }

    public GameObject GetShieldPrefab(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, shieldSOList.Count);
            return shieldSOList[idx].prefab;
        }
        else
        {
            return shieldSOList[idx].prefab;
        }
    }

    //Set Full
    [SerializeField] private List<SetFullSO> setFullSOList;

    public int GetSetFullSOCount()
    {
        return setFullSOList.Count;
    }

    public SetFullSO GetSetFullSO(int idx = -1)
    {
        if (idx == -1)
        {
            idx = Random.Range(0, setFullSOList.Count);
            return setFullSOList[idx];
        }
        else
        {
            return setFullSOList[idx];
        }
    }
}
