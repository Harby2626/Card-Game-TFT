using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class deck_empty_message : MonoBehaviour
{
    CanvasGroup canvas_group;
    RectTransform rect;
    bool anim_done = false;

    private void Start()
    {
        canvas_group = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!anim_done)
        {
            rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + 200), 1 * Time.deltaTime);
            canvas_group.alpha = Mathf.Lerp(canvas_group.alpha, 0, (1f * 2f) * Time.deltaTime);
            if (Mathf.Abs(0 - canvas_group.alpha) < 0.05f)
            {
                anim_done = true;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
