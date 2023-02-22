using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character")]
public class Character_SO : ScriptableObject
{
    public string characterName;
    public float health = 100f;
    public float movementSpeed = 1f;
    public float attackRange = 1f;
    public float attackDamage = 1f;
    public float attackCooldown = 1f;
}
