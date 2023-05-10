using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInfoPanel : MonoBehaviour
{
    public GameObject contentState;
    public GameObject emptyState;
    
    public Image icon;
    public TextMeshProUGUI title;
    public TextMeshProUGUI desc;
    public TextMeshProUGUI stat;
    public List<InventoryActionButton> buttons;

    public void Show(InventoryModel inventory_model, InventoryCellModel cell_model)
    {
        contentState.SetActive(true);
        emptyState.SetActive(false);

        icon.sprite = cell_model.definition.icon;
        title.text = cell_model.definition.name;
        desc.text = cell_model.definition.desc;
        stat.text = cell_model.definition.GetStatsLabel();

        for (var i = 0; i < buttons.Count; i++)
        {
            if (i < cell_model.actions.Count)
            {
                buttons[i].SetAction(inventory_model, cell_model, cell_model.actions[i]);
            }
            else
            {
                buttons[i].Hide();
            }
        }
    }

    public void Empty()
    {
        contentState.SetActive(false);
        emptyState.SetActive(true);
    }
}