using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryActionButton : MonoBehaviour
{
    public TextMeshProUGUI label;

    InventoryActionModel actionModel;
    InventoryCellModel cellModel;
    InventoryModel inventoryModel;

    float cooldown = 0f;
    Button component;

    void Start()
    {
        component = GetComponent<Button>();
        component.onClick.AddListener(OnClick);
    }

    public void SetAction(InventoryModel inventory_model, InventoryCellModel cell_model, InventoryActionModel action_model)
    {
        cooldown = 0;
        gameObject.SetActive(true);
        inventoryModel = inventory_model;
        cellModel = cell_model;
        actionModel = action_model;
        label.text = this.actionModel.name;
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
        component.interactable = cooldown < 0;
    }

    void OnClick()
    {
        cooldown = 1f;
        inventoryModel.onCellAction?.Invoke(cellModel, actionModel);
        actionModel.callback?.Invoke(this, cellModel);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}