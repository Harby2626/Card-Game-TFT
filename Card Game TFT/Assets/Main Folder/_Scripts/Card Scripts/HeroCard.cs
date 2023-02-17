using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCard : MonoBehaviour
{
    List<PuzzleCard_SO> activeUpgrades;
    List<PuzzleCard_SO> activeDowngrades;





    public void AddUpgradeToHero(PuzzleCard_SO upgrade)
    {
        activeUpgrades.Add(upgrade);
    }
    
    public void RemoveUpgradeFromHero(PuzzleCard_SO upgrade)
    {
        activeUpgrades.Remove(upgrade);
    }

    public void AddDowngradeToHero(PuzzleCard_SO downgrade)
    {
        activeDowngrades.Add(downgrade);
    }

    public void RemoveDowngradeFromHero(PuzzleCard_SO downgrade)
    {
        activeDowngrades.Remove(downgrade);
    }
}
