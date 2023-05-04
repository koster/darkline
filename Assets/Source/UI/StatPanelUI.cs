using UnityEngine;

public class StatPanelUI : MonoBehaviour
{
    static StatPanelUI i;

    void Awake()
    {
        i = this;
    }

    public static void Show()
    {
        i.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        i.gameObject.SetActive(false);
    }

    public static bool IsVisible()
    {
        return i.gameObject.activeSelf;
    }
}