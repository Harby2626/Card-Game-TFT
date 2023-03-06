using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] Image loading_fill_img;

    bool loading = true;
    float fill_target = .2f;
    private void Update()
    {
        if (loading)
        {
            LoadImageHandle();
        }
        else
        {
            SceneManager.LoadScene(2);// Load In-Game Scene
        }
    }

    void LoadImageHandle()
    {
        switch (fill_target)
        {
            case .2f:
                loading_fill_img.fillAmount = Mathf.Lerp(loading_fill_img.fillAmount, .2f, 2f * Time.deltaTime);
                if (Mathf.Abs(.2f - loading_fill_img.fillAmount) < 0.01f)
                {
                    fill_target = .6f;
                }
                break;

            case .6f:
                loading_fill_img.fillAmount = Mathf.Lerp(loading_fill_img.fillAmount, .6f, 2.5f * Time.deltaTime);
                if (Mathf.Abs(.6f - loading_fill_img.fillAmount) < 0.01f)
                {
                    fill_target = .75f;
                }
                break;

            case .75f:
                loading_fill_img.fillAmount = Mathf.Lerp(loading_fill_img.fillAmount, .75f, 2.75f * Time.deltaTime);
                if (Mathf.Abs(.75f - loading_fill_img.fillAmount) < 0.01f)
                {
                    fill_target = 1f;
                }
                break;

            case 1f:
                loading_fill_img.fillAmount = Mathf.Lerp(loading_fill_img.fillAmount, 1f, 4f * Time.deltaTime);
                if (Mathf.Abs(1f - loading_fill_img.fillAmount) < 0.01f)
                {
                    loading_fill_img.fillAmount = 1f;
                    loading = false;
                }
                break;

            default:
                break;
        }
    }
}
