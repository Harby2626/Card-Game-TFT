using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MatchLeftText : MonoBehaviour
{
    Animator MatchTextAnimator;
    MatchPuzzle_Manager puzzle_manager;
    [SerializeField] TextMeshProUGUI matchLeft_Text;

    public enum TextState
    {
        changing,
        idle
    }
    public TextState textState;

    private void Start()
    {
        MatchTextAnimator = transform.parent.GetComponent<Animator>();
        textState = TextState.idle;
        puzzle_manager = GameObject.Find("Match Puzzle Manager").GetComponent<MatchPuzzle_Manager>();
    }


    // Update is called once per frame
    void Update()
    {
        matchLeft_Text.text = puzzle_manager.MoveAmount.ToString();

        switch (textState)
        {
            case TextState.changing:
                break;
            case TextState.idle:
                break;
            default:
                break;
        }
    }
}
