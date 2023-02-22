using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleCard : MonoBehaviour
{
    RectTransform rectTransform;
    MatchPuzzle_Manager puzzle_manager;
    public PuzzleCard_SO Card;

    public Vector2 CardSlotTarget, CardTrueMatchTarget;
    public bool setToTarget = false, matchTargetSet = false;
    private void Start()
    {
        state = State.Closed;

        rectTransform = GetComponent<RectTransform>();
        puzzle_manager = GameObject.Find("Match Puzzle Manager").GetComponent<MatchPuzzle_Manager>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(puzzle_manager.ChoseCard);


    }

    public enum State
    {
        Closing, Closed, Opening, Opened, Matched, notMatched
    }
    public State state;


    private void Update()
    {
        if (setToTarget)// if setToTarget boolean turns true, this card lerps to its target slot
        {
            if (CardSlotTarget.magnitude - rectTransform.anchoredPosition.magnitude != 0f)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, CardSlotTarget, 4f * Time.deltaTime);
            }
            else if (CardSlotTarget.magnitude - rectTransform.anchoredPosition.magnitude == 0f)
            {
                setToTarget = false;
            }
        }

        if (matchTargetSet)// if matchTargetSet boolean turns true, this card lerps to trueMatchPoint
        {
            if (Vector2.Distance(CardTrueMatchTarget, rectTransform.anchoredPosition) > 50)
            {
                rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, CardTrueMatchTarget, 3f * Time.deltaTime);
            }
            else if (Vector2.Distance(CardTrueMatchTarget, rectTransform.anchoredPosition) < 50)
            {
                matchTargetSet = false;
                Destroy(this.gameObject);
            }
        }

        switch (state)
        {
            case State.Closing:
                PlayCloseAnimation();
                break;
            case State.Closed:
                break;
            case State.Opening:
                PlayOpenAnimation();
                break;
            case State.Opened:
                break;
            case State.Matched:
                break;
            case State.notMatched:
                break;
            default:
                break;
        }
    }

    void PlayCloseAnimation()
    {
        GetComponent<Animator>().SetBool("chosen", false);
        GetComponent<Animator>().SetBool("wrong", true);
        state = State.Closed;
    }
    void PlayOpenAnimation()
    {
        GetComponent<Animator>().SetBool("chosen", true);
        state = State.Opened;
    }
}
