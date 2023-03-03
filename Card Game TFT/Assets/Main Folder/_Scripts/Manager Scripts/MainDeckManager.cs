using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public class MainDeckManager : MonoBehaviour
{
    public static MainDeckManager instance;


    [SerializeField] List<GameObject> AllHeroCards = new List<GameObject>();
    [SerializeField] GameObject DeckContainer;
    List<GameObject> MainHeroCardDeck = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (instance.MainHeroCardDeck.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                instance.MainHeroCardDeck.Add(AllHeroCards[i]);
            }
            instance.SpawnHeroCards();
        }
    }

    public void SpawnHeroCards()
    {
        for (int i = 0; i < instance.MainHeroCardDeck.Count; i++)
        {
            Instantiate(instance.MainHeroCardDeck[i], instance.DeckContainer.transform);
        }
    }


    public List<GameObject> GetHeroCardDeck()
    {
        return instance.MainHeroCardDeck;
    }

    public void AddHeroCard(GameObject hero_card)
    {
        //bool contains = false;
        //foreach (GameObject item in MainHeroCardDeck)
        //{
        //    if (item.GetComponent<>)
        //    {

        //    }
        //}
        instance.MainHeroCardDeck.Add(hero_card);
    }

    public void RemoveHeroCard(GameObject hero_card)
    {
        instance.MainHeroCardDeck.Remove(hero_card);
    }
}
