using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface UILayerItem
{
    public void Show();
    public void Hide();
    public Transform GetTransform();
}

public class UILayer : MonoBehaviour
{
    static UILayer i;

    public GameObject inputBlocker;
    public RectTransform darkBackground;
    
    List<UILayerItem> uiQueue = new List<UILayerItem>();

    void Awake()
    {
        i = this;
        
        darkBackground.gameObject.SetActive(false);
        inputBlocker.gameObject.SetActive(false);
    }

    public static void PushToQueue(UILayerItem target)
    {
        target.Show();

        if (i.uiQueue.Contains(target))
            i.uiQueue.Remove(target);

        i.uiQueue.Add(target);

        i.darkBackground.gameObject.SetActive(true);
        i.darkBackground.SetAsLastSibling();
        NarrativeBoxUI.i.transform.SetAsLastSibling();
        target.GetTransform().SetAsLastSibling();
    }

    public static void ReleaseFromQueue(UILayerItem target, bool dontHide = false)
    {
        if (i.uiQueue.Contains(target))
        {
            if (!dontHide) 
                target.Hide();
            i.uiQueue.Remove(target);

            if (i.uiQueue.Count == 0)
                i.darkBackground.gameObject.SetActive(false);
            else
                PushToQueue(i.uiQueue.Last());
        }
    }

    public static T Instantiate<T>(T target) where T : MonoBehaviour
    {
        return Instantiate(target, i.transform);
    }
    
    public static bool IsInFocus(UILayerItem item)
    {
        return i.uiQueue.Last() == item;
    }

    public static void BlockInput()
    {
        i.inputBlocker.SetActive(true);
        i.inputBlocker.transform.SetAsLastSibling();
    }

    public static void UnblockInput()
    {
        i.inputBlocker.SetActive(false);
    }

    public static void PushOnTop(NarrativeBoxUI narrativeBoxUI)
    {
        narrativeBoxUI.transform.SetAsLastSibling();
    }
}