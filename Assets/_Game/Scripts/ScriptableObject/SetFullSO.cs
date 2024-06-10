using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SetFull")]
public class SetFullSO : ScriptableObject
{
    public int id;
    public string setFullName;
    public Sprite icon;
    public int price;
    public Material materialBody;
    public GameObject wing;
    public GameObject tail;
    public GameObject specialHair;
}
