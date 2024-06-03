using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SetFull")]
public class SetFullSO : ScriptableObject
{
    public int id;
    public Sprite icon;
    public int price;
    public int rangeBuf;
    public int speedBuf;
    public int goldBuf;
}
