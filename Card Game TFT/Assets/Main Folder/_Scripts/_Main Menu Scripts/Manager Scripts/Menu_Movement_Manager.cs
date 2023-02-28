using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Menu_Movement_Manager : MonoBehaviour
{
    Vector2 origin, init_touch_pos;

    public GameObject MenuGroup;

    public float MenuLerpModifier, menu_release_lerp_modifier;

    public enum SwipeTo
    {
        idle, menu_1_0, menu_1_2, menu_0_1, menu_2_1
    }

    public SwipeTo swipeTo;

    private void Start()
    {
        swipeTo = SwipeTo.idle;
        origin = new Vector2((float)Screen.width / 2, (float)Screen.height / 2);
    }


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    init_touch_pos = touch.position - origin;
                    Debug.Log(init_touch_pos);
                    break;
                
                case TouchPhase.Moved:
                    Vector2 moved_touch_pos = (touch.position - origin);
                    //Debug.Log(moved_touch_pos.x - init_touch_pos.x);
                    SwipeMenuGroup(touch.deltaPosition.x, MenuLerpModifier);
                    break;
                
                case TouchPhase.Stationary:
                    break;
                
                case TouchPhase.Ended:
                    float menu_x = MenuGroup.GetComponent<RectTransform>().anchoredPosition.x;
                    if (menu_x > -1200f && menu_x < -800f)// Card Deck Menu Swipe
                    {
                        swipeTo = SwipeTo.menu_1_2;
                    }
                    else if (menu_x > 800f && menu_x < 1200f)// Settings Menu Swipe
                    {
                        swipeTo = SwipeTo.menu_1_0;
                    }
                    else if (menu_x > -800 && menu_x < 0f)
                    {
                        swipeTo = SwipeTo.menu_2_1;
                    }
                    else if (menu_x > 0f && menu_x < 800f)
                    {
                        swipeTo = SwipeTo.menu_0_1;
                    }
                    break;
                
                case TouchPhase.Canceled:
                    break;
                
                default:
                    break;
            }
        }

        switch (swipeTo)
        {
            case SwipeTo.menu_1_0:
                float lerp_pos0 = 1550f;
                MenuGroup.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(MenuGroup.GetComponent<RectTransform>().anchoredPosition,
                                                                                        new Vector2(lerp_pos0, 0), menu_release_lerp_modifier * Time.deltaTime);
                if (Mathf.Abs(lerp_pos0 - MenuGroup.GetComponent<RectTransform>().anchoredPosition.x) < 0.05f)
                {
                    swipeTo = SwipeTo.idle;
                }
                break;
            
            case SwipeTo.menu_1_2:
                float lerp_pos1 = -1550f;
                MenuGroup.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(MenuGroup.GetComponent<RectTransform>().anchoredPosition,
                                                                                        new Vector2(lerp_pos1, 0), menu_release_lerp_modifier * Time.deltaTime);
                if (Mathf.Abs(lerp_pos1 - MenuGroup.GetComponent<RectTransform>().anchoredPosition.x) < 0.05f)
                {
                    swipeTo = SwipeTo.idle;
                }
                break;
            
            case SwipeTo.menu_0_1:
                float lerp_pos2 = 0;
                MenuGroup.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(MenuGroup.GetComponent<RectTransform>().anchoredPosition,
                                                                                        new Vector2(lerp_pos2, 0), menu_release_lerp_modifier * Time.deltaTime);
                if (Mathf.Abs(lerp_pos2 - MenuGroup.GetComponent<RectTransform>().anchoredPosition.x) < 0.05f)
                {
                    swipeTo = SwipeTo.idle;
                }
                break;
            
            case SwipeTo.menu_2_1:
                float lerp_pos3 = 0;
                MenuGroup.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(MenuGroup.GetComponent<RectTransform>().anchoredPosition,
                                                                                        new Vector2(lerp_pos3, 0), menu_release_lerp_modifier * Time.deltaTime);
                if (Mathf.Abs(lerp_pos3 - MenuGroup.GetComponent<RectTransform>().anchoredPosition.x) < 0.05f)
                {
                    swipeTo = SwipeTo.idle;
                }
                break;
            
            default:
                break;
        }
    }

    void SwipeMenuGroup(float moved_posX, float lerp)
    {
        Vector2 MenuGroupPos = MenuGroup.GetComponent<RectTransform>().anchoredPosition;
        Vector2 LerpPos = new Vector2(MenuGroupPos.x + moved_posX * 2, MenuGroupPos.y);
        // Clamping between borders
        LerpPos.x = Mathf.Clamp(LerpPos.x, -1550f, 1550f);
        MenuGroup.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(MenuGroupPos, LerpPos, lerp * Time.deltaTime);
    }

}
