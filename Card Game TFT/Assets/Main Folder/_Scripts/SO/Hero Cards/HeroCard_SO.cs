using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hero Card")]
public class HeroCard_SO : ScriptableObject
{
    public int attackDamage;
    public int attackSpeed;
    public int moveSpeed;
    public int hitpoint;
    public int regen;
}
