using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Source.Game.Deliveries;
using TMPro;
using UnityEngine;

public class DeliveryCompletedUI : UILayerItemBase
{
    static DeliveryCompletedUI i;

    public TextMeshProUGUI label;
    public CanvasGroup cg;
    public List<GameObject> pieces;

    int count;
    
    void Awake()
    {
        i = this;
        i.gameObject.SetActive(false);
        
        for (var i = 0; i < pieces.Count; i++)
        {
            pieces[i].SetActive(false);
        }
    }

    public static void Show(int deliveryNumber)
    {
        "sound/sfx_sh_short".PlayClip();
        i.transform.localScale = Vector3.one;
        i.cg.alpha = 0;
        i.cg.DOFade(1f, 1f);
        i.transform.DOScale(1.2f, 2f);
        i.count = deliveryNumber;
        i.Show();

        i.StartCoroutine(FlipNumber());
    }

    static IEnumerator FlipNumber()
    {
        yield return new WaitForSeconds(1f);
        i.count++;

        for (var j = 0; j < i.pieces.Count; j++)
        {
            i.pieces[j].SetActive(j < i.count);
        }
    }

    void Update()
    {
        i.label.text = (DeliveriesDatabase.all.Count - count) + " left...";
    }

    public new static void Hide()
    {
        for (var j = 0; j < i.pieces.Count; j++)
        {
            i.pieces[j].SetActive(false);
        }

        i.cg.DOFade(0f, 1f).OnComplete(() =>
        {
            i.gameObject.SetActive(false);
        });
    }
}