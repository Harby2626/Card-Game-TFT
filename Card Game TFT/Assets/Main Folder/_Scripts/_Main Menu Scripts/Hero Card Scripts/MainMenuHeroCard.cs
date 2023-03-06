using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuHeroCard : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public string type_tag;

    GameObject CurrentDeckHolder, CurrentInventoryHolder;
    DeselectClicks deselect;

    private void Start()
    {
        type_tag = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text;

        CurrentDeckHolder = GameObject.Find("Current Deck Holder");
        CurrentInventoryHolder = GameObject.Find("Card Inventory Holder");
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


    #region Hero Card Button Functions
    public void OpenCardInfoButton()
    {
        LeanTween.scale(GameObject.Find("Card Info"), Vector2.one, .1f).setEase(LeanTweenType.easeInSine);
    }

    public void AddToDeckButton()
    {
        MainDeckManager.instance.AddHeroCard_Deck(this.gameObject);
        this.gameObject.transform.SetParent(CurrentDeckHolder.transform);


        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(2).gameObject.SetActive(false);
    }

    public void RemoveToDeckButton()
    {
        MainDeckManager.instance.RemoveHeroCard_Deck(this.gameObject);
        this.gameObject.transform.SetParent(CurrentInventoryHolder.transform);


        this.transform.GetChild(1).gameObject.SetActive(false);
        this.transform.GetChild(3).gameObject.SetActive(false);
    }
    #endregion

}
