using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MatchPuzzle_Manager : MonoBehaviour
{
    public bool puzzle_ended = false, time_ended;

    Animator puzzle_animator;

    [SerializeField] AllPuzzleCards_SO AllPuzzleCards;
    [SerializeField] UD_DeckManager ud_manager;
    public List<GameObject> AvailableCardSlots = new List<GameObject>();
    public List<GameObject> CardsOnPuzzle = new List<GameObject>();
    public int MoveAmount;
    GameObject cardSpawnPos; [SerializeField] GameObject FightButton;
    [SerializeField] GameObject UD_menu;

    // This is a list for checking current match checking
    List<GameObject> ChosenCards = new List<GameObject>();

    public enum PuzzlePhase
    {
        Start, Halt, Playing, Ended
    }
    public PuzzlePhase puzzlePhase;

    private void Start()
    {
        puzzle_animator = GameObject.Find("Puzzle Background").GetComponent<Animator>();


        cardSpawnPos = GameObject.Find("CardSpawnPoint");
        GetAvailableSlots();

        puzzlePhase = PuzzlePhase.Start;
        MoveAmount = 8;
    }

    IEnumerator HandleMatch()
    {
        yield return new WaitForSeconds(1.5f);
        if(ChosenCards.Count == 2)
        {
            MoveAmount--;
            if (ChosenCards[0].GetComponent<PuzzleCard>().Card.cardType == ChosenCards[1].GetComponent<PuzzleCard>().Card.cardType)// When cards matches
            {
                DeactivateCardButtons();
                CardsOnPuzzle.Remove(ChosenCards[0]); CardsOnPuzzle.Remove(ChosenCards[1]);


                PuzzleCard_SO puzzle_card_type = ChosenCards[0].GetComponent<PuzzleCard>().Card;
                ud_manager.AddUpgradeCard(puzzle_card_type);


                MoveTrueCard(ChosenCards[0]); MoveTrueCard(ChosenCards[1]);
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
    IEnumerator StartFightingPhase()
    {
        yield return new WaitForSeconds(2f);
        puzzlePhase = PuzzlePhase.Ended;
        FightButton.SetActive(true);
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

    public void OpenUD_Menu()
    {

        if (UD_menu.GetComponent<Animator>().GetBool("open") == false)
        {
            UD_menu.GetComponent<Animator>().SetBool("open", true);
        }
        else if (UD_menu.GetComponent<Animator>().GetBool("open") == true)
        {
            UD_menu.GetComponent<Animator>().SetBool("open", false);
            UD_menu.GetComponent<Animator>().SetBool("close", true);
        }
    }

    private void Update()
    {
        if (puzzlePhase == PuzzlePhase.Playing)
        {
            if ((MoveAmount == 0 && !puzzle_ended) || time_ended || CardsOnPuzzle.Count == 0)// When puzzle phase ended
            {
                puzzle_ended = true;
                puzzle_animator.SetBool("puzzle_end", true);
                GameObject.Find("UD Card Button").GetComponent<Button>().interactable = true;
                StartCoroutine(StartFightingPhase());
            }
        }
        


        switch (puzzlePhase)
        {
            case PuzzlePhase.Start:
                SpawnCards();
                puzzlePhase = PuzzlePhase.Halt;
                break;
            case PuzzlePhase.Halt:

                break;
            case PuzzlePhase.Playing:
                //Playing phase (time runs out 15s (?))
                break;
            case PuzzlePhase.Ended:

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
    void SpawnCards()// This function spawns a set of cards to specific position || To be lerped to their positions ||
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

    public void ActivateCardButtons()// This function activates Button components of Cards
    {
        foreach (GameObject item in CardsOnPuzzle)
        {
            item.GetComponent<Button>().enabled = true;
        }
    }

    public void DeactivateCardButtons()// This function deactivates Button components of Cards
    {
        foreach (GameObject item in CardsOnPuzzle)
        {
            item.GetComponent<Button>().enabled = false;
        }
    }

    void CloseCard(GameObject card)// This function makes card play closing animation
    {
        card.GetComponent<Animator>().SetBool("chosen", false);
        card.GetComponent<Animator>().SetBool("wrong", true);
    }

    void MoveTrueCard(GameObject card)// This function makes matched cards to be lerped trough specific position
    {
        card.GetComponent<PuzzleCard>().CardTrueMatchTarget = GameObject.Find("TrueCardPos").GetComponent<RectTransform>().anchoredPosition;
        card.GetComponent<PuzzleCard>().matchTargetSet = true;
        card.GetComponent<Animator>().SetBool("correct", true);
        card.GetComponent<PuzzleCard>().setToTarget = false;
    }
}
