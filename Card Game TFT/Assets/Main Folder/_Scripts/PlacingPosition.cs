using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlacingPosition : MonoBehaviour
{
    GameObject currentCharacter;

    public GameObject GetCurrentChar()
    {
        return currentCharacter;
    }

    public void SetCurrentChar(GameObject _character)
    {
        currentCharacter = _character;
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "fighter")
    //    {

    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "fighter")
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).GetComponent<Animator>().SetBool("snapped", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "fighter")
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).GetComponent<Animator>().SetBool("snapped", false);
        }
    }
}
