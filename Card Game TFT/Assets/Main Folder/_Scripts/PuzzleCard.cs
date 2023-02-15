using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleCard : MonoBehaviour
{
    RectTransform rectTransform;
    MatchPuzzle_Manager puzzle_manager;
    public PuzzleCard_SO Card;

    public Vector2 CardSlotTarget;
    public bool setToTarget = false;
    private void Start()
    {

        rectTransform = GetComponent<RectTransform>();
        puzzle_manager = GameObject.Find("Match Puzzle Manager").GetComponent<MatchPuzzle_Manager>();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(puzzle_manager.ChoseCard);


    }

    private void Update()
    {
        if (setToTarget)// if public setToTarget boolean turns true, this card lerps to its target slot
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
        
    }

}
