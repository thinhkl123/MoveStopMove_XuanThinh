using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pant")]
public class PantSO : ScriptableObject
{
    public int Id;
    public float speedBuf;
    public int price;
    public Sprite icon;
    public Material material;
}
