using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Color")]
public class ColorSO: ScriptableObject
{
    public Material material;
    public Color color;
}
