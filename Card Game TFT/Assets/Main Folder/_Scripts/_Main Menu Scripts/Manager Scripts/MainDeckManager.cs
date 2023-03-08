using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainDeckManager : MonoBehaviour
{
    [SerializeField] AllPuzzleCards_SO cards_database; 

    public static MainDeckManager instance;

    [SerializeField] GameObject InventoryDeckContainer;
    [SerializeField] TextMeshProUGUI cannot_fight_text;

    [SerializeField] List<GameObject> InventoryCards = new List<GameObject>();
    [SerializeField] List<GameObject> MainDeckCards = new List<GameObject>();
    [SerializeField] List<GameObject> AllHeroCards = new List<GameObject>();

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
        if (instance.InventoryCards.Count == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                instance.InventoryCards.Add(AllHeroCards[i]);
            }
            instance.SpawnInventoryHeroCards();
        }

        if (MainDeckCards.Count != 0)
        {

        }
    }

    public void SpawnInventoryHeroCards()
    {
        for (int i = 0; i < instance.InventoryCards.Count; i++)
        {
            Instantiate(instance.InventoryCards[i], instance.InventoryDeckContainer.transform);
        }
    }

    public int GetInventoryCount()
    {
        return instance.InventoryCards.Count;
    }

    public int GetDeckCount()
    {
        return instance.MainDeckCards.Count;
    }

    public List<GameObject> GetInventoryCards()
    {
        return instance.InventoryCards;
    }

    public List<GameObject> GetDeckCards()
    {
        return instance.MainDeckCards;
    }

    public void AddHeroCard_Inv(GameObject hero_card)
    {
        instance.InventoryCards.Add(hero_card);
    }

    public void AddHeroCard_Deck(GameObject hero_card)
    {
        instance.MainDeckCards.Add(hero_card);
    }

    public void RemoveHeroCard_Inv(GameObject hero_card)
    {
        instance.InventoryCards.Remove(hero_card);
    }

    public void RemoveHeroCard_Deck(GameObject hero_card)
    {
        instance.MainDeckCards.Remove(hero_card);
    }

    #region Button Functions
    public void StartFightButton()
    {
        if (cards_database.GetDeckCount() == 0)
        {
            for (int i = 0; i < MainDeckCards.Count; i++)
            {
                string _tag = MainDeckCards[i].GetComponent<MainMenuHeroCard>().type_tag;
                cards_database.AddCardToList(_tag);
            }
        }
        else
        {
            foreach (GameObject item in MainDeckCards)
            {
                string _tag = item.GetComponent<MainMenuHeroCard>().type_tag;
                if (!cards_database.DeckCardTags.Contains(_tag))
                {
                    cards_database.AddCardToList(_tag);
                }
            }
        }

        if (MainDeckCards.Count == 0)
        {
            Instantiate(cannot_fight_text, GameObject.Find("Menu Canvas").transform);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
    #endregion
}
