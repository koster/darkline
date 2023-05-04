using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCellView : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject selectedOutline;
    public Image icon;
    public TextMeshProUGUI count;
    public Image bg;

    public int index;

    public InventoryModel inventoryModel;
    public InventoryCellModel cellModel;

    public void SetContent(InventoryModel inventory_model, InventoryCellModel content)
    {
        inventoryModel = inventory_model;
        cellModel = content;
        UpdateView();
    }

    void UpdateView()
    {
        if (cellModel == null)
        {
            Empty();
            return;
        }

        bg.color = new Color(0.25f, 0.25f, 0.25f, 0.5f);
        icon.enabled = true;
        icon.sprite = cellModel.definition.icon;
        count.enabled = cellModel.count > 0;
        count.text = "x" + cellModel.count;
    }

    void Empty()
    {
        bg.color = new Color(0.25f, 0.25f, 0.25f, 0.25f);
        icon.enabled = false;
        count.enabled = false;
        NotSelected();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (cellModel == null)
            return;
        
        inventoryModel.SetSelectedCell(index);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cellModel == null)
            return;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cellModel == null)
            return;

    }

    void Update()
    {
        if (inventoryModel == null)
            return;
        
        if (inventoryModel.selectedCellIndex == index)
        {
            Selected();
        }
        else
        {
            NotSelected();
        }
    }

    void Selected()
    {
        selectedOutline.SetActive(true);
    }

    void NotSelected()
    {
        selectedOutline.SetActive(false);
    }
}