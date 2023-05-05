using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeImageUI : MonoBehaviour
{
    static NarrativeImageUI i;

    public Image narrativeBackground;
    public Image image;

    void Awake()
    {
        i = this;
        gameObject.SetActive(false);
    }

    public static void Show(Sprite sprite, bool instant = false)
    {
        ShowBackground(instant);
        
        i.gameObject.SetActive(true);

        if (sprite != null)
        {
            i.image.color = new Color(1f, 1f, 1f, 0f);
            i.image.sprite = sprite;
            i.image.DOFade(1f, 1f);
            i.image.SetNativeSize();
        }
        else
        {
            i.image.DOFade(0f, 1f);
        }
    }

    public static void ShowBackground(bool instant = false)
    {
        if (!i.narrativeBackground.gameObject.activeSelf || !i.narrativeBackground.enabled)
        {
            i.narrativeBackground.enabled = true;
            i.narrativeBackground.gameObject.SetActive(true);
            i.narrativeBackground.color = new Color(1f, 1f, 1f, 0f);

            if (instant)
                i.narrativeBackground.color = new Color(1f, 1f, 1f, 1f);
            else
                i.narrativeBackground.DOFade(1f, 1f);
        }
    }

    public static void HideBackground()
    {
        i.narrativeBackground.gameObject.SetActive(false);
    }
    
    public static void Hide()
    {
        i.narrativeBackground.DOFade(0, 1f);
        i.image.DOFade(0f, 1f).OnComplete(() =>
        {
            HideBackground();
            i.gameObject.SetActive(false);
        });
    }

    public static void Tint(Color color, float time)
    {
        if (time == 0)
        {
            i.narrativeBackground.color = color;
        }
        else
        {
            i.narrativeBackground.DOColor(color, time);
        }
    }
}