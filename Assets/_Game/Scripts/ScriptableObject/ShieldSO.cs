using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shield")]
public class ShieldSO : ScriptableObject
{
    public int Id;
    public float goldBuf;
    public int price;
    public GameObject prefab;
    public Sprite icon;
}
