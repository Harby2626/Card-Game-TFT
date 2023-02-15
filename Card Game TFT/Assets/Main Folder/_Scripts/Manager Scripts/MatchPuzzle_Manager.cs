using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchPuzzle_Manager : MonoBehaviour
{


    [SerializeField] AllPuzzleCards_SO AllPuzzleCards;
    public List<GameObject> AvailableCardSlots = new List<GameObject>();
    public List<GameObject> CardsOnPuzzle = new List<GameObject>();
    [SerializeField] int MoveAmount;
    GameObject cardSpawnPos;

    // This is a list for checking current match checking
    List<PuzzleCard> ChosenCards = new List<PuzzleCard>();

    enum PuzzlePhase
    {
        Card_Dealing,
        Playing
    }
    PuzzlePhase puzzlePhase;

    private void Start()
    {
        cardSpawnPos = GameObject.Find("CardSpawnPoint");
        GetAvailableSlots();

        puzzlePhase = PuzzlePhase.Card_Dealing;
        MoveAmount = 8;
    }

    public void ChoseCard()// This function choses a card when selected, and plays turn animation adds chosen cards to List
    {
        if (MoveAmount > 0)
        {
            GameObject thisCardGO = EventSystem.current.currentSelectedGameObject; // Chosen card gameObject
            PuzzleCard selectedCard = thisCardGO.GetComponent<PuzzleCard>(); // Chosen card --PuzzleCard-- script

            if (ChosenCards.Count < 2) // If player doesn't choose two cards
            {
                ChosenCards.Add(selectedCard);
                thisCardGO.GetComponent<Animator>().SetBool("chosen", true);
            }

            if (ChosenCards.Count == 2) // If player selected two card
            {
                MoveAmount--;
                if (ChosenCards[0].Card.cardType == ChosenCards[1].Card.cardType)// When cards matches
                {
                    Debug.Log("MATCH!");

                }

                else// Cards doesn't match
                {
                    Debug.Log("TRY AGAIN");
                }
            }
        }
        
    }

    private void Update()
    {


        switch (puzzlePhase)
        {
            case PuzzlePhase.Card_Dealing:
                // Deal the cards from dealing pos to card slots randomly
                SpawnCards();
                puzzlePhase = PuzzlePhase.Playing;
                break;
            case PuzzlePhase.Playing:
                //Playing phase (time runs out 15s (?))
                break;
            default:
                break;
        }
    }

    void GetAvailableSlots()// This function takes available slots to a list
    {
        GameObject[] temp_slots = GameObject.FindGameObjectsWithTag("PuzzleCardSlot");
        foreach (GameObject item in temp_slots)
        {
            AvailableCardSlots.Add(item);
        }
    }
    void SpawnCards()// This function spawns a set of cards to out of borders. || To be lerped to their positions ||
    {

        for (int i = 0; i < AvailableCardSlots.Count/2; i++)
        {
            int RandVal = Random.Range(0, AllPuzzleCards.AllPuzzleCards.Count);
            
            if (!CardsOnPuzzle.Contains(AllPuzzleCards.AllPuzzleCards[RandVal]))
            {
                for (int j = 0; j < 2; j++)
                {
                    var newCard = Instantiate(AllPuzzleCards.AllPuzzleCards[RandVal], cardSpawnPos.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);
                    newCard.transform.SetParent(GameObject.Find("Cards").transform, false);
                    CardsOnPuzzle.Add(newCard);
                }
            }
        }

    }
}
