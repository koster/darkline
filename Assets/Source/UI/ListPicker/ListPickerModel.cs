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
        GameAnalytics.NewDesignEvent("option_" + modelOption.text);

        "sound/boop2".PlayClip();
        modelOption.optionCallback?.Invoke();
        OnSelected.Invoke(options.IndexOf(modelOption), modelOption);
    }
}

public class ListPickerOptionModel
{
    public string text;
    public UnityAction optionCallback = null;
    public ChoiceCost cost;
    public bool doesNotExit;
}
