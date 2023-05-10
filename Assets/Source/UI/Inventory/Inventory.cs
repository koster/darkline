using System.Collections.Generic;
using GameAnalyticsSDK;
using Source.GameQueue;
using UnityEngine.Events;

public class InventoryCellModel
{
    public InventoryItemDefinition definition;
    public int count = -1;
    public List<InventoryActionModel> actions = new List<InventoryActionModel>();
}

public class InventoryActionModel
{
    public string name;
    public UnityAction<InventoryActionButton, InventoryCellModel> callback;
}

public class InventoryModel
{
    public int selectedCellIndex = -1;
    public InventoryCellModel selectedCellModel;
    
    public List<InventoryCellModel> content;

    public UnityAction onHide;
    public UnityAction onRefreshed;
    public UnityAction<InventoryCellModel> onCellSelected;
    public UnityAction<InventoryCellModel, InventoryActionModel> onCellAction;
    public GameQueue queue;

    public void Initialize()
    {
        RefreshState();
    }

    public void RefreshState()
    {
        onRefreshed?.Invoke();

        if (selectedCellIndex == -1)
        {
            if (content.Count > 0)
                SetSelectedCell(0);
            else
                SetSelectedCell(-1);
        }
        else if (content.Count <= selectedCellIndex)
            SetSelectedCell(content.Count - 1);
        else
            SetSelectedCell(selectedCellIndex);
    }

    public void SetSelectedCell(int cellIndex)
    {
        selectedCellIndex = cellIndex;
        
        if (cellIndex < 0)
            onCellSelected(null);
        else
        {
            if (selectedCellModel == content[cellIndex]) return;
            
            selectedCellModel = content[cellIndex];
            onCellSelected(content[cellIndex]);
        }
    }

    public void Add(InventoryCellModel newModel)
    {
        AddAt(content.Count, newModel);
    }

    public void AddAt(int index, InventoryCellModel newModel)
    {
        if (index < 0)
            index = 0;
        content.Insert(index, newModel);
        RefreshState();
    }

    public void Discard(InventoryCellModel model)
    {
        Discard(content.IndexOf(model));
    }
    
    public void Discard(int cellIndex)
    {
        content.RemoveAt(cellIndex);
        RefreshState();
    }

    public void FromInventory(Inventory invtry)
    {
        queue = Game.contextQueue;
        
        var actionExamine = new InventoryActionModel
        {
            name = "EXAMINE"
        };
        
        actionExamine.callback = (btn, icm) =>
        {
            GameAnalytics.NewDesignEvent("examine_item_inv" + icm.definition.name);
            queue.Add(new GCQueue(icm.definition.OnExamine()));
        };
        
        var actionUse = new InventoryActionModel
        {
            name = "USE"
        };
        
        actionUse.callback = (btn, icm) =>
        {
            GameAnalytics.NewDesignEvent("use_item_inv" + icm.definition.name);

            queue.Add(new GCQueue(icm.definition.OnUse()));
            
            if (icm.count == -1)
                return;

            invtry.Take(icm.definition, 1);
            icm.count--;
        
            if (icm.count <= 0)
            {
                Discard(icm);
            }
        
            RefreshState();
        };
        
        content = new List<InventoryCellModel>();

        foreach (var i in invtry.items)
        {
            if (i.amount == 0) continue;
            if (i.definition.hidden) continue;
            
            var item = new InventoryCellModel
            {
                definition = i.definition,
                count = i.amount
            };
            
            if (i.definition.OnUse != null)
                item.actions.Add(actionUse);
            
            if (i.definition.OnExamine != null)
                item.actions.Add(actionExamine);
            
            content.Add(item);
        }
    }
}