using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTimeHandler : MonoBehaviour
{
    MatchPuzzle_Manager puzzle_manager;


    Image time_fill; RectTransform akrep;
    [SerializeField] float _lerp_time;

    private void Start()
    {
        puzzle_manager = GameObject.Find("Match Puzzle Manager").GetComponent<MatchPuzzle_Manager>();

        time_fill = transform.GetChild(1).GetComponent<Image>();
        akrep = transform.GetChild(2).GetChild(0).GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Mathf.Abs(time_fill.fillAmount - 0.1f) < .01f)
        {
            puzzle_manager.time_ended = true;
        }
        if (puzzle_manager.puzzlePhase == MatchPuzzle_Manager.PuzzlePhase.Playing)
        {
            HandleFillAmount();
        }
    }

    void HandleFillAmount()
    {
        time_fill.fillAmount = Mathf.MoveTowards(time_fill.fillAmount, 0, (1 / _lerp_time) * Time.deltaTime);
        akrep.Rotate(new Vector3(0, 0, -75) * Time.deltaTime);
    }
}
