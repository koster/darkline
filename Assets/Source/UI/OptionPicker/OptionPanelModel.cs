using System.Collections.Generic;
using UnityEngine.Events;

public class OptionSectionModel
{
    public string sectionName;
    public List<OptionActionModel> actions = new List<OptionActionModel>();
}

public class OptionActionModel
{
    public InventoryItemDefinition itemDefinition;
    public WeaponActionDefinition weaponAction;
    public string optionName;
    public UnityAction onPicked;
    public bool used;
}

public class OptionPanelModel
{
    public int actionPoints;

    public int sectionIndex = -1;
    public OptionSectionModel selection;
    
    public List<OptionSectionModel> sections = new List<OptionSectionModel>();

    public UnityAction onRefresh;

    public void OnShow()
    {
        // if (sectionIndex == -1)
        // {
        //     if (sections.Count > 0)
        //     {
        //         SetSelection(sections[0]);
        //     }
        // }
        onRefresh?.Invoke();
    }

    public void SetSelection(OptionSectionModel target)
    {
        sectionIndex = sections.IndexOf(target);
        selection = target;
        onRefresh?.Invoke();
    }
}