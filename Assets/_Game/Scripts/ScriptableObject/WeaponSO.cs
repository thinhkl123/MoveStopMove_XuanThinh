using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public int id;
    public string weaponName;
    public int speedBuf;
    public int rangeBuf;
    public int price;
    public Weapon weapon;
    public GameObject weaponOnHand;
    public Sprite icon;
}
