using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewDeliveryUI : UILayerItemBase
{
    static NewDeliveryUI i;

    public CanvasGroup icon_cg;
    public Image icon;
    public TextMeshProUGUI label;
    public TextMeshProUGUI desc;
    public CanvasGroup cg;

    void Awake()
    {
        i = this;
        i.gameObject.SetActive(false);
    }

    public static void Show(InventoryItemDefinition def)
    {
        i.icon_cg.alpha = 0;
        
        "sound/sfx_sh_short".PlayClip();
        
        i.transform.localScale = Vector3.one;
        i.cg.alpha = 0;
        i.cg.DOFade(1f, 1f);
        i.icon_cg.DOFade(1f, 1f).SetDelay(1f);
        i.transform.DOScale(1.2f, 2f);
        i.label.text = def.name;
        i.desc.text = def.desc;
        i.icon.sprite = def.icon;
        i.Show();
    }

    public new static void Hide()
    {
        DeliveryUI.Reset();
        
        i.cg.DOFade(0f, 1f).OnComplete(() =>
        {
            i.gameObject.SetActive(false);
        });
    }
}