using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    #region Scripts
    MatchPuzzle_Manager puzzle_manager;
    #endregion

    public bool interactable = false;

    Animator card_animator;

    [SerializeField] GameObject cardFighterObj; GameObject currentFighter;

    Image cardIMG; /*---*/ [SerializeField] Color dragColor; Color baseColor;

    RectTransform R_transform;
    Transform parentAfterDrag;

    RaycastHit hit; [SerializeField] LayerMask mask;

    private void Awake()
    {
        R_transform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        #region Script Attaching
        puzzle_manager = GameObject.Find("Match Puzzle Manager").GetComponent<MatchPuzzle_Manager>();
        #endregion

        card_animator = GetComponent<Animator>();
        cardIMG = GetComponent<Image>();
        baseColor = cardIMG.color;
    }

    private void Update()
    {
        if (puzzle_manager.puzzle_ended)
        {
            interactable = true;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (interactable)
        {
            card_animator.SetBool("dragBegin", true);
            cardIMG.color = dragColor;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();

            if (Input.touchCount > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out hit, 9999.9f))
                {
                    currentFighter = Instantiate(cardFighterObj, hit.point, hit.transform.rotation);
                    Character_Manager.Instance.AddPlayerCharacter(currentFighter.GetComponent<PlayerCharacter>());
                }
            }
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (interactable)
        {
            R_transform.anchoredPosition += eventData.delta;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 99999.9f, mask))
            {
                currentFighter.transform.position = hit.point;

                if (hit.collider.gameObject.layer == 7)
                {
                    currentFighter.transform.position = hit.collider.transform.position;
                }
            }
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (interactable)
        {
            card_animator.SetBool("dragBegin", false);
            cardIMG.color = baseColor;
            transform.SetParent(parentAfterDrag);


            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 99999.9f, mask))
            {
                if (hit.collider.gameObject.layer != 7)
                {
                    Destroy(currentFighter);
                }
            }
        }
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        dropped.GetComponent<UDCard>().dropped_onCard = true;

        if (dropped.GetComponent<UDCard>())// if dropped card is upgrade card
        {
            if (GetComponent<HeroCardUpgradeHandler>().GetActiveUpgradeCount() < GetComponent<HeroCardUpgradeHandler>().upgradeCount)
            {
                PuzzleCard_SO upgrade = dropped.GetComponent<UDCardEffect>().GetUpgradeType();
                GetComponent<HeroCardUpgradeHandler>().AddUpgrade(upgrade);
                Destroy(dropped);
            }

            else
            {
                dropped.GetComponent<UDCard>().ResetCard();
            }
        }
    }
}
