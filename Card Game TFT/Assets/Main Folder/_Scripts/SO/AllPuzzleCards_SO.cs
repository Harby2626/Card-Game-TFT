using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "All Puzzle Cards")]
public class AllPuzzleCards_SO : ScriptableObject
{
    public List<GameObject> AllPuzzleCards = new List<GameObject>();

    public List<string> DeckCardTags = new List<string>();

    public void AddCardToList(string tag)
    {
        DeckCardTags.Add(tag);
    }

    public void RemoveCardToList(string tag)
    {
        DeckCardTags.Remove(tag);
    }

    public int GetDeckCount()
    {
        return DeckCardTags.Count;
    }
}
