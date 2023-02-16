using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MatchPuzzle_Manager : MonoBehaviour
{
    //float timer = 0f, closeWaitTime = 1f;


    [SerializeField] AllPuzzleCards_SO AllPuzzleCards;
    public List<GameObject> AvailableCardSlots = new List<GameObject>();
    public List<GameObject> CardsOnPuzzle = new List<GameObject>();
    [SerializeField] int MoveAmount;
    GameObject cardSpawnPos;

    // This is a list for checking current match checking
    List<GameObject> ChosenCards = new List<GameObject>();

    public enum PuzzlePhase
    {
        Card_Dealing, Halt, Playing
    }
    public PuzzlePhase puzzlePhase;

    private void Start()
    {
        cardSpawnPos = GameObject.Find("CardSpawnPoint");
        GetAvailableSlots();

        puzzlePhase = PuzzlePhase.Card_Dealing;
        MoveAmount = 8;
    }

    IEnumerator HandleMatch()
    {
        yield return new WaitForSeconds(1f);
        if(ChosenCards.Count == 2)
        {
            MoveAmount--;
            if (ChosenCards[0].GetComponent<PuzzleCard>().Card.cardType == ChosenCards[1].GetComponent<PuzzleCard>().Card.cardType)// When cards matches
            {
                DeactivateCardButtons();
                ChosenCards.Clear();
                ActivateCardButtons();
                Debug.Log("MATCH!");

            }

            else// Cards doesn't match
            {
                DeactivateCardButtons();
                CloseCard(ChosenCards[0]); CloseCard(ChosenCards[1]);
                ChosenCards.Clear();
                ActivateCardButtons();
                Debug.Log("TRY AGAIN");
            }
        }
    }


    public void ChoseCard()// This function choses a card when selected, and plays turn animation adds chosen cards to List
    {
        if (MoveAmount > 0)
        {
            GameObject thisCardGO = EventSystem.current.currentSelectedGameObject; // Chosen card gameObject

            if (ChosenCards.Count < 2) // If player doesn't choose two cards
            {
                ChosenCards.Add(thisCardGO);
                thisCardGO.GetComponent<PuzzleCard>().state = PuzzleCard.State.Opening;
                thisCardGO.GetComponent<Animator>().SetBool("chosen", true);
            }

            StartCoroutine(HandleMatch());

        }
        
    }

    private void Update()
    {

        switch (puzzlePhase)
        {
            case PuzzlePhase.Card_Dealing:
                SpawnCards();
                puzzlePhase = PuzzlePhase.Halt;
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
    void HandleMatching()
    {

    }


    public void ActivateCardButtons()
    {
        foreach (GameObject item in CardsOnPuzzle)
        {
            item.GetComponent<Button>().enabled = true;
        }
    }

    public void DeactivateCardButtons()
    {
        foreach (GameObject item in CardsOnPuzzle)
        {
            item.GetComponent<Button>().enabled = false;
        }
    }

    void CloseCard(GameObject card)
    {
        card.GetComponent<Animator>().SetBool("chosen", false);
        card.GetComponent<Animator>().SetBool("wrong", true);
    }

}
