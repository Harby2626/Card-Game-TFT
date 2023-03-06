using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeckManager : MonoBehaviour
{
    [SerializeField] AllPuzzleCards_SO cards_database;

    [SerializeField] GameObject InGameDeckCont;
    [SerializeField] List<GameObject> AllHeroCards = new List<GameObject>();

    private void Start()
    {
        SpawnHeroDeckCards();
    }

    public void SpawnHeroDeckCards()
    {
        // GET GAMEOBJECTS FROM STRING LIST FROM SO'S;
    }
}
