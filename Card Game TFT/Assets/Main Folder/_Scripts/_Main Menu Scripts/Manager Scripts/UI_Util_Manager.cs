using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Util_Manager : MonoBehaviour
{
    public void Pop_UI_Element(GameObject ui, Vector3 tweenTo, float time, float delay, LeanTweenType leanType)
    {
        LeanTween.scale(ui, tweenTo, time).setDelay(delay).setEase(leanType);
    }

    public void Loop_UI_Element(GameObject ui, Vector3 tweenTo, float time, float delay, LeanTweenType leanType)
    {
        LeanTween.scale(ui, tweenTo, time).setDelay(delay).setEase(leanType).setLoopPingPong();
    }

    public void Flip_UI_Element(GameObject ui, Vector3 tweenTo, float time, float delay, LeanTweenType leanType)
    {

    }
}
