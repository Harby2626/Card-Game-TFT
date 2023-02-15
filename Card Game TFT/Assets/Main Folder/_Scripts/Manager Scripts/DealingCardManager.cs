using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DealingCardManager : MonoBehaviour
{
    MatchPuzzle_Manager matchPuzzleManager;
    private void Start()
    {
        matchPuzzleManager = GameObject.Find("Match Puzzle Manager").GetComponent<MatchPuzzle_Manager>();
    }

    private void Update()
    {

    }

}
