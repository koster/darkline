using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipModel
{
    public Sprite icon;
    public string header;
    public List<string> lines = new List<string>();
}

public class UITooltip : MonoBehaviour
{
    public static UITooltip i;

    public Image icon;
    public TextMeshProUGUI header;
    public List<TextMeshProUGUI> lines;

    void Awake()
    {
        i = this;
        Hide();
    }

    public static void Show(TooltipModel model)
    {
        i.gameObject.SetActive(true);
        i.icon.sprite = model.icon;
        i.header.text = model.header;

        for (var j = 0; j < i.lines.Count; j++)
        {
            var textMeshProUGUI = i.lines[j];
            if (j < model.lines.Count)
            {
                textMeshProUGUI.gameObject.SetActive(true);
                textMeshProUGUI.text = model.lines[j];
            }
            else
                textMeshProUGUI.gameObject.SetActive(false);
        }

        i.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 160 + 15 * model.lines.Count);
    }

    public static void Hide()
    {
        i.gameObject.SetActive(false);
    }
}