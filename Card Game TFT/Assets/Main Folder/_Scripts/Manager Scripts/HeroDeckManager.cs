using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeckManager : MonoBehaviour
{
    [SerializeField] AllPuzzleCards_SO cards_database;

    [SerializeField] GameObject InGameDeckCont;
    [SerializeField] List<GameObject> AllHeroCards = new List<GameObject>();

    bool hero_cards_spawned = true;

    private void Start()
    {
        //SpawnHeroDeckCards();
    }


    private void Update()
    {
        if (hero_cards_spawned)
        {
            SpawnHeroDeckCards();
        }
    }

    public void SpawnHeroDeckCards()
    {

        foreach (string _tag in cards_database.DeckCardTags)
        {
            switch (_tag)
            {
                case "elf":
                    Instantiate(AllHeroCards[0], InGameDeckCont.transform);
                    break;

                case "raider":
                    Instantiate(AllHeroCards[1], InGameDeckCont.transform);
                    break;

                case "wizard":
                    Instantiate(AllHeroCards[2], InGameDeckCont.transform);
                    break;

                default:
                    break;
            }
        }
        hero_cards_spawned = false;

    }
}
