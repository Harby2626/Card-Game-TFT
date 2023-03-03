using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DeselectClicks : MonoBehaviour, IPointerClickHandler
{
    private GameObject currentSelected;
    public GameObject CurrentSelected { get => currentSelected; set => currentSelected = value; }

    public void OnPointerClick(PointerEventData eventData)
    {

        CurrentSelected?.GetComponent<MainMenuHeroCard>().ChangeVisibility(false);
        CurrentSelected = EventSystem.current.currentSelectedGameObject;
    }
}
