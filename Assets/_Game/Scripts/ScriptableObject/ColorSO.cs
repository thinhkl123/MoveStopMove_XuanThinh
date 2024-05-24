using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSO : MonoBehaviour
{
    public enum ColorType
    {
        Aqua = 0,
        Blue = 1,
        Gray = 2,
        Green = 3,
        Silver = 4,
        Yellow = 5,
    }

    [CreateAssetMenu(menuName = "ColorData")]
    public class ColorData : ScriptableObject
    {
        [SerializeField] Material[] materials;

        public Material GetMaterial(ColorType colorType)
        {
            return materials[(int)colorType];
        }
    }
}
