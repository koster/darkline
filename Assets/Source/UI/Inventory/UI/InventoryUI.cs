using UnityEngine.UI;

public class InventoryUI : UILayerItemBase
{
    public static InventoryUI i;
    
    public Button close;

    public InventoryGridPanel gridPanel;
    public InventoryInfoPanel infoPanel;

    InventoryModel inventoryModel;

    void Awake()
    {
        i = this;
        Hide();
    }
    
    void Start()
    {
        close.onClick.AddListener(() =>
        {
            inventoryModel.onHide?.Invoke();
            UILayer.ReleaseFromQueue(this);
        });
    }
    
    public void Show(InventoryModel model)
    {
        UILayer.PushToQueue(this);
        
        inventoryModel = model;

        model.onCellSelected += OnCellSelected;
        model.onRefreshed += OnRefresh;

        inventoryModel.Initialize();
    }

    void OnRefresh()
    {
        gridPanel.ShowList(inventoryModel);
    }

    void OnCellSelected(InventoryCellModel arg0)
    {
        if (arg0 == null)
            infoPanel.Empty();
        else
            infoPanel.Show(inventoryModel, arg0);
    }
}