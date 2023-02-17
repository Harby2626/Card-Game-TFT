using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class DragDropScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
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
        card_animator = GetComponent<Animator>();
        cardIMG = GetComponent<Image>();
        baseColor = cardIMG.color;
    }

    public void OnBeginDrag(PointerEventData eventData)
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
            }
        }
        


    }

    public void OnDrag(PointerEventData eventData)
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

    public void OnEndDrag(PointerEventData eventData)
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