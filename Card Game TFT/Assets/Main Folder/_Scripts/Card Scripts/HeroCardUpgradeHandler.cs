using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCardUpgradeHandler : MonoBehaviour
{
    List<PuzzleCard_SO> ActiveUpgrades = new List<PuzzleCard_SO>();

    public void AddUpgrade(PuzzleCard_SO upgrade)
    {
        ActiveUpgrades.Add(upgrade);
    }

    public void RemoveUpgrade(PuzzleCard_SO upgrade)
    {
        ActiveUpgrades.Remove(upgrade);
    }
}
