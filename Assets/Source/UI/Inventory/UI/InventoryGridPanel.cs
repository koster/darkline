using System.Collections.Generic;
using UnityEngine;

public class InventoryGridPanel : MonoBehaviour
{
    public List<InventoryCellView> cells = new List<InventoryCellView>();
    
    public void ShowList(InventoryModel inventory_model)
    {
        for (var i = 0; i < cells.Count; i++)
        {
            var ic = cells[i];
            ic.index = i;
            
            if (i < inventory_model.content.Count)
            {
                ic.SetContent(inventory_model, inventory_model.content[i]);
            }
            else
            {
                ic.SetContent(inventory_model, null);
            }
        }
    }
}
