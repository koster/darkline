using System.Collections.Generic;
using GameAnalyticsSDK;
using Source.Game.Deliveries;
using UnityEngine.Events;

public class ListPickerModel
{
    public List<ListPickerOptionModel> options = new List<ListPickerOptionModel>();
    public UnityAction<int, ListPickerOptionModel> OnSelected;

    public void SetSelection(ListPickerOptionModel modelOption)
    {
        GameAnalytics.NewDesignEvent("option_" + Sanitize(modelOption));

        "sound/boop2".PlayClip();
        modelOption.cost?.Take();
        modelOption.optionCallback?.Invoke();
        OnSelected.Invoke(options.IndexOf(modelOption), modelOption);
    }

    static string Sanitize(ListPickerOptionModel modelOption)
    {
        return modelOption.text
            .Replace("-","")
            .Replace("'","")
            .Replace("\\","")
            .Replace("\"","");
    }
}

public class ListPickerOptionModel
{
    public string text;
    public UnityAction optionCallback = null;
    public ChoiceCost cost;
    public ChoiceCondition condition = new ChoiceCondition();
    public bool doesNotExit;
}

public class ChoiceCondition
{
    public List<string> all_facts = new List<string>();
    public List<string> any_of_facts = new List<string>();
    
    public bool Evaluate()
    {
        foreach (var f in all_facts)
        {
            if (!Game.world.player.factsKnown.Contains(f))
                return false;
        }
        
        foreach (var f in any_of_facts)
        {
            if (Game.world.player.factsKnown.Contains(f))
                return true;
        }

        return true;
    }
}
