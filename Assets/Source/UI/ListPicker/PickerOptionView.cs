using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickerOptionView : UILayerItemBase, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI label;
    public GameObject highlight;
    public GameObject pushHight;

    ListPickerModel modelPanel;
    ListPickerOptionModel modelOption;
    
    public void Show(ListPickerModel model, ListPickerOptionModel modelOption)
    {
        gameObject.SetActive(true);
        
        highlight.SetActive(false);
        pushHight.SetActive(false);
        
        this.modelPanel = model;
        this.modelOption = modelOption;
        
        label.text = modelOption.text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
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