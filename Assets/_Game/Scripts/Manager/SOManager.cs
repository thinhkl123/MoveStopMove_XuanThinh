using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOManager : Singleton<SOManager>
{
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

    [SerializeField] private List<WeaponSO> weaponSOList;

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
}
