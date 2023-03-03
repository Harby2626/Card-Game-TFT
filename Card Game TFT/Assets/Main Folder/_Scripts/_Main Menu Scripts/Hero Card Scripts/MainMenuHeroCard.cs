using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuHeroCard : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, ISelectHandler ,IDeselectHandler
{
    DeselectClicks deselect;

    private void Start()
    {
        deselect = GameObject.Find("Card Deck Menu").GetComponent<DeselectClicks>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        deselect.CurrentSelected?.GetComponent<MainMenuHeroCard>().ChangeVisibility(false);
        deselect.CurrentSelected = gameObject;

        ChangeVisibility(true);
        //Debug.Log("PointerClick");
    }

    public void ChangeVisibility(bool visible) 
    {
        if (transform.parent.name == "Current Deck Holder")
        {
            transform.GetChild(1).gameObject.SetActive(visible);
            transform.GetChild(3).gameObject.SetActive(visible);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(visible);
            transform.GetChild(2).gameObject.SetActive(visible);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("pointerDown");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("PointerEnter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("PointerExit");
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        //Debug.Log("PointerMove");
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("SELECT");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("DESELECT");
    }

    #region Hero Card Button Functions
    public void OpenCardInfoButton()
    {
        LeanTween.scale(GameObject.Find("Card Info"), Vector2.one, .1f).setEase(LeanTweenType.easeInSine);
    }

    public void AddToDeckButton()
    {
        // add to deck
    }

    public void RemoveToDeckButton()
    {
        // remove to deck
    }
    #endregion

}
