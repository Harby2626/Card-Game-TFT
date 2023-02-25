using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCardUpgradeHandler : MonoBehaviour
{
    public int upgradeCount;

    List<PuzzleCard_SO> ActiveUpgrades = new List<PuzzleCard_SO>();
    


    public void AddUpgrade(PuzzleCard_SO upgrade)
    {
        if (ActiveUpgrades.Count < upgradeCount)
        {
            ActiveUpgrades.Add(upgrade);
            foreach (Transform child in transform.GetChild(0))
            {
                if (child.GetComponent<Image>().IsActive())
                {
                    continue;
                }
                else
                {
                    child.GetComponent<Image>().enabled = true;
                    break;
                }
            }
        }
    }

    public void RemoveUpgrade(PuzzleCard_SO upgrade)
    {
        ActiveUpgrades.Remove(upgrade);
    }

    public int GetActiveUpgradeCount()
    {
        return ActiveUpgrades.Count;
    }
}
