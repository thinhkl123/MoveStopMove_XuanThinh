using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hair")]
public class HairSO : ScriptableObject
{
    public int Id;
    public float rangeBuf;
    public int price;
    public GameObject prefab;
    public Sprite icon;
}
