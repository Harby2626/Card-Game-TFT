using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DealingCardManager : MonoBehaviour
{
    public int counter = 0;
    float timer = 0f, card_deal_wait = .25f;

    MatchPuzzle_Manager puzzleMng;
    private void Start()
    {
        puzzleMng = GameObject.Find("Match Puzzle Manager").GetComponent<MatchPuzzle_Manager>();
    }

    IEnumerator DealCards()
    {
        yield return new WaitForSeconds(1.1f);
        if (puzzleMng.AvailableCardSlots.Count > 0)
        {

            timer += Time.deltaTime;
            if (timer > card_deal_wait)
            {
                int randVal = Random.Range(0, puzzleMng.AvailableCardSlots.Count);
                DealCard(counter, randVal);
                counter++;
                if (counter == puzzleMng.CardsOnPuzzle.Count)
                {
                    puzzleMng.puzzlePhase = MatchPuzzle_Manager.PuzzlePhase.Playing;
                    puzzleMng.ActivateCardButtons();
                }
                timer = 0f;
            }
        }
    }

    private void Update()
    {
        StartCoroutine(DealCards());
        //if (puzzleMng.AvailableCardSlots.Count > 0)
        //{

        //    timer += Time.deltaTime;
        //    if (timer > card_deal_wait)
        //    {
        //        int randVal = Random.Range(0, puzzleMng.AvailableCardSlots.Count);
        //        DealCard(counter, randVal);
        //        counter++;
        //        if (counter == puzzleMng.CardsOnPuzzle.Count)
        //        {
        //            puzzleMng.puzzlePhase = MatchPuzzle_Manager.PuzzlePhase.Playing;
        //            puzzleMng.ActivateCardButtons();
        //        }
        //        timer = 0f;
        //    }
        //}

    }


    void DealCard(int index, int slot_index)
    {
        puzzleMng.CardsOnPuzzle[index].GetComponent<PuzzleCard>().CardSlotTarget = puzzleMng.AvailableCardSlots[slot_index].GetComponent<RectTransform>().anchoredPosition;
        puzzleMng.CardsOnPuzzle[index].GetComponent<PuzzleCard>().setToTarget = true;
        puzzleMng.AvailableCardSlots.Remove(puzzleMng.AvailableCardSlots[slot_index]);
    }


}
