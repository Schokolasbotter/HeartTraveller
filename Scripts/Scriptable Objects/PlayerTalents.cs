using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Talents", menuName = "Talents")]
public class PlayerTalents : ScriptableObject
{
    public int Offensive;
    public int Defensive;
    public int Mobility;
}
