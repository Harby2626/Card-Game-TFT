using System.Collections;
using System.Collections.Generic;
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

    public void AddUpgradeCard(GameObject card)
    {
        UpgradeCard_Deck.Add(card);
        
    }

    public void RemoveUpgradeCard(GameObject card)
    {
        UpgradeCard_Deck.Remove(card);
    }
}
