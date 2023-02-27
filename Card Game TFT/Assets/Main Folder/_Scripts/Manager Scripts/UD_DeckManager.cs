using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class UD_DeckManager : MonoBehaviour
{
    private static UD_DeckManager instance;
    public static UD_DeckManager Instance
    {
        get => instance == null
            ? (instance = FindObjectOfType<UD_DeckManager>())
            : instance;
    }



    List<GameObject> UpgradeCard_Deck = new List<GameObject>();
    [SerializeField] List<GameObject> AllUpgradeCardPrefabs = new List<GameObject>();

    [SerializeField] GameObject UD_Grid_Layout;

    public void AddUpgradeCard(PuzzleCard_SO type)// This function adds specific card to UpgradeCard_Deck list by type
    {
        GameObject card = GetCardFromType(type);
        UpgradeCard_Deck.Add(card);
        Instantiate(card, UD_Grid_Layout.transform);
        
    }

    public void RemoveUpgradeCard(GameObject card)// This function removes a card from UpgradeCard_Deck list
    {
        UpgradeCard_Deck.Remove(card);
    }

    public GameObject GetCardFromType(PuzzleCard_SO card_type)// This function returns a card by given type 
    {
        foreach (GameObject item in AllUpgradeCardPrefabs)
        {
            if (item.GetComponent<UDCardEffect>().GetUpgradeType() == card_type)
            {
                return item;
            }
        }
        return null;

    }
}
