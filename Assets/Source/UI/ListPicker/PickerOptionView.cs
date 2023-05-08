using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickerOptionView : UILayerItemBase, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI label;
    
    public Image background;
    public GameObject highlight;
    public GameObject pushHight;

    ListPickerModel modelPanel;
    ListPickerOptionModel modelOption;

    float cooldown;
    
    public void Show(ListPickerModel model, ListPickerOptionModel modelOption)
    {
        cooldown = 0.33f;
        gameObject.SetActive(true);

        if (modelOption.doesNotExit)
        {
            label.color = Color.white;
            background.color = new Color(0.16f, 0.16f, 0.16f, 1f);
        }
        else
        {
            label.color = Color.white;
            background.color = new Color(0.8f, 0, 0.8f, 1f);
        }
        
        highlight.SetActive(false);
        pushHight.SetActive(false);
        
        this.modelPanel = model;
        this.modelOption = modelOption;
        
        label.text = modelOption.text;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cooldown > 0) return;
        modelPanel.SetSelection(modelOption);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        highlight.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pushHight.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pushHight.SetActive(false);
    }

    void OnShowMessage()
    {
        highlight.SetActive(false);
        pushHight.SetActive(false);
    }

    void OnHideMessage()
    {
        highlight.SetActive(false);
        pushHight.SetActive(false);
    }
}