using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainDeckManager : MonoBehaviour
{
    [SerializeField] List<GameObject> AllHeroCards = new List<GameObject>();
    [SerializeField] GameObject DeckContainer;
    List<GameObject> MainHeroCardDeck = new List<GameObject>();

    private void Start()
    {
        if (MainHeroCardDeck.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                MainHeroCardDeck.Add(AllHeroCards[i]);
            }
            SpawnHeroCards();
        }
    }

    public void SpawnHeroCards()
    {
        for (int i = 0; i < MainHeroCardDeck.Count; i++)
        {
            Instantiate(MainHeroCardDeck[i], DeckContainer.transform);
        }
    }


    public void AddHeroCard(GameObject hero_card)
    {
        MainHeroCardDeck.Add(hero_card);
    }

    public void RemoveHeroCard(GameObject hero_card)
    {
        MainHeroCardDeck.Remove(hero_card);
    }
}
