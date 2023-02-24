using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UDCardEffect : MonoBehaviour
{
    [SerializeField] PuzzleCard_SO cardType;

    public void AffectHeroCard(GameObject heroCard)
    {
        GameObject upgrade_holder = heroCard.transform.GetChild(0).gameObject;
        int upgrade_cap = upgrade_holder.transform.childCount;

    }

    public PuzzleCard_SO GetUpgradeType()
    {
        return cardType;
    }
}
